// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Data.QueryBuilder;

namespace Jeebs.Data.Common;

public abstract partial class Db
{
	#region Query + Param

	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(string query, object? param) =>
		QuerySingleAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public async Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type)
	{
		await using var w = await StartWorkAsync();
		return await QuerySingleAsync<T>(query, param, type, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			ok: LogQuery<T>
		)
		.MapAsync(
			x => transaction.Connection!.QuerySingleOrDefaultAsync<T>(x.query, x.parameters, transaction, commandType: x.type)
		)
		.BindAsync(
			x => x switch
			{
				T =>
					R.Wrap(x),

				_ =>
					R.Fail("Item not found or multiple items returned.", query, param)
						.Ctx(nameof(Db), nameof(QuerySingleAsync))
			}
		);

	#endregion

	#region Parts

	/// <inheritdoc/>
	public override async Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts)
	{
		await using var w = await StartWorkAsync();
		return await QuerySingleAsync<T>(parts, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts, IDbTransaction transaction) =>
		from q in Client.GetQuery(parts)
		from r in QuerySingleAsync<T>(q.query, q.param, CommandType.Text, transaction)
		select r;

	#endregion

	#region Builder

	/// <inheritdoc/>
	public override async Task<Result<T>> QuerySingleAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder)
	{
		await using var w = await StartWorkAsync();
		return await QuerySingleAsync<T>(builder, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IDbTransaction transaction) =>
		DataF.BuildQuery<T>(
			builder
		)
		.BindAsync(
			x => QuerySingleAsync<T>(x, transaction)
		);

	#endregion
}
