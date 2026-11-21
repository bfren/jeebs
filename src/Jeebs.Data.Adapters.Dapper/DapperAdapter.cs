// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
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
	/// [Optional] Caching service.
	/// </summary>
	private ICacheService Cache { get; init; }

	/// <summary>
	/// Return a default instance of the adapter.
	/// </summary>
	public static DapperAdapter DefaultInstance =>
		new();

	/// <summary>
	/// Inject dependencies - DI uses the default for whichever is not registered.
	/// </summary>
	/// <param name="mapper">[Optional] ITypeMapper - defaults to <see cref="DapperTypeMapper.Instance"/>.</param>
	/// <param name="cache">[Optional] ICacheService - defaults to no cache.</param>
	public DapperAdapter(ITypeMapper? mapper = null, ICacheService? cache = null) =>
		(Mapper, Cache) = (mapper ?? DapperTypeMapper.Instance, cache ?? new NullCacheService());

	/// <inheritdoc/>
	public Task<Result<int>> ExecuteAsync(IDbConnection connection, IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => connection.ExecuteAsync(query, param, transaction, commandType: type),
			ex => Fail(nameof(ExecuteAsync), ex, query, param)
		);

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(IDbConnection connection, IDbTransaction transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => connection.ExecuteScalarAsync<T>(query, param, transaction, commandType: type),
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
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(IDbConnection connection, IDbTransaction? transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => Cache.GetOrCreateAsync(CacheKey(connection, query), param,
				() => connection.QueryAsync<T>(query, param, transaction, commandType: type)
			),
			ex => Fail(nameof(QueryAsync), ex, query, param)
		);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(IDbConnection connection, IDbTransaction? transaction, string query, object? param, CommandType type) =>
		R.TryAsync(
			() => Cache.GetOrCreateAsync(CacheKey(connection, query), param,
				() => connection.QuerySingleAsync<T>(query, param, transaction, commandType: type)
			),
			ex => Fail(nameof(QuerySingleAsync), ex, query, param)
		);

	/// <summary>
	/// Prefix <paramref name="query"/> with a hash of the connection string, so databases sharing a
	/// cache don't share entries - hashed so passwords never appear in a cache key.
	/// </summary>
	/// <param name="connection">Database connection.</param>
	/// <param name="query">Query text.</param>
	internal static string CacheKey(IDbConnection connection, string query) =>
		$"{Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(connection.ConnectionString)))}:{query}";

	private static Failure Fail(string function, Exception exception, string query, object? param) =>
		R.Fail(exception)
			// parameters are left out because they can hold secrets (e.g. password hashes) - they are
			// logged at Verbose before the query runs
			.Msg("Error executing query '{Query}'.", query)
			.Ctx(nameof(DapperAdapter), function);
}
