// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Common;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Functions;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryPosts"/>
public sealed class Posts : IQueryPosts
{
	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> ExecuteAsync<T>(
		IWpDb db,
		IUnitOfWork w,
		GetPostsOptions opt,
		params IContentFilter[] filters
	)
		where T : IWithId<WpPostId, ulong> =>
		QueryPostsF.ExecuteAsync<T>(db, w, opt, filters);

	/// <inheritdoc/>
	public Task<Result<IPagedList<T>>> ExecuteAsync<T>(
		IWpDb db,
		IUnitOfWork w,
		ulong page,
		GetPostsOptions opt,
		params IContentFilter[] filters
	)
		where T : IWithId<WpPostId, ulong> =>
		QueryPostsF.ExecuteAsync<T>(db, w, page, opt, filters);
}
