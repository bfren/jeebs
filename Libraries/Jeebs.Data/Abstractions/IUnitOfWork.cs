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
		/// Shorthand for Table[].ExtractColumns and then IAdapter.Join
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		string Extract<T>(params Table[] tables);

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

		#region C

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>Inserted entity, with actual ID</returns>
		Result<T> Insert<T>(T poco) where T : class, IEntity;

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>Entity (complete with new ID)</returns>
		public Task<Result<T>> InsertAsync<T>(T poco) where T : class, IEntity;

		#endregion

		#region R: Query

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Result<IEnumerable<dynamic>> Query(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<Result<IEnumerable<dynamic>>> QueryAsync(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Result<IEnumerable<T>> Query<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		#endregion

		#region R: Single

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		Result<T> Single<T>(int id) where T : class, IEntity;

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Result<T> Single<T>(string query, object parameters, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<Result<T>> SingleAsync<T>(string query, object parameters, CommandType commandType = CommandType.Text);

		#endregion

		#region U

		/// <summary>
		/// Update an object
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		Result<bool> Update<T>(T poco) where T : class, IEntity;

		#endregion

		#region D

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity to delete</param>
		Result<bool> Delete<T>(T poco) where T : class, IEntity;

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity to delete</param>
		Task<Result<bool>> DeleteAsync<T>(T poco) where T : class, IEntity;

		#endregion

		#region Direct

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		Result<int> Execute(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		/// <returns>Affected rows</returns>
		Task<Result<int>> ExecuteAsync(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Result<T> ExecuteScalar<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <param name="commandType">CommandType</param>
		Task<Result<T>> ExecuteScalarAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text);

		#endregion
	}
}
