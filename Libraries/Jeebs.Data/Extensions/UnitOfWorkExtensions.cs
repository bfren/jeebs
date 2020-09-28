using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

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
		/// <param name="this">Unit of Work</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static IR<IEnumerable<dynamic>> Query(this IUnitOfWork @this, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> @this.Query(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="this">Unit of Work</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static Task<IR<IEnumerable<dynamic>>> QueryAsync(this IUnitOfWork @this, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> @this.QueryAsync(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <param name="this">Unit of Work</param>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static IR<IEnumerable<T>> Query<T>(this IUnitOfWork @this, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> @this.Query<T>(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <param name="this">Unit of Work</param>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static Task<IR<IEnumerable<T>>> QueryAsync<T>(this IUnitOfWork @this, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> @this.QueryAsync<T>(Result.Ok(), query, parameters, commandType);

		#endregion

		#region R: Single

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="this">Unit of Work</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static IR<T> Single<T>(this IUnitOfWork @this, string query, object parameters, CommandType commandType = CommandType.Text)
			=> @this.Single<T>(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="this">Unit of Work</param>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static Task<IR<T>> SingleAsync<T>(this IUnitOfWork @this, string query, object parameters, CommandType commandType = CommandType.Text)
			=> @this.SingleAsync<T>(Result.Ok(), query, parameters, commandType);

		#endregion

		#region Execute

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="this">Unit of Work</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		public static IR<int> Execute(this IUnitOfWork @this, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> @this.Execute(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="this">Unit of Work</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		public static Task<IR<int>> ExecuteAsync(this IUnitOfWork @this, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> @this.ExecuteAsync(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="this">Unit of Work</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static IR<T> ExecuteScalar<T>(this IUnitOfWork @this, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> @this.ExecuteScalar<T>(Result.Ok(), query, parameters, commandType);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="this">Unit of Work</param>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		public static Task<IR<T>> ExecuteScalarAsync<T>(this IUnitOfWork @this, string query, object? parameters = null, CommandType commandType = CommandType.Text)
			=> @this.ExecuteScalarAsync<T>(Result.Ok(), query, parameters, commandType);

		#endregion
	}
}
