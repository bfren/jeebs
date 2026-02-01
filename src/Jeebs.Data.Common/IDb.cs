// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Query;

namespace Jeebs.Data.Common;

/// <summary>
/// Enables agnostic interaction with a database.
/// </summary>
public interface IDb : Data.IDb
{
	/// <summary>
	/// Database Client.
	/// </summary>
	new IDbClient Client { get; }

	/// <summary>
	/// Start a new Unit of Work.
	/// </summary>
	IUnitOfWork StartWork();

	/// <summary>
	/// Start a new Unit of Work asynchronously.
	/// </summary>
	Task<IUnitOfWork> StartWorkAsync();

	/// <inheritdoc cref="QueryAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type);

	/// <summary>
	/// Run a query and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">Command type.</param>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>List of matching items.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="QueryAsync{T}(ulong, IQueryParts, IDbTransaction)"/>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IDbTransaction transaction);

	/// <summary>
	/// Build a query from <see cref="IQueryParts"/> and return paged items.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="parts">Query parts.</param>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>Paged list of matching items.</returns>
	Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IDbTransaction transaction);

	/// <summary>
	/// Build a query using <see cref="IQueryBuilder"/> and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return model type.</typeparam>
	/// <param name="builder">Query builder.</param>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>List of matching items.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IDbTransaction transaction);

	/// <summary>
	/// Use a fluent <see cref="IQueryBuilder"/> to create a query to run against the database.
	/// </summary>
	/// <typeparam name="T">Return model type.</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="builder">Query builder.</param>
	/// <param name="transaction">Database transaction.</param>
	Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IDbTransaction transaction);

	/// <inheritdoc cref="QuerySingleAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type);

	/// <summary>
	/// Run a query and return a single item.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">Command type.</param>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>Single item.</returns>
	Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <summary>
	/// Build a query from <see cref="IQueryParts"/> and return a single item.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="parts">Query parts.</param>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>Single item.</returns>
	Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts, IDbTransaction transaction);

	/// <inheritdoc cref="ExecuteAsync(string, object?, CommandType, IDbTransaction)"/>
	Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type);

	/// <summary>
	/// Execute a query and return a single value.
	/// </summary>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">Command type.</param>
	/// <param name="transaction">Database transaction.</param>
	Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="ExecuteAsync{TReturn}(string, object?, CommandType, IDbTransaction)"/>
	Task<Result<TReturn>> ExecuteAsync<TReturn>(string query, object? param, CommandType type);

	/// <summary>
	/// Execute a query and return a single scalar value.
	/// </summary>
	/// <typeparam name="TReturn">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">Command type.</param>
	/// <param name="transaction">Database transaction.</param>
	Task<Result<TReturn>> ExecuteAsync<TReturn>(string query, object? param, CommandType type, IDbTransaction transaction);
}
