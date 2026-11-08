// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

/// <summary>
/// ADO.NET compatible database adapter for querying and mapping to result objects.
/// </summary>
public interface IAdapter
{
	/// <summary>
	/// ITypeMapper.
	/// </summary>
	ITypeMapper Mapper { get; }

	/// <summary>
	/// Execute a database query and return the number of rows affected.
	/// </summary>
	/// <param name="connection">Open database connection.</param>
	/// <param name="transaction">Database transaction.</param>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">CommandType.</param>
	/// <returns>Query result.</returns>
	Task<Result<int>> ExecuteAsync(IDbConnection connection, IDbTransaction transaction, string query, object? param, CommandType type);

	/// <summary>
	/// Execute a database query and return a single value.
	/// </summary>
	/// <param name="connection">Open database connection.</param>
	/// <param name="transaction">Database transaction.</param>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">CommandType.</param>
	/// <returns>Query result.</returns>
	Task<Result<T>> ExecuteAsync<T>(IDbConnection connection, IDbTransaction transaction, string query, object? param, CommandType type);

	/// <summary>
	/// Execute a database query and return a list of items.
	/// </summary>
	/// <param name="connection">Open database connection.</param>
	/// <param name="transaction">[Optional] Database transaction - null to run without one.</param>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">CommandType.</param>
	/// <returns>Query result.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(IDbConnection connection, IDbTransaction? transaction, string query, object? param, CommandType type);

	/// <summary>
	/// Execute a database query and return a single item.
	/// </summary>
	/// <param name="connection">Open database connection.</param>
	/// <param name="transaction">[Optional] Database transaction - null to run without one.</param>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">CommandType.</param>
	/// <returns>Query result.</returns>
	Task<Result<T>> QuerySingleAsync<T>(IDbConnection connection, IDbTransaction? transaction, string query, object? param, CommandType type);
}
