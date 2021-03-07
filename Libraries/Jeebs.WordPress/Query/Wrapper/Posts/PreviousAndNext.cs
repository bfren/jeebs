// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Find the IDs of the previous and next posts given the current search query
		/// </summary>
		/// <param name="r">Result - value should be the current ID</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		public async Task<IR<(long? prev, long? next)>> QueryPostsPreviousAndNextAsync(IOkV<long> r, Action<QueryPosts.Options>? modify = null)
		{
			// Get query
			var query = GetPostsQuery<PostWithId>(opt =>
			{
				modify?.Invoke(opt);
				opt.Limit = null;
			});

			// Shorthand for returning result
			IR<(long? prev, long? next)> val(long? prev, long? next) =>
				r.OkV((prev, next));

			// Handle scenarios
			IR<(long? prev, long? next)> handle(long currentId, List<long> ids)
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

			// Execute query
			return await query.ExecuteQueryAsync(r).ConfigureAwait(false) switch
			{
				IOkV<List<PostWithId>> ids =>
					handle(r.Value, ids.Value.ConvertAll(x => x.PostId)),

				{ } x =>
					x.Error<(long?, long?)>()
			};
		}

		private record PostWithId()
		{
			public long PostId { get; init; }
		}
	}
}
