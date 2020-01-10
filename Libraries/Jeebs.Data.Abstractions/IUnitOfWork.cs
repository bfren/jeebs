using System;
using System.Collections.Generic;
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
		/// Shorthand for Table[].ExtractColumns and then IAdapter.Join
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		string Extract<T>(params Table[] tables);

		/// <summary>
		/// Shorthand for IAdapter.SplitAndEscape
		/// </summary>
		/// <param name="element">The element to split and escape</param>
		string Escape(in object element);

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
		/// <returns>Entity (complete with new ID)</returns>
		IDbResult<T> Insert<T>(T poco) where T : class, IEntity;

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>Entity (complete with new ID)</returns>
		public Task<IDbResult<T>> InsertAsync<T>(T poco) where T : class, IEntity;

		#endregion

		#region R

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		IDbResult<IEnumerable<dynamic>> Query(in string query, in object? parameters = null);

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		Task<IDbResult<IEnumerable<dynamic>>> QueryAsync(string query, object? parameters = null);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		IDbResult<IEnumerable<T>> Query<T>(string query, object? parameters = null);

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		Task<IDbResult<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters = null);

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>Entity (or null if not found)</returns>
		IDbResult<T> Single<T>(int id) where T : class, IEntity;

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Object or default value</returns>
		IDbResult<T> Single<T>(string query, object parameters);

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Object or default value</returns>
		Task<IDbResult<T>> SingleAsync<T>(string query, object parameters);

		#endregion

		#region U

		/// <summary>
		/// Update an object
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>IDbResult - Whether or not the update was successful</returns>
		IDbResult Update<T>(in T poco) where T : class, IEntity;

		#endregion

		#region D

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity to delete</param>
		/// <result>IDbResult - Whether or not the delete was successful</result>
		IDbResult Delete<T>(in T poco) where T : class, IEntity;

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity to delete</param>
		/// <result>IDbResult - Whether or not the delete was successful</result>
		Task<IDbResult> DeleteAsync<T>(T poco) where T : class, IEntity;

		#endregion

		#region Direct

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Affected rows</returns>
		IDbResult<int> Execute(in string query, in object? parameters = null);

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Affected rows</returns>
		Task<IDbResult<int>> ExecuteAsync(string query, object? parameters = null);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IDbResult - on success, Value is Scalar value T</returns>
		IDbResult<T> ExecuteScalar<T>(in string query, in object? parameters = null);

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IDbResult - on success, Value is Scalar value T</returns>
		Task<IDbResult<T>> ExecuteScalarAsync<T>(string query, object? parameters = null);

		#endregion
	}
}
