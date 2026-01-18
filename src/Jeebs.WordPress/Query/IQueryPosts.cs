// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities.StrongIds;
using StrongId;

namespace Jeebs.WordPress.Query;

/// <summary>
/// Query Posts - to enable testing of static functions.
/// </summary>
public interface IQueryPosts
{
	/// <inheritdoc cref="ExecuteAsync{T}(IWpDb, IUnitOfWork, ulong, GetPostsOptions, IContentFilter[])"/>
	Task<Maybe<IEnumerable<T>>> ExecuteAsync<T>(
		IWpDb db,
		IUnitOfWork w,
		GetPostsOptions opt,
		params IContentFilter[] filters
	)
		where T : IWithId<WpPostId>;

	/// <summary>
	/// Run a query and return multiple items with paging
	/// </summary>
	/// <typeparam name="T">Return value type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="page">Page number</param>
	/// <param name="opt">Function to return query options</param>
	/// <param name="filters">Optional content filters to apply</param>
	Task<Maybe<IPagedList<T>>> ExecuteAsync<T>(
		IWpDb db,
		IUnitOfWork w,
		ulong page,
		GetPostsOptions opt,
		params IContentFilter[] filters
	)
		where T : IWithId<WpPostId>;
}
