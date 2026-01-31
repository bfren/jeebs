// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Data.Common;

namespace Jeebs.Data.Adapters.Dapper;

public sealed class DapperAdapter : IAdapter
{
	public ITypeMapper Mapper { get; init; } =
		DapperTypeMapper.Instance;

	/// <inheritdoc/>
	public Task<Result<int>> ExecuteAsync(IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => transaction.Connection!.ExecuteAsync(query, param, transaction, commandType: type),
			ex => R.Fail(ex).Msg("Error executing query '{Query}' with parameters {@Param}.", query, param)
		);

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => transaction.Connection!.ExecuteScalarAsync<T>(query, param, transaction, commandType: type),
			ex => R.Fail(ex).Msg("Error executing query '{Query}' with parameters {@Param}.", query, param)
		)
		.BindAsync(
			x => x switch
			{
				T =>
					R.Wrap(x),

				_ =>
					R.Fail("Execution returned null value.", query, param)
						.Ctx(nameof(Db), nameof(ExecuteAsync))
			}
		);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => transaction.Connection!.QueryAsync<T>(query, param, transaction, commandType: type),
			ex => R.Fail(ex).Msg("Error executing query '{Query}' with parameters {@Param}.", query, param)
		);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => transaction.Connection!.QuerySingleAsync<T>(query, param, transaction, commandType: type),
			ex => R.Fail(ex).Msg("Error executing query '{Query}' with parameters {@Param}.", query, param)
		);
}
