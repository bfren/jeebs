// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Linq;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Querying;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Get Previous and Next posts, if they exist, for the specified query options
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="currentId">Current Post ID</param>
		/// <param name="opt">Function to return query options</param>
		internal static Task<Option<(WpPostId? prev, WpPostId? next)>> GetPreviousAndNextAsync(
			IWpDb db,
			IUnitOfWork w,
			WpPostId currentId,
			GetPostsOptions opt
		)
		{
			return
				ExecuteAsync<PostWithId>(
					db, w, x => opt(x) with { Maximum = null }
				)
				.MapAsync(
					x => x.Select(p => p.Id.Value).ToList(),
					DefaultHandler
				)
				.MapAsync(
					x => GetPreviousAndNext(currentId.Value, x),
					e => new Msg.ErrorWhileGettingPreviousAndNextPostsMsg(e)
				);
		}

		private record PostWithId : WpPostEntityWithId;

		public static partial class Msg
		{
			/// <summary>Error while calculating previous and next posts</summary>
			/// <param name="Exception">Exception</param>
			public sealed record ErrorWhileGettingPreviousAndNextPostsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
