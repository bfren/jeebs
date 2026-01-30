// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Common;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <inheritdoc cref="ExecuteAsync{TModel}(IWpDb, IUnitOfWork, ulong, GetPostsOptions, IContentFilter[])"/>
	internal static Task<Result<IEnumerable<TModel>>> ExecuteAsync<TModel>(
		IWpDb db,
		IUnitOfWork w,
		GetPostsOptions opt,
		params IContentFilter[] filters
	)
		where TModel : IWithId<WpPostId, ulong> =>
		GetQueryParts<TModel>(
			db, opt
		)
		.BindAsync(
			x => db.QueryAsync<TModel>(x, w.Transaction)
		)
		.BindAsync(
			x => x.Count() switch
			{
				> 0 =>
					Process<IEnumerable<TModel>, TModel>(db, w, x, filters),

				_ =>
					R.Wrap(x).AsTask()
			}
		);

	/// <summary>
	/// Run a query and return multiple items with paging.
	/// </summary>
	/// <typeparam name="TModel">Return value type.</typeparam>
	/// <param name="db">IWpDb.</param>
	/// <param name="w">IUnitOfWork.</param>
	/// <param name="page">Page number.</param>
	/// <param name="opt">Function to return query options.</param>
	/// <param name="filters">Optional content filters to apply.</param>
	internal static Task<Result<IPagedList<TModel>>> ExecuteAsync<TModel>(
		IWpDb db,
		IUnitOfWork w,
		ulong page,
		GetPostsOptions opt,
		params IContentFilter[] filters
	)
		where TModel : IWithId<WpPostId, ulong> =>
		GetQueryParts<TModel>(
			db, opt
		)
		.BindAsync(
			x => db.QueryAsync<TModel>(page, x, w.Transaction)
		)
		.BindAsync(
			x => x switch
			{
				PagedList<TModel> when x.Count > 0 =>
					Process<IPagedList<TModel>, TModel>(db, w, x, filters),

				PagedList<TModel> =>
					R.Wrap(x).AsTask(),

				_ =>
					R.Fail("Unrecognised PagedList type.")
						.Ctx(nameof(QueryPostsF), nameof(ExecuteAsync))
						.AsTask<IPagedList<TModel>>()
			}
		);
}
