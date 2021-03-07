// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Provides test-friendly abstraction of Dapper query extension methods
	/// </summary>
	public interface IQueryDriver
	{
		/// <summary>
		/// Return a sequence of dynamic objects with properties matching the columns.
		/// </summary>
		/// <param name="cnn">The connection to query on.</param>
		/// <param name="sql">The SQL to execute for the query.</param>
		/// <param name="param">The parameters to pass, if any.</param>
		/// <param name="transaction">The transaction to use.</param>
		/// <param name="commandType">The type of command to execute.</param>
		/// <remarks>Note: each row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
		IEnumerable<dynamic> Query(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Execute a query asynchronously using Task.
		/// </summary>
		/// <param name="cnn">The connection to query on.</param>
		/// <param name="sql">The SQL to execute for the query.</param>
		/// <param name="param">The parameters to pass, if any.</param>
		/// <param name="transaction">The transaction to use.</param>
		/// <param name="commandType">The type of command to execute.</param>
		/// <remarks>Note: each row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
		Task<IEnumerable<dynamic>> QueryAsync(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Executes a query, returning the data typed as <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of results to return.</typeparam>
		/// <param name="cnn">The connection to query on.</param>
		/// <param name="sql">The SQL to execute for the query.</param>
		/// <param name="param">The parameters to pass, if any.</param>
		/// <param name="transaction">The transaction to use.</param>
		/// <param name="commandType">The type of command to execute.</param>
		IEnumerable<T> Query<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Execute a query asynchronously using Task.
		/// </summary>
		/// <typeparam name="T">The type of results to return.</typeparam>
		/// <param name="cnn">The connection to query on.</param>
		/// <param name="sql">The SQL to execute for the query.</param>
		/// <param name="param">The parameters to pass, if any.</param>
		/// <param name="transaction">The transaction to use.</param>
		/// <param name="commandType">The type of command to execute.</param>
		Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Executes a single-row query, returning the data typed as <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of result to return.</typeparam>
		/// <param name="cnn">The connection to query on.</param>
		/// <param name="sql">The SQL to execute for the query.</param>
		/// <param name="param">The parameters to pass, if any.</param>
		/// <param name="transaction">The transaction to use.</param>
		/// <param name="commandType">The type of command to execute.</param>
		T QuerySingle<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Execute a single-row query asynchronously using Task.
		/// </summary>
		/// <typeparam name="T">The type of result to return.</typeparam>
		/// <param name="cnn">The connection to query on.</param>
		/// <param name="sql">The SQL to execute for the query.</param>
		/// <param name="param">The parameters to pass, if any.</param>
		/// <param name="transaction">The transaction to use.</param>
		/// <param name="commandType">The type of command to execute.</param>
		Task<T> QuerySingleAsync<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Execute parameterized SQL.
		/// </summary>
		/// <param name="cnn">The connection to query on.</param>
		/// <param name="sql">The SQL to execute for this query.</param>
		/// <param name="param">The parameters to use for this query.</param>
		/// <param name="transaction">The transaction to use for this query.</param>
		/// <param name="commandType">Is it a stored proc or a batch?</param>
		/// <returns>The number of rows affected.</returns>
		int Execute(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Execute a command asynchronously using Task.
		/// </summary>
		/// <param name="cnn">The connection to query on.</param>
		/// <param name="sql">The SQL to execute for this query.</param>
		/// <param name="param">The parameters to use for this query.</param>
		/// <param name="transaction">The transaction to use for this query.</param>
		/// <param name="commandType">Is it a stored proc or a batch?</param>
		/// <returns>The number of rows affected.</returns>
		Task<int> ExecuteAsync(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Execute parameterized SQL that selects a single value.
		/// </summary>
		/// <typeparam name="T">The type to return.</typeparam>
		/// <param name="cnn">The connection to execute on.</param>
		/// <param name="sql">The SQL to execute.</param>
		/// <param name="param">The parameters to use for this command.</param>
		/// <param name="transaction">The transaction to use for this command.</param>
		/// <param name="commandType">Is it a stored proc or a batch?</param>
		T ExecuteScalar<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Execute a command that selects a single value asynchronously using Task.
		/// </summary>
		/// <typeparam name="T">The type to return.</typeparam>
		/// <param name="cnn">The connection to execute on.</param>
		/// <param name="sql">The SQL to execute.</param>
		/// <param name="param">The parameters to use for this command.</param>
		/// <param name="transaction">The transaction to use for this command.</param>
		/// <param name="commandType">Is it a stored proc or a batch?</param>
		Task<T> ExecuteScalarAsync<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType);
	}
}
