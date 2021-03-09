// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data.Querying;
using Jm.WordPress.Query.Wrapper.Posts;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Find the IDs of the previous and next posts given the current search query
		/// </summary>
		/// <param name="postId">Post ID</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		public async Task<Option<(long? prev, long? next)>> QueryPostsPreviousAndNextAsync(long postId, Action<QueryPosts.Options>? modify = null)
		{
			return await Option.True
				.Map(
					getQuery,
					e => new GetPostsQueryExceptionMsg(e)
				)
				.BindAsync(
					getPosts // exceptions handled by query code
				)
				.BindAsync(
					x => handle(postId, x.ConvertAll(x => x.PostId)),
					e => new CalculatePreviousAndNextExceptionMsg(e)
				);

			// Get query
			IQuery<PostWithId> getQuery() =>
				GetPostsQuery<PostWithId>(opt =>
				{
					modify?.Invoke(opt);
					opt.Limit = null;
				});

			// Get posts
			async Task<Option<List<PostWithId>>> getPosts(IQuery<PostWithId> query) =>
				await query.ExecuteQueryAsync();

			// Shorthand for returning result
			Option<(long? prev, long? next)> val(long? prev, long? next) =>
				(prev, next);

			// Handle scenarios
			Option<(long? prev, long? next)> handle(long currentId, List<long> ids)
			{
				// If there are no posts, or only one post, or the current post is not in the list, return null
				if (ids.Count <= 1 || !ids.Contains(currentId))
				{
					return val(null, null);
				}

				// Get the index of the current ID
				var index = ids.IndexOf(currentId);

				// If it is the first ID, Previous should be null
				if (index == 0)
				{
					return val(null, ids[1]);
				}
				// If it is the last ID, Next should be null
				else if (index == ids.Count - 1)
				{
					return val(ids[index - 1], null);
				}
				// Return the IDs either side of the current ID
				else
				{
					return val(ids[index - 1], ids[index + 1]);
				}
			}
		}

		private record PostWithId()
		{
			public long PostId { get; init; }
		}
	}
}
