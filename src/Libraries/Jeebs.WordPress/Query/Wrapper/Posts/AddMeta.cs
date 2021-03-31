// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Querying;
using Jeebs.WordPress.Entities;
using static F.OptionF;

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
		private Task<Option<TList>> AddMetaAsync<TList, TModel>(TList posts)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there is a meta property for this model
			return GetMetaDictionaryInfo<TModel>() switch
			{
				Some<Meta<TModel>> meta =>
					Return(posts)
						.BindAsync(
							getMetaAsync
						)
						.BindAsync(
							x => setMeta(x, meta.Value)
						),

				_ =>
					Return(posts).AsTask
			};

			//
			// Get Post Meta values
			//
			Task<Option<(TList, List<PostMeta>)>> getMetaAsync(TList posts)
			{
				return
					Return(
						getOptions,
						DefaultHandler
					)
					.Bind(
						getQuery
					)
					.BindAsync(
						getMeta
					)
					.MapAsync(
						createTuple,
						DefaultHandler
					);

				// Get query options
				QueryPostsMeta.Options getOptions() =>
					new() { PostIds = posts.Select(p => p.Id).ToList() };

				// Get query
				Option<IQuery<PostMeta>> getQuery(QueryPostsMeta.Options options) =>
					Return(
						() => StartNewQuery()
							.WithModel<PostMeta>()
							.WithOptions(options)
							.WithParts(new QueryPostsMeta.Builder<PostMeta>(db))
							.GetQuery(),
						e => new Msg.GetPostsMetaQueryExceptionMsg(e)
					);

				// Execute query to get meta
				Task<Option<List<PostMeta>>> getMeta(IQuery<PostMeta> query) =>
					query.ExecuteQueryAsync();

				// Create tuple of posts and meta
				(TList, List<PostMeta>) createTuple(List<PostMeta> meta) =>
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

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>An exception occured while adding meta to posts</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record AddMetaExceptionMsg<T>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>An exception occured while setting meta property on posts</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record SetMetaExceptionMsg<T>(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
