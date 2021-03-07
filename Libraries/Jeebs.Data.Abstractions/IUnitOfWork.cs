// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Unit of Work for interacting with a database
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// IAdapter
		/// </summary>
		IAdapter Adapter { get; }

		/// <summary>
		/// IDbConnection
		/// </summary>
		IDbConnection Connection { get; }

		/// <summary>
		/// IDbTransaction
		/// </summary>
		IDbTransaction Transaction { get; }

		/// <summary>
		/// Provider-agnostic query methods
		/// </summary>
		IQueryDriver Driver { get; }

		/// <summary>
		/// Shorthand for IAdapter.SplitAndEscape
		/// </summary>
		/// <param name="element">The element to split and escape</param>
		string Escape(object element);

		/// <summary>
		/// Shorthand for IAdapter.EscapeAndJoin
		/// </summary>
		/// <param name="elements">The elements to escape and join</param>
		string Escape(params string?[] elements);

		/// <summary>
		/// Commit all queries - should normally be called as part of Dispose()
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollback all queries
		/// </summary>
		void Rollback();

		#region R: Query

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="sql">Query string</param>
		/// <param name="parameters">Query parameters - should normally be an object with properties matching parameter names</param>
		/// <param name="commandType">CommandType</param>
		IR<IEnumerable<dynamic>> Query(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		IR<IEnumerable<dynamic>> Query(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		IR<IEnumerable<dynamic>> Query(IOk r, string sql);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		Task<IR<IEnumerable<dynamic>>> QueryAsync(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		Task<IR<IEnumerable<dynamic>>> QueryAsync(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		Task<IR<IEnumerable<dynamic>>> QueryAsync(IOk r, string sql);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		IR<IEnumerable<T>> Query<T>(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		IR<IEnumerable<T>> Query<T>(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		IR<IEnumerable<T>> Query<T>(IOk r, string sql);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		Task<IR<IEnumerable<T>>> QueryAsync<T>(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		Task<IR<IEnumerable<T>>> QueryAsync<T>(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		Task<IR<IEnumerable<T>>> QueryAsync<T>(IOk r, string sql);

		#endregion

		#region R: Single

		/// <summary>
		/// Return a single object
		/// </summary>
		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		IR<T> Single<T>(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="Single{T}(IOk, string, object?, CommandType)"/>
		IR<T> Single<T>(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="Single{T}(IOk, string, object?, CommandType)"/>
		IR<T> Single<T>(IOk r, string sql);

		/// <inheritdoc cref="Single{T}(IOk, string, object?, CommandType)"/>
		Task<IR<T>> SingleAsync<T>(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="Single{T}(IOk, string, object?, CommandType)"/>
		Task<IR<T>> SingleAsync<T>(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="Single{T}(IOk, string, object?, CommandType)"/>
		Task<IR<T>> SingleAsync<T>(IOk r, string sql);

		#endregion

		#region Execute

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		IR<int> Execute(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="Execute(IOk, string, object?, CommandType)"/>
		IR<int> Execute(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="Execute(IOk, string, object?, CommandType)"/>
		IR<int> Execute(IOk r, string sql);

		/// <inheritdoc cref="Execute(IOk, string, object?, CommandType)"/>
		Task<IR<int>> ExecuteAsync(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="Execute(IOk, string, object?, CommandType)"/>
		Task<IR<int>> ExecuteAsync(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="Execute(IOk, string, object?, CommandType)"/>
		Task<IR<int>> ExecuteAsync(IOk r, string sql);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <inheritdoc cref="Query(IOk, string, object?, CommandType)"/>
		IR<T> ExecuteScalar<T>(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="ExecuteScalar{T}(IOk, string, object?, CommandType)"/>
		IR<T> ExecuteScalar<T>(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="ExecuteScalar{T}(IOk, string, object?, CommandType)"/>
		IR<T> ExecuteScalar<T>(IOk r, string sql);

		/// <inheritdoc cref="ExecuteScalar{T}(IOk, string, object?, CommandType)"/>
		Task<IR<T>> ExecuteScalarAsync<T>(IOk r, string sql, object? parameters, CommandType commandType);

		/// <inheritdoc cref="ExecuteScalar{T}(IOk, string, object?, CommandType)"/>
		Task<IR<T>> ExecuteScalarAsync<T>(IOk r, string sql, object? parameters);

		/// <inheritdoc cref="ExecuteScalar{T}(IOk, string, object?, CommandType)"/>
		Task<IR<T>> ExecuteScalarAsync<T>(IOk r, string sql);

		#endregion
	}
}
