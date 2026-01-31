// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Config.Db;
using Jeebs.Data.Query;
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
	/// <returns>List of matching items.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param);

	/// <summary>
	/// Build a query from <see cref="IQueryParts"/> and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="parts">Query parts.</param>
	/// <returns>List of matching items.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts);

	/// <summary>
	/// Build a query from <see cref="IQueryParts"/> and return paged items.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="parts">Query parts.</param>
	/// <returns>Paged list of matching items.</returns>
	Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts);

	/// <summary>
	/// Build a query using <see cref="IQueryBuilder"/> and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return model type.</typeparam>
	/// <param name="builder">Query builder.</param>
	/// <returns>List of matching items.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder);

	/// <summary>
	/// Build a query using <see cref="IQueryBuilder"/> and return paged items.
	/// </summary>
	/// <typeparam name="T">Return model type.</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="builder">Query builder.</param>
	/// <returns>Paged list of matching items.</returns>
	Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder);

	/// <summary>
	/// Run a query and return a single item.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <returns>Single item.</returns>
	Task<Result<T>> QuerySingleAsync<T>(string query, object? param);

	/// <summary>
	/// Build a query from <see cref="IQueryParts"/> and return a single item.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="parts">Query parts.</param>
	/// <returns>Single item.</returns>
	Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts);

	/// <summary>
	/// Build a query using <see cref="IQueryBuilder"/> and return a single item.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="builder">Query builder.</param>
	/// <returns>Single item.</returns>
	Task<Result<T>> QuerySingleAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder);

	/// <summary>
	/// Execute a query.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <returns>Whether or not the query was successful.</returns>
	Task<Result<bool>> ExecuteAsync(string query, object? param);

	/// <summary>
	/// Execute a query and return a value.
	/// </summary>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <returns>Query return value.</returns>
	Task<Result<TReturn>> ExecuteAsync<TReturn>(string query, object? param);
}
