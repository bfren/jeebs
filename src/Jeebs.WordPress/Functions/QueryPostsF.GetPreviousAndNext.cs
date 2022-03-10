// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;
using Jeebs.WordPress.Entities.StrongIds;
using MaybeF.Internals;

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
			prev is Some<long> x ? new WpPostId(x.Value) : null,
			next is Some<long> y ? new WpPostId(y.Value) : null
		);
	}
}
