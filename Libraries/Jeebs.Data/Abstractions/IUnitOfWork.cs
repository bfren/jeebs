using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Unit of Work
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

		#region Logging & Failure

		/// <summary>
		/// Log a query
		/// </summary>
		/// <typeparam name="T">Parameter object type</typeparam>
		/// <param name="method">Calling method</param>
		/// <param name="query">SQL query</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public void LogQuery<T>(string method, string query, T parameters, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Query failure -
		///		Rollback
		///		Log Error
		///		Return Failure Result
		/// </summary>
		/// <param name="error">Error message</param>
		/// <param name="args">Error message arguments</param>
		public IResult<bool> Fail(string error, params object[] args);

		/// <summary>
		/// Query failure -
		///		Rollback
		///		Log Error
		///		Return Failure Result
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="error">Error message</param>
		/// <param name="args">Error message arguments</param>
		public IResult<bool> Fail(Exception ex, string error, params object[] args);

		/// <summary>
		/// Query failure -
		///		Rollback
		///		Log Error
		///		Return Failure Result
		/// </summary>
		/// <typeparam name="T">Return value type</typeparam>
		/// <param name="error">Error message</param>
		/// <param name="args">Error message arguments</param>
		public IResult<T> Fail<T>(string error, params object[] args);

		/// <summary>
		/// Query failure -
		///		Rollback
		///		Log Error
		///		Return Failure Result
		/// </summary>
		/// <typeparam name="T">Return value type</typeparam>
		/// <param name="ex">Exception</param>
		/// <param name="error">Error message</param>
		/// <param name="args">Error message arguments</param>
		public IResult<T> Fail<T>(Exception ex, string error, params object[] args);

		#endregion

		#region R: Query

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		IResult<IEnumerable<dynamic>> Query(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<IResult<IEnumerable<dynamic>>> QueryAsync(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		IResult<IEnumerable<T>> Query<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<IResult<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		#endregion

		#region R: Single

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		IResult<T> Single<T>(string query, object parameters, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<IResult<T>> SingleAsync<T>(string query, object parameters, CommandType commandType = CommandType.Text);

		#endregion

		#region Execute

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		IResult<int> Execute(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		Task<IResult<int>> ExecuteAsync(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		IResult<T> ExecuteScalar<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<IResult<T>> ExecuteScalarAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		#endregion
	}
}
