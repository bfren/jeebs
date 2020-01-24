using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Util;

namespace Jeebs.Data
{
	/// <summary>
	/// Database Unit of Work
	/// </summary>
	public sealed class UnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Provides thread-safe locking
		/// </summary>
		private static readonly object _ = new object();

		/// <summary>
		/// IDbTransaction
		/// </summary>
		private readonly IDbTransaction transaction;

		/// <summary>
		/// IDbConnection
		/// </summary>
		private IDbConnection Connection => transaction.Connection;

		/// <summary>
		/// IAdapter
		/// </summary>
		private readonly IAdapter adapter;

		/// <summary>
		/// ILog
		/// </summary>
		private readonly ILog log;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="connection">IDbConnection</param>
		/// <param name="adapter">IAdapter</param>
		/// <param name="log">ILog</param>
		internal UnitOfWork(in IDbConnection connection, in IAdapter adapter, in ILog log)
		{
			transaction = connection.BeginTransaction();
			this.adapter = adapter;
			this.log = log;
		}

		/// <summary>
		/// Shorthand for Table[].ExtractColumns and then IAdapter.Join
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		public string Extract<T>(params Table[] tables) => adapter.Join(tables.ExtractColumns<T>());

		/// <summary>
		/// Shorthand for IAdapter.SplitAndEscape
		/// </summary>
		/// <param name="element">The element to split and escape</param>
		public string Escape(in object element) => adapter.SplitAndEscape(element.ToString());

		/// <summary>
		/// Commit all queries - should normally be called as part of Dispose()
		/// </summary>
		public void Commit()
		{
			try
			{
				transaction.Commit();
			}
			catch (Exception ex)
			{
				log.Error(ex, "Error committing transaction.");
			}
		}

		/// <summary>
		/// Rollback all queries
		/// </summary>
		public void Rollback()
		{
			try
			{
				transaction.Rollback();
			}
			catch (Exception ex)
			{
				log.Error(ex, "Error rolling back transaction.");
			}
		}

		/// <summary>
		/// Commit transaction and close connection
		/// </summary>
		public void Dispose()
		{
			Commit();
			transaction.Dispose();
			Connection.Dispose();
		}

		#region Logging

		/// <summary>
		/// Log a query
		/// </summary>
		/// <typeparam name="T">Parameter object type</typeparam>
		/// <param name="method">Calling method</param>
		/// <param name="query">SQL query</param>
		/// <param name="parameters">Parameters</param>
		private void LogQuery<T>(string method, string query, T parameters)
		{
			log.Debug("Method: UnitOfWork.{0}()", method);
			log.Debug("Query: {0}", query);
			log.Debug("Parameters: {0}", Json.Serialise(parameters));
		}

		/// <summary>
		/// Query failure -
		///		Rollback
		///		Log Error
		///		Return Failure Result
		/// </summary>
		/// <param name="error">Error message</param>
		/// <param name="args">Error message arguments</param>
		private Failure Fail(string error, params object[] args)
		{
			// Rollback transaction
			Rollback();

			// Log error
			log.Error(error, args);

			// Return failure object
			return Result.Failure(error);
		}

		/// <summary>
		/// Query failure -
		///		Rollback
		///		Log Error
		///		Return Failure Result
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="error">Error message</param>
		/// <param name="args">Error message arguments</param>
		private Failure Fail(Exception ex, string error, params object[] args)
		{
			// Rollback transaction
			Rollback();

			// Log exception
			log.Error(ex, error, args);

			// Return failure object
			return Result.Failure(error);
		}

		/// <summary>
		/// Query failure -
		///		Rollback
		///		Log Error
		///		Return Failure Result
		/// </summary>
		/// <typeparam name="T">Return value type</typeparam>
		/// <param name="error">Error message</param>
		/// <param name="args">Error message arguments</param>
		private Failure<T> Fail<T>(string error, params object[] args)
		{
			// Rollback transaction
			Rollback();

			// Log error
			log.Error(error, args);

			// Return failure object
			return Result.Failure<T>(error);
		}

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
		private Failure<T> Fail<T>(Exception ex, string error, params object[] args)
		{
			// Rollback transaction
			Rollback();

			// Log exception
			log.Error(ex, error, args);

			// Return failure object
			return Result.Failure<T>(error);
		}

		#endregion

