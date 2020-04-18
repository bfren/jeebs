using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
		public IQuery<T> QueryPosts<T>(Action<QueryPosts.Options>? modifyOptions = null)
		{
			return StartNewQuery()
				.WithModel<T>()
				.WithOptions(modifyOptions)
				.WithParts(new QueryPosts.Builder<T>(db))
				.GetQuery();
		}

		/// <summary>
		/// Add Meta and Custom Fields to the list of posts
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">Expression to return MetaDictionary property</param>
		public async Task<IResult<bool>> AddMetaAndCustomFieldsToPostsAsync<T>(IEnumerable<T> posts)
			where T : IEntity
		{
			// Get MetaDictionary
			if (GetMetaDictionary<T>() is PropertyInfo<T, MetaDictionary> meta)
			{
				// Get Meta
				var addMetaResult = await AddMetaToPostsAsync(posts, meta);
				if (addMetaResult.Err is IErrorList addMetaErr)
				{
					return Result.Failure(addMetaErr);
				}

				// Get Custom Fields
				var addCustomFieldsResult = await AddCustomFieldsToPostsAsync(posts, meta);
				if (addCustomFieldsResult.Err is IErrorList addCustomFieldsErr)
				{
					return Result.Failure(addCustomFieldsErr);
				}

				// Return success
				return Result.Success();
			}

			throw new Jx.WordPress.QueryException($"Model {typeof(T)} does not have a MetaDictionary property.");
		}

		/// <summary>
		/// Add meta values to posts
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">MetaDictionary property</param>
		private async Task<IResult<bool>> AddMetaToPostsAsync<T>(IEnumerable<T> posts, PropertyInfo<T, MetaDictionary> meta)
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
			var query = StartNewQuery()
				.WithModel<PostMeta>()
				.WithOptions(options)
				.WithParts(new QueryPostsMeta.Builder<PostMeta>(db))
				.GetQuery();

			// Get meta
			var metaResult = await query.ExecuteQuery();

			// Return errors if there are any
			if (metaResult.Err is IErrorList)
			{
				return Result.Failure(metaResult.Err);
			}
			// If no meta, return success
			else if (!metaResult.Val.Any())
			{
				return Result.Success();
			}

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
				meta.Set(post, new MetaDictionary(postMeta));
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Add custom fields to posts
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">MetaDictionary property</param>
		private async Task<IResult<bool>> AddCustomFieldsToPostsAsync<T>(IEnumerable<T> posts, PropertyInfo<T, MetaDictionary> meta)
			where T : IEntity
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
			}

			// Get all custom field properties from the model
			var customFields = GetCustomFields<T>();

			// If no custom fields, return success
			if (customFields.Count == 0)
			{
				return Result.Success();
			}

			// Hydrate all custom fields for all posts
			foreach (var post in posts)
			{
				// If post is null, continue
				if (post == null)
				{
					continue;
				}

				// Get meta
				var metaDictionary = meta.Get(post);

				// Add each custom field
				foreach (var customField in customFields)
				{
					// Get field property
					var customFieldProperty = (ICustomField)post.GetProperty(customField.Name);

					// Hydrate the field
					var result = await customFieldProperty.Hydrate(db, unitOfWork, metaDictionary);
					if (result.Err is IErrorList err && customFieldProperty.IsRequired)
					{
						return Result.Failure(err);
					}
				}
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Add Taxonomies to posts - <typeparamref name="TTerm"/> can contain columns from Term and TermRelationship tables
		/// </summary>
		/// <typeparam name="TPost">Post model</typeparam>
		/// <typeparam name="TTerm">Term model</typeparam>
		/// <param name="posts"></param>
		/// <param name="taxonomies"></param>
		public async Task<IResult<bool>> AddTaxonomiesToPostsAsync<TPost, TTerm>(IEnumerable<TPost> posts)
			where TPost : IEntity
			where TTerm : ITerm
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
			}



			// Don't do anything if there aren't any taxonomies
			if (taxonomies.Length == 0)
			{
				return Result.Failure("No taxonomies to add.");
			}

			// Get post IDS
			var postIds = posts.Select(p => p.Id).ToList();

			// Add each taxonomy
			foreach ((var taxonomy, var postTaxonomy) in taxonomies)
			{
				// Build query
				var query = StartNewQuery()
					.WithModel<TTerm>()
					.WithOptions<QueryPostsTaxonomy.Options>(opt =>
					{
						opt.Taxonomy = taxonomy;
						opt.PostIds = postIds;
					})
					.WithParts(new QueryPostsTaxonomy.Builder<TTerm>(db))
					.GetQuery();

			}

			throw new NotImplementedException();
		}

		/// <summary>
		/// Private PostMeta entity for meta queries
		/// </summary>
		private class PostMeta : Entities.WpPostMetaEntity { }
	}
}
