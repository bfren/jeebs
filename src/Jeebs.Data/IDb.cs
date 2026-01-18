// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Config.Db;

namespace Jeebs.Data;

/// <summary>
/// Enables agnostic interaction with a database.
/// </summary>
public interface IDb
{
	/// <summary>
	/// Database Client
	/// </summary>
	IDbClient Client { get; }

	/// <summary>
	/// Configuration for this database connection
	/// </summary>
	DbConnectionConfig Config { get; }

	/// <summary>
	/// Start a new Unit of Work
	/// </summary>
	IUnitOfWork StartWork();

	/// <summary>
	/// Start a new Unit of Work asynchronously
	/// </summary>
	Task<IUnitOfWork> StartWorkAsync();

	/// <inheritdoc cref="QueryAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type);

	/// <summary>
	/// Run a query and return multiple items
	/// </summary>
	/// <typeparam name="T">Return value type</typeparam>
	/// <param name="query">Query text</param>
	/// <param name="param">Query parameters</param>
	/// <param name="type">Command type</param>
	/// <param name="transaction">Database transaction</param>
	Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="QuerySingleAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, CommandType type);

	/// <summary>
	/// Run a query and return a single item
	/// </summary>
	/// <typeparam name="T">Return value type</typeparam>
	/// <param name="query">Query text</param>
	/// <param name="param">Query parameters</param>
	/// <param name="type">Command type</param>
	/// <param name="transaction">Database transaction</param>
	Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="ExecuteAsync(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<bool>> ExecuteAsync(string query, object? param, CommandType type);

	/// <summary>
	/// Execute a query and return a single value
	/// </summary>
	/// <param name="query">Query text</param>
	/// <param name="param">Query parameters</param>
	/// <param name="type">Command type</param>
	/// <param name="transaction">Database transaction</param>
	Task<Maybe<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="ExecuteAsync{TReturn}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<TReturn>> ExecuteAsync<TReturn>(string query, object? param, CommandType type);

	/// <summary>
	/// Execute a query and return a single scalar value
	/// </summary>
	/// <typeparam name="TReturn">Return value type</typeparam>
	/// <param name="query">Query text</param>
	/// <param name="param">Query parameters</param>
	/// <param name="type">Command type</param>
	/// <param name="transaction">Database transaction</param>
	Task<Maybe<TReturn>> ExecuteAsync<TReturn>(string query, object? param, CommandType type, IDbTransaction transaction);
}
