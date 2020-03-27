using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query wrapper
	/// </summary>
	public sealed partial class Query : Data.Query
	{
		/// <summary>
		/// Query Posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		public QueryExec<T> QueryPosts<T>(Action<Posts.QueryOptions>? modifyOptions = null)
		{
			// Create and modify options
			var options = new Posts.QueryOptions();
			modifyOptions?.Invoke(options);

			// Get Exec
			return new Posts.QueryBuilder<T>(db)
				.Build(options)
				.GetExec(UnitOfWork);
		}

		/// <summary>
		/// Add meta values to posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">Expression to return MetaDictionary property</param>
		public async Task<Result> AddMetaToPosts<T>(IEnumerable<T> posts, Expression<Func<T, MetaDictionary>> meta)
			where T : IEntity
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
			}

			// Create options
			var options = new PostsMeta.QueryOptions
			{
				PostIds = posts.Select(p => p.Id).ToList()
			};

			// Get Exec
			var exec = new PostsMeta.QueryBuilder<PostMeta>(db)
				.Build(options)
				.GetExec(UnitOfWork);

			// Get meta
			var allMeta = await exec.Retrieve();

			// Return errors if there are any
			if (allMeta.Err is ErrorList)
			{
				return Result.Failure(allMeta.Err.Prepend("Error fetching meta to add to posts.").ToArray());
			}
			// If no meta, return success
			else if (!allMeta.Val.Any())
			{
				return Result.Success();
			}

			// Prepare expression for use
			var metaPropertyInfo = meta.GetPropertyInfo();

			// Add meta to each post
			foreach (var post in posts)
			{
				var postMeta = from m in allMeta.Val
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
		public async Task<Result> AddCustomFieldsToPosts<T>(IEnumerable<T> posts, Expression<Func<T, MetaDictionary>> meta)
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
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
