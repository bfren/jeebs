using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Util;

namespace Jeebs.Data
{
	public sealed class UnitOfWork
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
		/// Setup object
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
		/// Commit all queries - should normally be called as part of Dispose()
		/// </summary>
		public void Commit() => transaction.Commit();

		/// <summary>
		/// Rollback all queries
		/// </summary>
		public void Rollback() => transaction.Rollback();

		/// <summary>
		/// Commit transaction and close connection
		/// </summary>
		public void Dispose()
		{
			Commit();
			transaction.Dispose();
			Connection.Dispose();
		}

		#region C

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>Entity (complete with new ID)</returns>
		public IDbResult<T> Insert<T>(T poco) where T : class, IEntity
		{
			int newId;

			try
			{
				var query = adapter.CreateSingleAndReturnId<T>();
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(poco));

				newId = Connection.ExecuteScalar<int>(query, poco, transaction);
			}
			catch (Exception ex)
			{
				// Rollback
				Rollback();

				// Log error
				var error = $"Unable to insert {typeof(T)}.";
				log.Error(error);
				return DbResult.Failure<T>(error, ex.Message);
			}

			if (newId == 0)
			{
				// Rollback
				Rollback();

				// Log error
				var error = $"Unable to retrieve ID of inserted {typeof(T)}.";
				log.Error(error);
				return DbResult.Failure<T>(error);
			}

