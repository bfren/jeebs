// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get Previous and Next IDs from a list of IDs
	/// </summary>
	/// <param name="currentId">Current Post ID</param>
	/// <param name="ids">List of IDs</param>
	internal static (WpPostId? prev, WpPostId? next) GetPreviousAndNext(long currentId, List<long> ids)
	{
		var (prev, next) = ids.GetEitherSide(currentId);

		return (
			prev.IsSome(out var x) ? new WpPostId { Value = x } : null,
			next.IsSome(out var y) ? new WpPostId { Value = y } : null
		);
	}
}
