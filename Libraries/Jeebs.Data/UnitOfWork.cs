using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Jeebs.Data
{
	public sealed class UnitOfWork
	{
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
				var insertSql = adapter.CreateSingleAndReturnId<T>();
				newId = Connection.ExecuteScalar<int>(insertSql, poco, transaction);
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
				var insertSql = adapter.CreateSingleAndReturnId<T>();
				newId = await Connection.ExecuteScalarAsync<int>(insertSql, poco, transaction);
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
				var result = Connection.Query<dynamic>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occurred while executing {nameof(Query)}";
				log.Error(error, ex.Message, query);
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
				var result = await Connection.QueryAsync<dynamic>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occurred while executing {nameof(QueryAsync)}";
				log.Error(ex, error, query, parameters);
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
				var result = Connection.Query<T>(query, param: parameters, transaction: transaction);
				return DbResult.Success(result);
			}
			catch (Exception ex)
			{
				// Log error
				var error = $"An error occurred while executing {nameof(Query)}";
				log.Error(ex, error, query, parameters);
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
		public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
		{
			try
			{
				return await Connection.QueryAsync<T>(query, param: parameters, transaction: transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="builder">Query builder</param>
		/// <returns>IEnumerable</returns>
		public IEnumerable<T> Query<T>(Func<Mapper.Build.Query<T>, Mapper.Build.Query<T>> builder) where T : IEntity
		{
			try
			{
				var (query, parameters) = builder(Mapper.Build.RetrieveByQuery<T>(Connection)).Build();
				return Connection.Query<T>(query, parameters, transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		/// <summary>
		/// Run a query against the database
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="builder">Query builder</param>
		/// <returns>IEnumerable</returns>
		public async Task<IEnumerable<T>> QueryAsync<T>(Func<Mapper.Build.Query<T>, Mapper.Build.Query<T>> builder) where T : IEntity
		{
			try
			{
				var (query, parameters) = builder(Mapper.Build.RetrieveByQuery<T>(Connection)).Build();
				return await Connection.QueryAsync<T>(query, parameters, transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>Entity (or null if not found)</returns>
		private T Get<T>(int id) where T : class, IEntity
		{
			try
			{
				var selectSql = Mapper.Build.RetrieveById<T>(Connection);
				return Connection.QuerySingleOrDefault<T>(selectSql, new { id }, transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>Entity (or null if not found)</returns>
		private async Task<T> GetAsync<T>(int id) where T : class, IEntity
		{
			try
			{
				var selectSql = Mapper.Build.RetrieveById<T>(Connection);
				return await Connection.QuerySingleOrDefaultAsync<T>(selectSql, new { id }, transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		/// <summary>
		/// Return a single object by primary key, throwing an exception if the object cannot be found
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="id">Primary key</param>
		/// <returns>Object</returns>
		public T Single<T>(int id) where T : class, IEntity
		{
			try
			{
				var entity = Get<T>(id);
				if (entity == null)
				{
					throw new Jx.Data.RetrieveException($"Null is not allowed when {nameof(Single)} is used.");
				}

				return entity;
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), id, ex);
			}
		}

		/// <summary>
		/// Return a single object by primary key, throwing an exception if the object cannot be found
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="id">Primary key</param>
		/// <returns>Object</returns>
		public async Task<T> SingleAsync<T>(int id) where T : class, IEntity
		{
			try
			{
				var entity = await GetAsync<T>(id);
				if (entity == null)
				{
					throw new Jx.Data.RetrieveException($"Null is not allowed when {nameof(SingleAsync)} is used.");
				}

				return entity;
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), id, ex);
			}
		}

		/// <summary>
		/// Return a single object by primary key, or default value if the object cannot be found
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="id">Primary key</param>
		/// <returns>Object or default value</returns>
		public T SingleOrDefault<T>(int id) where T : class, IEntity
		{
			try
			{
				return Get<T>(id);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), id, ex);
			}
		}

		/// <summary>
		/// Return a single object by primary key, or default value if the object cannot be found
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="id">Primary key</param>
		/// <returns>Object or default value</returns>
		public async Task<T> SingleOrDefaultAsync<T>(int id) where T : class, IEntity
		{
			try
			{
				return await GetAsync<T>(id);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), id, ex);
			}
		}

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Object or default value</returns>
		public T SingleOrDefault<T>(string query, object parameters = null)
		{
			try
			{
				return Connection.QuerySingleOrDefault<T>(query, parameters, transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		/// <summary>
		/// Return a single object by query, or default value if the object cannot be found
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Object or default value</returns>
		/// <returns>Object or default value</returns>
		public async Task<T> SingleOrDefaultAsync<T>(string query, object parameters = null)
		{
			try
			{
				return await Connection.QuerySingleOrDefaultAsync<T>(query, parameters, transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		/// <summary>
		/// Return a single object using a query, or default value if the object cannot be found
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="builder">Query builder</param>
		/// <returns>Object or default value</returns>
		public T SingleOrDefault<T>(Func<Mapper.Build.Query<T>, Mapper.Build.Query<T>> builder) where T : IEntity
		{
			try
			{
				var (query, parameters) = builder(Mapper.Build.RetrieveByQuery<T>(Connection)).Build();
				return Connection.QuerySingleOrDefault<T>(query, parameters, transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		/// <summary>
		/// Return a single object using a query, or default value if the object cannot be found
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="builder">Query builder</param>
		/// <returns>Object or default value</returns>
		public async Task<T> SingleOrDefaultAsync<T>(Func<Mapper.Build.Query<T>, Mapper.Build.Query<T>> builder) where T : IEntity
		{
			try
			{
				var (query, parameters) = builder(Mapper.Build.RetrieveByQuery<T>(Connection)).Build();
				return await Connection.QuerySingleOrDefaultAsync<T>(query, parameters, transaction);
			}
			catch (Exception ex)
			{
				throw new Jx.Data.RetrieveException(typeof(T), ex);
			}
		}

		#endregion

		#region U

		/// <summary>
		/// Provides thread-safe locking for Updates
		/// </summary>
		private static readonly object _ = new object();

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
				var updateSql = adapter.UpdateSingle<T>(poco.Id, poco.Version);

				// Now increase the row version and execute query
				poco.Version++;
				var rowsAffected = Connection.Execute(updateSql, poco, transaction);
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
				log.Error(ex, error);
				return new DbFailure(error, ex.Message);
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
				return new DbFailureConcurrency<T>();
			}
			else
			{
				// Rollback
				Rollback();

				// Log error
				log.Error(error);
				return new DbFailure(error);
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
				var updateSql = adapter.UpdateSingle<T>(poco.Id);

				// Now execute query
				var rowsAffected = Connection.Execute(updateSql, poco, transaction);
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
					return new DbFailure(error);
				}
			}
			catch (Exception ex)
			{
				// Rollback
				Rollback();

				// Log error
				log.Error(ex, error);
				return new DbFailure(error, ex.Message);
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
			throw new NotImplementedException();
		}

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="poco">Entity to delete</param>
		/// <result>IDbResult - Whether or not the delete was successful</result>
		public Task<IDbResult> DeleteAsync<T>(in T poco) where T : class, IEntity
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Direct

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		public void Execute(in string query, in object? parameters = null)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Execute a query on the database
		/// </summary>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		public Task ExecuteAsync(in string query, in object? parameters = null)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		/// <summary>
		/// Execute a query and return a scalar value
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">SQL qyery</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>IDbResult - on success, Value is Scalar value T</returns>
		public Task<IDbResult<T>> ExecuteScalarAsync<T>(in string query, in object? parameters = null)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
