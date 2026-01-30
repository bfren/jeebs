// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Collections;
using Jeebs.Data.QueryBuilder;
using Defaults = Jeebs.Collections.Defaults.PagingValues;

namespace Jeebs.Data.Common;

public abstract partial class Db
{
	#region Query + Param

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param) =>
		QueryAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type)
	{
		await using var w = await StartWorkAsync();
		return await QueryAsync<T>(query, param, type, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			ok: LogQuery<T>
		)
		.MapAsync(
			x => transaction.Connection!.QueryAsync<T>(x.query, x.parameters, transaction, commandType: x.type)
		);

	#endregion

	#region Parts

	/// <inheritdoc/>
	public override async Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts)
	{
		await using var w = await StartWorkAsync();
		return await QueryAsync<T>(parts, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IDbTransaction transaction) =>
		from q in Client.GetQuery(parts)
		from r in QueryAsync<T>(q.query, q.param, CommandType.Text, transaction)
		select r;

	/// <inheritdoc/>
	public override async Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts)
	{
		await using var w = await StartWorkAsync();
		return await QueryAsync<T>(page, parts, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IDbTransaction transaction) =>
		from count in Client.GetCountQuery(parts)
		from countResults in ExecuteAsync<ulong>(
			count.query, count.param, CommandType.Text, transaction
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
			items.query, items.param, CommandType.Text, transaction
		)
		select (IPagedList<T>)new PagedList<T>(paging, itemsResults);

	#endregion

	#region Builder

	/// <inheritdoc/>
	public override async Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder)
	{
		await using var w = await StartWorkAsync();
		return await QueryAsync<T>(builder, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IDbTransaction transaction) =>
		DataF.BuildQuery<T>(
			builder
		)
		.BindAsync(
			x => QueryAsync<T>(x, transaction)
		);

	/// <inheritdoc/>
	public override async Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder)
	{
		await using var w = await StartWorkAsync();
		return await QueryAsync<T>(page, builder, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IDbTransaction transaction) =>
		DataF.BuildQuery<T>(
			builder
		)
		.BindAsync(
			x => QueryAsync<T>(page, x, transaction)
		);

	#endregion
}
