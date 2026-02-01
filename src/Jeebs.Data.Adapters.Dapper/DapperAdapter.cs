// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Data.Common;

namespace Jeebs.Data.Adapters.Dapper;

/// <summary>
/// Dapper database adapter.
/// </summary>
public sealed class DapperAdapter : IAdapter
{
	/// <inheritdoc/>
	public ITypeMapper Mapper { get; init; }

	/// <summary>
	/// Return a default instance of the adapter.
	/// </summary>
	public static DapperAdapter DefaultInstance =>
		new(DapperTypeMapper.Instance);

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="mapper">ITypeMapper.</param>
	public DapperAdapter(ITypeMapper mapper) =>
		Mapper = mapper;

	/// <inheritdoc/>
	public Task<Result<int>> ExecuteAsync(IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => transaction.Connection!.ExecuteAsync(query, param, transaction, commandType: type),
			ex => Fail(nameof(ExecuteAsync), ex, query, param)
		);

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => transaction.Connection!.ExecuteScalarAsync<T>(query, param, transaction, commandType: type),
			ex => Fail(nameof(ExecuteAsync), ex, query, param)
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
			ex => Fail(nameof(QueryAsync), ex, query, param)
		);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => transaction.Connection!.QuerySingleAsync<T>(query, param, transaction, commandType: type),
			ex => Fail(nameof(QuerySingleAsync), ex, query, param)
		);

	private static Failure Fail(string function, Exception exception, string query, object? param) =>
		R.Fail(exception)
			.Msg("Error executing query '{Query}' with parameters {@Param}.", query, param)
			.Ctx(nameof(DapperAdapter), function);
}
