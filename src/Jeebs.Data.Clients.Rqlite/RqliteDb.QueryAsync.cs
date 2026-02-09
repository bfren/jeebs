// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Functions;
using Jeebs.Data.Query;
using Defaults = Jeebs.Collections.Defaults.PagingValues;

namespace Jeebs.Data.Clients.Rqlite;

public abstract partial class RqliteDb : Db
{
	/// <inheritdoc/>
	public override async Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param)
	{
		using var w = StartWork();
		return await w.QueryAsync<T>(query, param ?? new())
			.MapAsync(x => x.AsEnumerable());
	}

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts) =>
		Client.GetQuery(parts)
			.BindAsync(x => QueryAsync<T>(x.query, x.param));

	/// <inheritdoc/>
	public override Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts) =>
			from count in Client.GetCountQuery(parts)
			from countResults in ExecuteAsync<ulong>(
				count.query, count.param
			)
			from paging in R.Try(
				() => new PagingValues(countResults, page, parts.Maximum ?? Defaults.ItemsPer, Defaults.PagesPer),
				e => R.Fail(e).Msg("Error creating paging values.", countResults, page, parts.Maximum)
					.Ctx(GetType().Name, nameof(QueryAsync))
			)
			from items in Client.GetQuery(new QueryParts(parts) with
			{
				Skip = (paging.Page - 1) * paging.ItemsPer,
				Maximum = paging.ItemsPer
			})
			from itemsResults in QueryAsync<T>(
				items.query, items.param
			)
			select (IPagedList<T>)new PagedList<T>(paging, itemsResults);

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		DataF.BuildQuery<T>(builder)
			.BindAsync(QueryAsync<T>);

	/// <inheritdoc/>
	public override Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		DataF.BuildQuery<T>(builder)
			.BindAsync(x => QueryAsync<T>(page, x));
}
