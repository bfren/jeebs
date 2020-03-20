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
		public QueryExec<T> Posts<T>(Action<Posts.QueryOptions>? modifyOptions = null)
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
		public async Task AddPostsMeta<T>(IEnumerable<T> posts, Expression<Func<T, MetaDictionary>> meta)
			where T : IEntity
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return;
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
			if (allMeta.Err is ErrorList)
			{
				throw new Jx.Data.QueryException("Error fetching meta to add to posts.");
			}
			else if (!allMeta.Val.Any())
			{
				return;
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
		}

		public class PostMeta : Entities.WpPostMetaEntity { }
	}
}