		#region C

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>Entity (complete with new ID)</returns>
		public Result<T> Insert<T>(T poco)
			where T : class, IEntity
		{
			// Declare here so accessible outside try...catch
			int newId;

			try
			{
				// Build query
				var query = adapter.CreateSingleAndReturnId<T>();
				LogQuery(nameof(Insert), query, poco);

				// Insert and capture new ID
				newId = Connection.ExecuteScalar<int>(query, param: poco, transaction: transaction);
			}
			catch (Exception ex)
			{
				return Fail<T>(ex, $"Unable to insert {typeof(T)}.");
			}

			// If newId is still 0, rollback changes - something went wrong
			if (newId == 0)
			{
				return Fail<T>($"Unable to retrieve ID of inserted {typeof(T)}.");
			}

			// Retrieve fresh POCO with inserted ID
			return Single<T>(newId);
		}

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>Entity (complete with new ID)</returns>
		public async Task<Result<T>> InsertAsync<T>(T poco)
			where T : class, IEntity
		{
			// Declare here so accessible outside try...catch
			int newId;

			try
			{
				// Build query
				var query = adapter.CreateSingleAndReturnId<T>();
				LogQuery(nameof(InsertAsync), query, poco);

				// Insert and capture new ID
				newId = await Connection.ExecuteScalarAsync<int>(query, param: poco, transaction: transaction);
			}
			catch (Exception ex)
			{
				return Fail<T>(ex, $"Unable to insert {typeof(T)}.");
			}

			// If newId is still 0, rollback changes - something went wrong
			if (newId == 0)
			{
				return Fail<T>($"Unable to retrieve ID of inserted {typeof(T)}.");
			}

			return await SingleAsync<T>(newId);
		}

		#endregion

