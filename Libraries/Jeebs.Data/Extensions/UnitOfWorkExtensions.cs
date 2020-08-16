using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Jeebs;

namespace Jeebs.Data
{
	/// <summary>
	/// <see cref="IUnitOfWork"/> Extension Methods
	/// </summary>
	public static class UnitOfWorkExtensions
	{
		#region R: Query

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="w">Unit of Work</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static IR<IEnumerable<dynamic>> Query(this IUnitOfWork w, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> w.Query(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="w">Unit of Work</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static Task<IR<IEnumerable<dynamic>>> QueryAsync(this IUnitOfWork w, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> w.QueryAsync(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <param name="w">Unit of Work</param>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static IR<IEnumerable<T>> Query<T>(this IUnitOfWork w, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> w.Query<T>(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <param name="w">Unit of Work</param>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static Task<IR<IEnumerable<T>>> QueryAsync<T>(this IUnitOfWork w, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> w.QueryAsync<T>(Result.Ok(), query, parameters, commandType);

		#endregion

		#region R: Single

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="w">Unit of Work</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static IR<T> Single<T>(this IUnitOfWork w, string query, object parameters, CommandType commandType = CommandType.Text)
			=> w.Single<T>(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="w">Unit of Work</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static Task<IR<T>> SingleAsync<T>(this IUnitOfWork w, string query, object parameters, CommandType commandType = CommandType.Text)
			=> w.SingleAsync<T>(Result.Ok(), query, parameters, commandType);

		#endregion

		#region Execute

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="w">Unit of Work</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		public static IR<int> Execute(this IUnitOfWork w, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> w.Execute(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="w">Unit of Work</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		public static Task<IR<int>> ExecuteAsync(this IUnitOfWork w, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> w.ExecuteAsync(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="w">Unit of Work</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static IR<T> ExecuteScalar<T>(this IUnitOfWork w, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> w.ExecuteScalar<T>(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="w">Unit of Work</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static Task<IR<T>> ExecuteScalarAsync<T>(this IUnitOfWork w, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> w.ExecuteScalarAsync<T>(Result.Ok(), query, parameters, commandType);

		#endregion
	}
}
