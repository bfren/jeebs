﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;

namespace Jeebs.Data
{
	/// <summary>
	/// Enables agnostic interaction with a database
	/// </summary>
	public interface IDb
	{
		/// <summary>
		/// Database Client
		/// </summary>
		IDbClient Client { get; }

		/// <summary>
		/// Start a new unit of work
		/// </summary>
		IUnitOfWork UnitOfWork { get; }

		/// <inheritdoc cref="QueryAsync{T}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type);

		/// <summary>
		/// Run a query and return multiple items
		/// </summary>
		/// <typeparam name="T">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		/// <param name="transaction">Database transaction</param>
		Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

		/// <inheritdoc cref="QuerySingleAsync{T}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<T>> QuerySingleAsync<T>(string query, object? param, CommandType type);

		/// <summary>
		/// Run a query and return a single item
		/// </summary>
		/// <typeparam name="T">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		/// <param name="transaction">Database transaction</param>
		Task<Option<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

		/// <inheritdoc cref="ExecuteAsync(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<bool>> ExecuteAsync(string query, object? param, CommandType type);

		/// <summary>
		/// Execute a query and return a single value
		/// </summary>
		/// <param name="query">Query text</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		/// <param name="transaction">Database transaction</param>
		Task<Option<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction);

		/// <inheritdoc cref="ExecuteAsync{TReturn}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<TReturn>> ExecuteAsync<TReturn>(string query, object? param, CommandType type);

		/// <summary>
		/// Execute a query and return a single scalar value
		/// </summary>
		/// <typeparam name="TReturn">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		/// <param name="transaction">Database transaction</param>
		Task<Option<TReturn>> ExecuteAsync<TReturn>(string query, object? param, CommandType type, IDbTransaction transaction);
	}
}
