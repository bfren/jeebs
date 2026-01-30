// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Config.Db;
using Jeebs.Logging;

namespace Jeebs.Data;

/// <summary>
/// Enables agnostic interaction with a database.
/// </summary>
public interface IDb
{
	/// <summary>
	/// Database Client.
	/// </summary>
	IDbClient Client { get; }

	/// <summary>
	/// Configuration for this database connection.
	/// </summary>
	DbConnectionConfig Config { get; }

	/// <summary>
	/// ILog (should be given a context of the implementing class).
	/// </summary>
	ILog Log { get; }

	/// <summary>
	/// Run a query and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param);

	/// <summary>
	/// Run a query and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	Task<Result<T>> QuerySingleAsync<T>(string query, object? param);

	/// <summary>
	/// Run a query and return a single item.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	Task<Result<bool>> ExecuteAsync(string query, object? param);

	/// <summary>
	/// Execute a query and return a single value.
	/// </summary>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	Task<Result<TReturn>> ExecuteAsync<TReturn>(string query, object? param);
}
