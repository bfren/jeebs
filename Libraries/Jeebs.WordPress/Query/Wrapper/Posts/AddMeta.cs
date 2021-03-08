// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Data;
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
		/// <param name="r">Result - value is list of posts</param>
		private async Task<IR<TList>> AddMetaAsync<TList, TModel>(IOkV<TList> r)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there is a meta property for this model
			if (GetMetaDictionaryInfo<TModel>() is Some<Meta<TModel>> s)
			{
				return r
					.Link()
						.Catch().AllUnhandled().With<GetMetaExceptionMsg>()
						.MapAsync(getMetaAsync).Await()
					.Link()
						.Catch().AllUnhandled().With<SetMetaExceptionMsg>()
						.Map(okV => setMeta(okV, s.Value));
			}

			return r;

			//
			// Get Post Meta values
			//
			async Task<IR<(TList, List<PostMeta>)>> getMetaAsync(IOkV<TList> r)
			{
				// Create options
				var options = new QueryPostsMeta.Options
				{
					PostIds = r.Value.Select(p => p.Id).ToList()
				};

				// Get query
				var query = StartNewQuery()
					.WithModel<PostMeta>()
					.WithOptions(options)
					.WithParts(new QueryPostsMeta.Builder<PostMeta>(db))
					.GetQuery();

				// Get meta
				return (await query.ExecuteQueryAsync(r).ConfigureAwait(false)).Switch(
					x => x.OkV((r.Value, x.Value))
				);
			}

			//
			// Set Meta for each Post
			//
			IR<TList> setMeta(IOkV<(TList, List<PostMeta>)> r, Meta<TModel> meta)
			{
				var (posts, postsMeta) = r.Value;

				// If no meta, return success
				if (postsMeta.Count == 0)
				{
					return r.OkV(posts);
				}

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

				return r.OkV(posts);
			}
		}

		private record PostMeta : WpPostMetaEntity { }
	}
}
