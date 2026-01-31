// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Common;

namespace Jeebs.Data;

public interface IAdapter
{
	/// <summary>
	/// ITypeMapper.
	/// </summary>
	ITypeMapper Mapper { get; }

	/// <summary>
	/// Execute a database query and return the number of rows affected.
	/// </summary>
	/// <param name="transaction">IDbTransaction.</param>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">CommandType.</param>
	/// <returns>Query result.</returns>
	Task<Result<int>> ExecuteAsync(IDbTransaction transaction, string query, object? param, CommandType type);

	/// <summary>
	/// Execute a database query and return a single value.
	/// </summary>
	/// <param name="transaction">IDbTransaction.</param>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">CommandType.</param>
	/// <returns>Query result.</returns>
	Task<Result<T>> ExecuteAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);

	/// <summary>
	/// Execute a database query and return a list of items.
	/// </summary>
	/// <param name="transaction">IDbTransaction.</param>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">CommandType.</param>
	/// <returns>Query result.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);

	/// <summary>
	/// Execute a database query and return a single item.
	/// </summary>
	/// <param name="transaction">IDbTransaction.</param>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">CommandType.</param>
	/// <returns>Query result.</returns>
	Task<Result<T>> QuerySingleAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);
}