		#region R

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		public Result<IEnumerable<dynamic>> Query(in string query, in object? parameters = null)
		{
			try
			{
				// Log query
				LogQuery(nameof(Query), query, parameters);

				// Execute and return
				var result = Connection.Query<dynamic>(query, param: parameters, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(
					new Jx.Data.QueryException(query, parameters, ex),
					$"An error occurred while executing the query."
				);
			}
		}

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		public async Task<Result<IEnumerable<dynamic>>> QueryAsync(string query, object? parameters = null)
		{
			try
			{
				// Log query
				LogQuery(nameof(QueryAsync), query, parameters);

				// Execute and return
				var result = await Connection.QueryAsync<dynamic>(query, param: parameters, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(
					new Jx.Data.QueryException(query, parameters, ex),
					$"An error occurred while executing the query."
				);
			}
		}

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		public Result<IEnumerable<T>> Query<T>(string query, object? parameters = null)
		{
			try
			{
				// Log query
				LogQuery(nameof(Query), query, parameters);

				// Execute and return
				var result = Connection.Query<T>(query, param: parameters, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(
					new Jx.Data.QueryException(query, parameters, ex),
					$"An error occurred while executing the query."
				);
			}
		}

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		public async Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters = null)
		{
			try
			{
				// Log query
				LogQuery(nameof(QueryAsync), query, parameters);

				// Execute and return
				var result = await Connection.QueryAsync<T>(query, param: parameters, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(
					new Jx.Data.QueryException(query, parameters, ex),
					$"An error occurred while executing the query."
				);
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>Entity (or null if not found)</returns>
		public Result<T> Single<T>(int id)
			where T : class, IEntity
		{
			try
			{
				// Build query
				var query = adapter.RetrieveSingleById<T>();
				LogQuery(nameof(Single), query, new { id });

				// Execute and return
				var result = Connection.QuerySingle<T>(query, param: new { id }, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(ex, $"An error occured while retrieving {typeof(T)} with ID '{id}'.");
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>Entity (or null if not found)</returns>
		private async Task<Result<T>> SingleAsync<T>(int id)
			where T : class, IEntity
		{
			try
			{
				// Build query
				var query = adapter.RetrieveSingleById<T>();
				LogQuery(nameof(SingleAsync), query, new { id });

				// Execute and return
				var result = await Connection.QuerySingleAsync<T>(query, param: new { id }, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(ex, $"An error occured while retrieving {typeof(T)} with ID '{id}'.");
			}
		}

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Object or default value</returns>
		public Result<T> Single<T>(string query, object parameters)
		{
			try
			{
				// Log query
				LogQuery(nameof(Single), query, parameters);

				// Execute and return
				var result = Connection.QuerySingle<T>(query, param: parameters, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(
					new Jx.Data.QueryException(query, parameters, ex),
					$"An error occurred while retrieving {typeof(T)}."
				);
			}
		}

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Object or default value</returns>
		public async Task<Result<T>> SingleAsync<T>(string query, object parameters)
		{
			try
			{
				// Log query
				LogQuery(nameof(SingleAsync), query, parameters);

				// Execute and return
				var result = await Connection.QuerySingleAsync<T>(query, param: parameters, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(
					new Jx.Data.QueryException(query, parameters, ex),
					$"An error occurred while retrieving {typeof(T)}."
				);
			}
		}

		#endregion

		#region U

		/// <summary>
		/// Update an object
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>IDbResult - Whether or not the update was successful</returns>
		public Result<bool> Update<T>(in T poco)
			where T : class, IEntity
		{
			lock (_)
			{
				if (poco is IEntityWithVersion pocoWithVersion)
				{
					return UpdateWithVersion(pocoWithVersion);
				}
				else
				{
					return UpdateWithoutVersion(poco);
				}
			}
		}

		/// <summary>
		/// Update using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Object</param>
		/// <returns>IDbResult - Whether or not the update was successful</returns>
		private Result<bool> UpdateWithVersion<T>(in T poco)
			where T : class, IEntityWithVersion
		{
			var currentVersion = poco.Version;
			var error = $"Unable to update {typeof(T)} '{poco.Id}'.";

			try
			{
				// Build query and increase the version number
				var query = adapter.UpdateSingle<T>();
				poco.Version++;
				LogQuery(nameof(UpdateWithVersion), query, poco);

				// Execute and return
				var rowsAffected = Connection.Execute(query, param: poco, transaction: transaction);
				if (rowsAffected == 1)
				{
					return Result.Success();
				}

				return Fail(error);
			}
			catch (Exception ex)
			{
				return Fail(ex, error);
			}
		}

		/// <summary>
		/// Update without using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Object</param>
		/// <returns>IDbResult - Whether or not the update was successful</returns>
		private Result<bool> UpdateWithoutVersion<T>(in T poco)
			where T : class, IEntity
		{
			var error = $"Unable to update {typeof(T)} '{poco.Id}'.";

			try
			{
				// Build query
				var query = adapter.UpdateSingle<T>();
				LogQuery(nameof(UpdateWithoutVersion), query, poco);

				// Execute and return
				var rowsAffected = Connection.Execute(query, param: poco, transaction: transaction);
				if (rowsAffected == 1)
				{
					return Result.Success();
				}

				return Fail(error);
			}
			catch (Exception ex)
			{
				return Fail(ex, error);
			}
		}

		#endregion

		#region D

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity to delete</param>
		/// <result>IDbResult - Whether or not the delete was successful</result>
		public Result<bool> Delete<T>(in T poco)
			where T : class, IEntity
		{
			var error = $"Unable to delete {typeof(T)} '{poco.Id}'.";

			try
			{
				// Build query
				var query = adapter.DeleteSingle<T>();
				LogQuery(nameof(Delete), query, poco);

				// Execute and return
				var rowsAffected = Connection.Execute(query, param: poco, transaction: transaction);
				if (rowsAffected == 1)
				{
					return Result.Success();
				}

				return Fail(error);
			}
			catch (Exception ex)
			{
				return Fail(ex, error);
			}
		}

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity to delete</param>
		/// <result>IDbResult - Whether or not the delete was successful</result>
		public async Task<Result<bool>> DeleteAsync<T>(T poco)
			where T : class, IEntity
		{
			var error = $"Unable to delete {typeof(T)} '{poco.Id}'.";

			try
			{
				// Build query
				var query = adapter.DeleteSingle<T>();
				LogQuery(nameof(DeleteAsync), query, poco);

				// Execute and return
				var rowsAffected = await Connection.ExecuteAsync(query, param: poco, transaction: transaction);
				if (rowsAffected == 1)
				{
					return Result.Success();
				}

				return Fail(error);
			}
			catch (Exception ex)
			{
				return Fail(ex, error);
			}
		}

		#endregion

		#region Direct

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Affected rows</returns>
		public Result<int> Execute(in string query, in object? parameters = null)
		{
			try
			{
				// Log query
				LogQuery(nameof(Execute), query, parameters);

				// Execute and return
				var affectedRows = Connection.Execute(query, param: parameters, transaction: transaction);
				return Result.Success(affectedRows);
			}
			catch (Exception ex)
			{
				return Fail<int>(new Jx.Data.QueryException(query, parameters, ex), "Error executing query.");
			}
		}

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Affected rows</returns>
		public async Task<Result<int>> ExecuteAsync(string query, object? parameters = null)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteAsync), query, parameters);

				// Execute and return
				var affectedRows = await Connection.ExecuteAsync(query, param: parameters, transaction: transaction);
				return Result.Success(affectedRows);
			}
			catch (Exception ex)
			{
				return Fail<int>(new Jx.Data.QueryException(query, parameters, ex), "Error executing query.");
			}
		}

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IDbResult - on success, Value is Scalar value T</returns>
		public Result<T> ExecuteScalar<T>(in string query, in object? parameters = null)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteScalar), query, parameters);

				// Execute and return
				var result = Connection.ExecuteScalar<T>(query, param: parameters, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(new Jx.Data.QueryException(query, parameters, ex), "Error executing query.");
			}
		}

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IDbResult - on success, Value is Scalar value T</returns>
		public async Task<Result<T>> ExecuteScalarAsync<T>(string query, object? parameters = null)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteScalarAsync), query, parameters);

				// Execute and return
				var result = await Connection.ExecuteScalarAsync<T>(query, param: parameters, transaction: transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(new Jx.Data.QueryException(query, parameters, ex), "Error executing query.");
			}
		}

		#endregion
	}
}
