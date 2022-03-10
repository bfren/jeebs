// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Messages;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities.StrongIds;
using Maybe;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Apply the specified content filters to the list of posts
	/// </summary>
	/// <typeparam name="TList">List type</typeparam>
	/// <typeparam name="TModel">Post type</typeparam>
	/// <param name="posts">Posts</param>
	/// <param name="filters">Content Filters</param>
	internal static Maybe<TList> ApplyContentFilters<TList, TModel>(TList posts, IContentFilter[] filters)
		where TList : IEnumerable<TModel>
		where TModel : Id.IWithId<WpPostId>
	{
		// If there are no posts or filters, do nothing
		if (!posts.Any() || filters.Length == 0)
		{
			return posts;
		}

		// Post content field is required as we are expected to apply content filters
		return GetPostContentInfo<TModel>()
			.Map(
				x => ExecuteContentFilters(posts, x, filters),
				e => new M.ApplyContentFiltersExceptionMsg<TModel>(e)
			);
	}

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary>An exception occured while applying content filters to posts</summary>
		/// <typeparam name="T">Post Model type</typeparam>
		/// <param name="Value">Exception object</param>
		public sealed record class ApplyContentFiltersExceptionMsg<T>(Exception Value) : ExceptionMsg;
	}
}
