using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
		/// Send a simple message to the debug log
		/// </summary>
		void LogDebug<T>(T message) where T : IMessage;

		/// <summary>
		/// Log a query
		/// </summary>
		/// <typeparam name="T">Parameter object type</typeparam>
		/// <param name="method">Calling method</param>
		/// <param name="query">SQL query</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		void LogQuery<T>(string method, string query, T parameters, CommandType commandType = CommandType.Text);

		/// <summary>
		/// General failure -
		///	<para>	Rollback</para>
		///	<para>	Return Error Result</para>
		/// </summary>
		/// <typeparam name="T">Result value type</typeparam>
		/// <typeparam name="TMessage">Message type</typeparam>
		/// <param name="r">Result</param>
		//Error<T> Fail<T, TMessage>(Ok<dynamic> r) where TMessage : IMessage, new();

		/// <summary>
		/// General failure -
		///	<para>	Rollback</para>
		///	<para>	Log Error</para>
		///	<para>	Return Error Result</para>
		/// </summary>
		/// <typeparam name="T">Result value type</typeparam>
		/// <typeparam name="TMessage">Message type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="ex">Exception</param>
		//Error<T> Fail<T, TMessage>(Ok<dynamic> r, Exception ex) where TMessage : IMessage, new();

		/// <summary>
		/// Query failure -
		///	<para>	Rollback</para>
		///	<para>	Log Error</para>
		///	<para>	Return Error Result</para>
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="ex">Exception</param>
		/// <param name="query">Query being executed</param>
		/// <param name="parameters">Query parameters</param>
		//Error<T> Fail<T>(Ok<dynamic> r, Exception ex, string query, object? parameters = null);

		#endregion

		#region R: Query

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		IR<IEnumerable<dynamic>> Query(IOk<dynamic> r, string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<IR<IEnumerable<dynamic>>> QueryAsync(IOk<dynamic> r, string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		IR<IEnumerable<T>> Query<T>(IOk<dynamic> r, string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<IR<IEnumerable<T>>> QueryAsync<T>(IOk<dynamic> r, string query, object? parameters = null, CommandType commandType = CommandType.Text);

		#endregion

		#region R: Single

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		IR<T> Single<T>(IOk<dynamic> r, string query, object parameters, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<IR<T>> SingleAsync<T>(IOk<dynamic> r, string query, object parameters, CommandType commandType = CommandType.Text);

		#endregion

		#region Execute

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		IR<int> Execute(IOk<dynamic> r, string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		Task<IR<int>> ExecuteAsync(IOk<dynamic> r, string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		IR<T> ExecuteScalar<T>(IOk<dynamic> r, string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<IR<T>> ExecuteScalarAsync<T>(IOk<dynamic> r, string query, object? parameters = null, CommandType commandType = CommandType.Text);

		#endregion
	}
}