			return Single<T>(newId);
		}

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity object</param>
		/// <returns>Entity (complete with new ID)</returns>
		public async Task<IResult<T>> InsertAsync<T>(T poco) where T : class, IEntity
		{
			int newId;

			try
			{
				var query = adapter.CreateSingleAndReturnId<T>();
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(poco));

				newId = await Connection.ExecuteScalarAsync<int>(query, poco, transaction);
			}
			catch (Exception ex)
			{
				// Rollback
				Rollback();

				// Log error
				var error = $"Unable to insert {typeof(T)}.";
				log.Error(ex, error);
				return DbResult.Failure<T>(error, ex.Message);
			}

			if (newId == 0)
			{
				// Rollback
				Rollback();

				// Log error
				var error = $"Unable to retrieve ID of inserted {typeof(T)}.";
				log.Error(error);
				return DbResult.Failure<T>(error);
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
		public IDbResult<IEnumerable<dynamic>> Query(in string query, in object? parameters = null)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));

				var result = Connection.Query<dynamic>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occurred while executing {nameof(Query)}";
				log.Error(error, ex.Message, query, "parameters: " + Json.Serialise(parameters));
				return DbResult.Failure<IEnumerable<dynamic>>(error);
			}
		}

		/// <summary>
		/// Perform a query, returning a dynamic object
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		public async Task<IDbResult<IEnumerable<dynamic>>> QueryAsync(string query, object? parameters = null)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));

				var result = await Connection.QueryAsync<dynamic>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occurred while executing {nameof(QueryAsync)}";
				log.Error(ex, error, query, "parameters: " + Json.Serialise(parameters));
				return DbResult.Failure<IEnumerable<dynamic>>(error);
			}
		}

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		public IDbResult<IEnumerable<T>> Query<T>(string query, object? parameters = null)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));

				var result = Connection.Query<T>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occurred while executing {nameof(Query)}";
				log.Error(ex, error, query, "parameters: " + Json.Serialise(parameters));
				return DbResult.Failure<IEnumerable<T>>(error);
			}
		}

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IEnumerable</returns>
		public async Task<IDbResult<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters = null)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));

				var result = await Connection.QueryAsync<T>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occurred while executing {nameof(QueryAsync)}";
				log.Error(ex, error, query, "parameters: " + Json.Serialise(parameters));
				return DbResult.Failure<IEnumerable<T>>(error);
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>Entity (or null if not found)</returns>
		public IDbResult<T> Single<T>(int id) where T : class, IEntity
		{
			try
			{
				var query = adapter.RetrieveSingleById<T>(id);

				log.Debug("Query: {0}", query);

				var result = Connection.QuerySingle<T>(query, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occured while retrieving {typeof(T)} with ID '{id}'.";
				log.Error(ex, error);
				return DbResult.Failure<T>(error);
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>Entity (or null if not found)</returns>
		private async Task<IDbResult<T>> SingleAsync<T>(int id) where T : class, IEntity
		{
			try
			{
				var query = adapter.RetrieveSingleById<T>(id);

				log.Debug("Query: {0}", query);

				var result =  await Connection.QuerySingleAsync<T>(query, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occured while retrieving {typeof(T)} with ID '{id}'.";
				log.Error(ex, error);
				return DbResult.Failure<T>(error);
			}
		}

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Object or default value</returns>
		public IDbResult<T> Single<T>(string query, object parameters)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));

				var result = Connection.QuerySingle<T>(query, parameters, transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occured while retrieving {typeof(T)}.";
				log.Error(ex, error, query, Json.Serialise(parameters));
				return DbResult.Failure<T>(error);
			}
		}

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Object or default value</returns>
		public async Task<IDbResult<T>> SingleAsync<T>(string query, object parameters)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));

				var result = await Connection.QuerySingleAsync<T>(query, parameters, transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occured while retrieving {typeof(T)}.";
				log.Error(ex, error, query, Json.Serialise(parameters));
				return DbResult.Failure<T>(error);
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
		public IDbResult Update<T>(in T poco) where T : class, IEntity
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
		private IDbResult UpdateWithVersion<T>(in T poco) where T : class, IEntityWithVersion
		{
			var currentVersion = poco.Version;
			var error = $"Unable to update {typeof(T)} {poco.Id}.";

			try
			{
				// Build the query
				var query = adapter.UpdateSingle<T>(poco.Id, poco.Version);

				// Now increase the row version and execute query
				poco.Version++;

				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(poco));

				var rowsAffected = Connection.Execute(query, poco, transaction);
				if (rowsAffected == 1)
				{
					return new DbSuccess();
				}
			}
			catch (Exception ex)
			{
				// Rollback
				Rollback();

				// Log error
				log.Error(ex, error, Json.Serialise(poco));
				return DbResult.Failure(error, ex.Message);
			}

			// Build the query to get a fresh poco
			var selectSql = adapter.RetrieveSingleById<T>(poco.Id);

			// Get the fresh poco
			var freshPoco = Connection.QuerySingle<T>(selectSql);
			if (freshPoco.Version > currentVersion)
			{
				// Rollback
				Rollback();

				// Log error
				log.Error(error + " Concurrency check failed.");
				return DbResult.ConcurrencyFailure<T>();
			}
			else
			{
				// Rollback
				Rollback();

				// Log error
				log.Error(error);
				return DbResult.Failure(error);
			}
		}

		/// <summary>
		/// Update without using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Object</param>
		private IDbResult UpdateWithoutVersion<T>(in T poco) where T : class, IEntity
		{
			var error = $"Unable to update {typeof(T)} {poco.Id}.";

			try
			{
				// Build the query
				var query = adapter.UpdateSingle<T>(poco.Id);
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(poco));

				// Now execute query
				var rowsAffected = Connection.Execute(query, poco, transaction);
				if (rowsAffected == 1)
				{
					return new DbSuccess();
				}
				else
				{
					// Rollback
					Rollback();

					// Log error
					log.Error(error);
					return DbResult.Failure(error);
				}
			}
			catch (Exception ex)
			{
				// Rollback
				Rollback();

				// Log error
				log.Error(ex, error);
				return DbResult.Failure(error, ex.Message);
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
		public IDbResult Delete<T>(in T poco) where T : class, IEntity
		{
			var error = $"Unable to delete {typeof(T)} {poco.Id}.";

			try
			{
				// Build the query
				var query = adapter.DeleteSingle<T>(poco.Id);
				log.Debug("Query: {0}");

				// Now execute query
				var rowsAffected = Connection.Execute(query, transaction: transaction);
				if (rowsAffected == 1)
				{
					return DbResult.Success();
				}
				else
				{
					// Rollback
					Rollback();

					// Log error
					log.Error(error);
					return DbResult.Failure(error);
				}
			}
			catch (Exception ex)
			{
				// Rollback
				Rollback();

				// Log error
				log.Error(ex, error);
				return DbResult.Failure(error, ex.Message);
			}
		}

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity to delete</param>
		/// <result>IDbResult - Whether or not the delete was successful</result>
		public async Task<IDbResult> DeleteAsync<T>(T poco) where T : class, IEntity
		{
			var error = $"Unable to delete {typeof(T)} {poco.Id}.";

			try
			{
				// Build the query
				var query = adapter.DeleteSingle<T>(poco.Id);
				log.Debug("Query: {0}");

				// Now execute query
				var rowsAffected = await Connection.ExecuteAsync(query, transaction: transaction);
				if (rowsAffected == 1)
				{
					return DbResult.Success();
				}
				else
				{
					// Rollback
					Rollback();

					// Log error
					log.Error(error);
					return DbResult.Failure(error);
				}
			}
			catch (Exception ex)
			{
				// Rollback
				Rollback();

				// Log error
				log.Error(ex, error);
				return DbResult.Failure(error, ex.Message);
			}
		}

		#endregion

		#region Direct

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		public IDbResult Execute(in string query, in object? parameters = null)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));
				Connection.Execute(query, param: parameters, transaction: transaction);
				return DbResult.Success();
			}
			catch (Exception ex)
			{
				// Log error
				var error = "Error executing query";
				log.Error(ex, error, query, Json.Serialise(parameters));
				return DbResult.Failure();
			}
		}

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		public async Task<IDbResult> ExecuteAsync(string query, object? parameters = null)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));
				await Connection.ExecuteAsync(query, param: parameters, transaction: transaction);
				return DbResult.Success();
			}
			catch (Exception ex)
			{
				// Log error
				var error = "Error executing query";
				log.Error(ex, error, query, Json.Serialise(parameters));
				return DbResult.Failure();
			}
		}

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IDbResult - on success, Value is Scalar value T</returns>
		public IDbResult<T> ExecuteScalar<T>(in string query, in object? parameters = null)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));
				var result = Connection.ExecuteScalar<T>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = "Error executing query";
				log.Error(ex, error, query, Json.Serialise(parameters));
				return DbResult.Failure<T>();
			}
		}

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IDbResult - on success, Value is Scalar value T</returns>
		public async Task<IDbResult<T>> ExecuteScalarAsync<T>(string query, object? parameters = null)
		{
			try
			{
				log.Debug("Query: {0}\nParameters: {1}", query, Json.Serialise(parameters));
				var result = await Connection.ExecuteScalarAsync<T>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = "Error executing query";
				log.Error(ex, error, query, Json.Serialise(parameters));
				return DbResult.Failure<T>();
			}
		}

		#endregion
	}
}
