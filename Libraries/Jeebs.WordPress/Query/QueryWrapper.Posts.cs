using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Reflection;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query wrapper
	/// </summary>
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		public Query<T> QueryPosts<T>(Action<QueryPosts.Options>? modifyOptions = null)
		{
			return StartNewQuery()
				.WithOptions(modifyOptions)
				.BuildParts(opt => new QueryPosts.Builder<T>(db).Build(opt));
		}

		/// <summary>
		/// Add Meta and Custom Fields to the list of posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">Expression to return MetaDictionary property</param>
		public async Task<Result> AddMetaAndCustomFieldsToPosts<T>(IEnumerable<T> posts, Expression<Func<T, MetaDictionary>> meta)
			where T : IEntity
		{
			// Add Meta
			var addMetaResult = await AddMetaToPosts(posts, meta);
			if (addMetaResult.Err is ErrorList addMetaErr)
			{
				return Result.Failure(addMetaErr);
			}

			// Add Custom Fields
			var addCustomFieldsResult = await AddCustomFieldsToPosts(posts, meta);
			if (addCustomFieldsResult.Err is ErrorList addCustomFieldsErr)
			{
				return Result.Failure(addCustomFieldsErr);
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Add meta values to posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">Expression to return MetaDictionary property</param>
		private async Task<Result> AddMetaToPosts<T>(IEnumerable<T> posts, Expression<Func<T, MetaDictionary>> meta)
			where T : IEntity
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
			}

			// Create options
			var options = new QueryPostsMeta.Options
			{
				PostIds = posts.Select(p => p.Id).ToList()
			};

			// Get Exec
			var query = new QueryPostsMeta.Builder<PostMeta>(db)
				.Build(options)
				.GetQuery(unitOfWork);

			// Get meta
			var metaResult = await query.ExecuteQuery();

			// Return errors if there are any
			if (metaResult.Err is ErrorList)
			{
				return Result.Failure(metaResult.Err);
			}
			// If no meta, return success
			else if (!metaResult.Val.Any())
			{
				return Result.Success();
			}

			// Prepare expression for use
			var metaPropertyInfo = meta.GetPropertyInfo();

			// Add meta to each post
			foreach (var post in posts)
			{
				var postMeta = from m in metaResult.Val
							   where m.PostId == post.Id
							   select new KeyValuePair<string, string>(m.Key, m.Value);

				if (!postMeta.Any())
				{
					continue;
				}

				// Set the value of the meta property
				metaPropertyInfo.Set(post, new MetaDictionary(postMeta));
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Add custom fields to posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">Expression to return MetaDictionary property</param>
		private async Task<Result> AddCustomFieldsToPosts<T>(IEnumerable<T> posts, Expression<Func<T, MetaDictionary>> meta)
			where T : IEntity
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
			}

			// Get all custom field properties from the model
			var customFields = from cf in typeof(T).GetProperties()
							   where cf.PropertyType.GetInterfaces().Contains(typeof(ICustomField))
							   select cf;

			// If no custom fields, return success
			if (!customFields.Any())
			{
				return Result.Success();
			}

			// Hydrate all custom fields for all posts
			var metaFunc = meta.Compile();
			foreach (var post in posts)
			{
				// If post is null, continue
				if (post == null)
				{
					continue;
				}

				// Get meta
				var metaDictionary = metaFunc.Invoke(post);

				// Add each custom field
				foreach (var customField in customFields)
				{
					// Get field property
					var customFieldProperty = (ICustomField)post.GetProperty(customField.Name);

					// Hydrate the field
					var result = await customFieldProperty.Hydrate(db, unitOfWork, metaDictionary);
					if (result.Err is ErrorList err && customFieldProperty.IsRequired)
					{
						return Result.Failure(err);
					}
				}
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Private PostMeta entity for meta queries
		/// </summary>
		private class PostMeta : Entities.WpPostMetaEntity { }
	}
}
