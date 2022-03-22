// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Messages;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get Previous and Next posts, if they exist, for the specified query options
	/// </summary>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="currentId">Current Post ID</param>
	/// <param name="opt">Function to return query options</param>
	internal static Task<Maybe<(WpPostId? prev, WpPostId? next)>> GetPreviousAndNextAsync(
		IWpDb db,
		IUnitOfWork w,
		WpPostId currentId,
		GetPostsOptions opt
	) =>
		ExecuteAsync<PostWithId>(
			db, w, x => opt(x) with { Maximum = null }
		)
		.MapAsync(
			x => x.Select(p => p.Id.Value).ToList(),
			F.DefaultHandler
		)
		.MapAsync(
			x => GetPreviousAndNext(currentId.Value, x),
			e => new M.ErrorWhileGettingPreviousAndNextPostsMsg(e)
		);

	private sealed record class PostWithId : WpPostEntityWithId;

	public static partial class M
	{
		/// <summary>Error while calculating previous and next posts</summary>
		/// <param name="Value">Exception</param>
		public sealed record class ErrorWhileGettingPreviousAndNextPostsMsg(Exception Value) : ExceptionMsg;
	}
}
