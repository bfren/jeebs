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
	/// Run <paramref name="work"/> in a new Unit of Work, and commit it if <paramref name="work"/> succeeds.
	/// If it fails or throws, nothing is committed: the Unit of Work rolls back when it is disposed.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="work">Queries to run - pass the Unit of Work to each one.</param>
	/// <returns>The result of <paramref name="work"/>, or a failure if the commit fails.</returns>
	Task<Result<T>> DoWorkAsync<T>(Func<IUnitOfWork, Task<Result<T>>> work);

	/// <inheritdoc cref="QueryAsync{T}(string, object?, CommandType, IUnitOfWork)"/>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type);

	/// <summary>
	/// Run a query and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">Command type.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	/// <returns>List of matching items.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IUnitOfWork unitOfWork);

	/// <inheritdoc cref="QueryAsync{T}(ulong, IQueryParts, IUnitOfWork)"/>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IUnitOfWork unitOfWork);

	/// <summary>
	/// Build a query from <see cref="IQueryParts"/> and return paged items.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="parts">Query parts.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	/// <returns>Paged list of matching items.</returns>
	Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IUnitOfWork unitOfWork);

	/// <summary>
	/// Build a query using <see cref="IQueryBuilder"/> and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return model type.</typeparam>
	/// <param name="builder">Query builder.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	/// <returns>List of matching items.</returns>
	Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IUnitOfWork unitOfWork);

	/// <summary>
	/// Use a fluent <see cref="IQueryBuilder"/> to create a query to run against the database.
	/// </summary>
	/// <typeparam name="T">Return model type.</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="builder">Query builder.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IUnitOfWork unitOfWork);

	/// <inheritdoc cref="QuerySingleAsync{T}(string, object?, CommandType, IUnitOfWork)"/>
	Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type);

	/// <summary>
	/// Run a query and return a single item.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">Command type.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	/// <returns>Single item.</returns>
	Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IUnitOfWork unitOfWork);

	/// <summary>
	/// Build a query from <see cref="IQueryParts"/> and return a single item.
	/// </summary>
	/// <typeparam name="T">Return value type.</typeparam>
	/// <param name="parts">Query parts.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	/// <returns>Single item.</returns>
	Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts, IUnitOfWork unitOfWork);

	/// <inheritdoc cref="ExecuteAsync(string, object?, CommandType, IUnitOfWork)"/>
	Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type);

	/// <summary>
	/// Execute a query and return a single value.
	/// </summary>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">Command type.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type, IUnitOfWork unitOfWork);

	/// <inheritdoc cref="InsertAsync{TReturn}(string, object?, CommandType, IUnitOfWork)"/>
	Task<Result<TId>> InsertAsync<TId>(string query, object? param, CommandType type);

	/// <summary>
	/// Insert a record into the database and return the ID.
	/// </summary>
	/// <typeparam name="TId">ID value type.</typeparam>
	/// <param name="query">Query text.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="type">Command type.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	Task<Result<TId>> InsertAsync<TId>(string query, object? param, CommandType type, IUnitOfWork unitOfWork);
}
