﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Jeebs;
using Jeebs.WordPress.Data.Entities;

namespace F.WordPressF.DataF
{
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
}