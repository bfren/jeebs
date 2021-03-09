// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Entities;
using Jm.WordPress.Query.Wrapper.Posts;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Add meta dictionary to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="posts">Posts</param>
		private async Task<Option<TList>> AddMetaAsync<TList, TModel>(TList posts)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there is a meta property for this model
			return GetMetaDictionaryInfo<TModel>() switch
			{
				Some<Meta<TModel>> meta =>
					await Option
						.Wrap(posts)
						.BindAsync(
							getMetaAsync,
							e => new AddMetaExceptionMsg(e)
						)
						.BindAsync(
							x => setMeta(x, meta.Value),
							e => new SetMetaExceptionMsg(e)
						),

				_ =>
					posts
			};

			//
			// Get Post Meta values
			//
			async Task<Option<(TList, List<PostMeta>)>> getMetaAsync(TList posts)
			{
				return await Option.True
					.Map(getOptions)
					.Map(getQuery)
					.BindAsync(getMeta)
					.BindAsync(createTuple);

				// Get query options
				QueryPostsMeta.Options getOptions() =>
					new() { PostIds = posts.Select(p => p.Id).ToList() };

				// Get query
				IQuery<PostMeta> getQuery(QueryPostsMeta.Options options) =>
					StartNewQuery()
						.WithModel<PostMeta>()
						.WithOptions(options)
						.WithParts(new QueryPostsMeta.Builder<PostMeta>(db))
						.GetQuery();

				// Execute query to get meta
				async Task<Option<List<PostMeta>>> getMeta(IQuery<PostMeta> query) =>
					await query.ExecuteQueryAsync();

				// Create tuple of posts and meta
				Option<(TList, List<PostMeta>)> createTuple(List<PostMeta> meta) =>
					(posts, meta);
			}

			//
			// Set Meta for each Post
			//
			Option<TList> setMeta((TList, List<PostMeta>) lists, Meta<TModel> meta)
			{
				var (posts, postsMeta) = lists;
				if (postsMeta.Count > 0)
				{
					// Add meta to each post
					foreach (var post in posts)
					{
						var postMeta = from m in postsMeta
									   where m.PostId == post.Id
									   select new KeyValuePair<string, string>(m.Key, m.Value);

						if (!postMeta.Any())
						{
							continue;
						}

						// Set the value of the meta property
						meta.Set(post, new MetaDictionary(postMeta));
					}
				}

				return posts;
			}
		}

		private record PostMeta : WpPostMetaEntity { }
	}
}
