// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Functions;
using Jeebs.Data.Query;

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
			fOk: LogQuery<T>
		)
		.BindAsync(
			x => Client.Adapter.QueryAsync<T>(transaction, x.query, x.parameters, x.type)
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
		QueryAsync(
			page,
			parts,
			(query, param) => QuerySingleAsync<ulong>(query, param, CommandType.Text, transaction),
			(query, param) => QueryAsync<T>(query, param, CommandType.Text, transaction)
		);

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
