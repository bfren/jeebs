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
	/// <inheritdoc cref="IUnitOfWork"/>
	public sealed class UnitOfWork : IUnitOfWork
	{
		/// <inheritdoc/>
		public IAdapter Adapter { get; }

		/// <inheritdoc/>
		public IDbConnection Connection => Transaction.Connection;

		/// <inheritdoc/>
		public IDbTransaction Transaction { get; }

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
		internal UnitOfWork(IDbConnection connection, IAdapter adapter, ILog log)
		{
			Adapter = adapter;
			Transaction = connection.BeginTransaction();
			this.log = log;
		}

		/// <inheritdoc/>
		public string Escape(object element) => Adapter.SplitAndEscape(element.ToString());

		/// <inheritdoc/>
		public string Escape(params string?[] elements) => Adapter.EscapeAndJoin(elements);

		/// <inheritdoc/>
		public void Commit()
		{
			try
			{
				Transaction.Commit();
			}
			catch (Exception ex)
			{
				log.Error(ex, "Error committing transaction.");
			}
		}

		/// <inheritdoc/>
		public void Rollback()
		{
			try
			{
				Transaction.Rollback();
			}
			catch (Exception ex)
			{
				log.Error(ex, "Error rolling back transaction.");
			}
		}

		/// <inheritdoc/>
		public void Dispose()
		{
			Commit();
			Transaction.Dispose();
			Connection.Dispose();
		}

		#region Logging & Failure

		/// <inheritdoc/>
		public void LogQuery<T>(string method, string query, T parameters, CommandType commandType = CommandType.Text)
		{
			log.Debug("Method: UnitOfWork.{0}()", method);
			log.Debug("Query [{0}]: {1}", commandType, query);
			log.Debug("Parameters: {0}", Json.Serialise(parameters));
		}

		/// <inheritdoc/>
		public IResult<bool> Fail(string error, params object[] args)
		{
			// Rollback transaction
			Rollback();

			// Log error
			log.Error(error, args);

			// Return failure object
			return Result.Failure(error);
		}

		/// <inheritdoc/>
		public IResult<bool> Fail(Exception ex, string error, params object[] args)
		{
			// Rollback transaction
			Rollback();

			// Log exception
			log.Error(ex, error, args);

			// Return failure object
			return Result.Failure(error);
		}

		/// <inheritdoc/>
		public IResult<T> Fail<T>(string error, params object[] args)
		{
			// Rollback transaction
			Rollback();

			// Log error
			log.Error(error, args);

			// Return failure object
			return Result.Failure<T>(error);
		}

		/// <inheritdoc/>
		public IResult<T> Fail<T>(Exception ex, string error, params object[] args)
		{
			// Rollback transaction
			Rollback();

			// Log exception
			log.Error(ex, error, args);

			// Return failure object
			return Result.Failure<T>(error);
		}

		#endregion

		#region R: Query

		/// <inheritdoc/>
		public IResult<IEnumerable<dynamic>> Query(string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(Query), query, parameters, commandType);

				// Execute and return
				var result = Connection.Query<dynamic>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(
					new Jx.Data.UnitOfWorkException(query, parameters, ex),
					"An error occurred while executing the query."
				);
			}
		}

		/// <inheritdoc/>
		public async Task<IResult<IEnumerable<dynamic>>> QueryAsync(string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(QueryAsync), query, parameters, commandType);

				// Execute and return
				var result = await Connection.QueryAsync<dynamic>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(
					new Jx.Data.UnitOfWorkException(query, parameters, ex),
					"An error occurred while executing the query."
				);
			}
		}

		/// <inheritdoc/>
		public IResult<IEnumerable<T>> Query<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(Query), query, parameters, commandType);

				// Execute and return
				var result = Connection.Query<T>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(
					new Jx.Data.UnitOfWorkException(query, parameters, ex),
					"An error occurred while executing the query."
				);
			}
		}

		/// <inheritdoc/>
		public async Task<IResult<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(QueryAsync), query, parameters, commandType);

				// Execute and return
				var result = await Connection.QueryAsync<T>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(
					new Jx.Data.UnitOfWorkException(query, parameters, ex),
					"An error occurred while executing the query."
				);
			}
		}

		#endregion

		#region R: Single

		/// <inheritdoc/>
		public IResult<T> Single<T>(string query, object parameters, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(Single), query, parameters, commandType);

				// Execute and return
				var result = Connection.QuerySingle<T>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(
					new Jx.Data.UnitOfWorkException(query, parameters, ex),
					$"An error occurred while retrieving {typeof(T)}."
				);
			}
		}

		/// <inheritdoc/>
		public async Task<IResult<T>> SingleAsync<T>(string query, object parameters, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(SingleAsync), query, parameters, commandType);

				// Execute and return
				var result = await Connection.QuerySingleAsync<T>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(
					new Jx.Data.UnitOfWorkException(query, parameters, ex),
					$"An error occurred while retrieving {typeof(T)}."
				);
			}
		}

		#endregion

		#region Execute

		/// <inheritdoc/>
		public IResult<int> Execute(string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(Execute), query, parameters, commandType);

				// Execute and return
				var affectedRows = Connection.Execute(query, param: parameters, transaction: Transaction, commandType: commandType);
				return Result.Success(affectedRows);
			}
			catch (Exception ex)
			{
				return Fail<int>(new Jx.Data.UnitOfWorkException(query, parameters, ex), "Error executing query.");
			}
		}

		/// <inheritdoc/>
		public async Task<IResult<int>> ExecuteAsync(string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteAsync), query, parameters, commandType);

				// Execute and return
				var affectedRows = await Connection.ExecuteAsync(query, param: parameters, transaction: Transaction);
				return Result.Success(affectedRows);
			}
			catch (Exception ex)
			{
				return Fail<int>(new Jx.Data.UnitOfWorkException(query, parameters, ex), "Error executing query.");
			}
		}

		/// <inheritdoc/>
		public IResult<T> ExecuteScalar<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteScalar), query, parameters, commandType);

				// Execute and return
				var result = Connection.ExecuteScalar<T>(query, param: parameters, transaction: Transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(new Jx.Data.UnitOfWorkException(query, parameters, ex), "Error executing query.");
			}
		}

		/// <inheritdoc/>
		public async Task<IResult<T>> ExecuteScalarAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteScalarAsync), query, parameters, commandType);

				// Execute and return
				var result = await Connection.ExecuteScalarAsync<T>(query, param: parameters, transaction: Transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(new Jx.Data.UnitOfWorkException(query, parameters, ex), "Error executing query.");
			}
		}

		#endregion
	}
}
