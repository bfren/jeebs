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
		public IDbConnection Connection
			=> Transaction.Connection;

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
			=> (Adapter, Transaction, this.log) = (adapter, connection.BeginTransaction(), log);

		/// <inheritdoc/>
		public string Escape(object element)
			=> Adapter.SplitAndEscape(element.ToString());

		/// <inheritdoc/>
		public string Escape(params string?[] elements)
			=> Adapter.EscapeAndJoin(elements);

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
		public void LogDebug<T>(T message) where T : IMsg
			=> log.Debug(message.ToString());

		/// <inheritdoc/>
		public void LogQuery<T>(string method, string query, T parameters, CommandType commandType = CommandType.Text)
		{
			var sb = new StringBuilder();
			sb.AppendLine($"Method: UnitOfWork.{method}()");
			sb.AppendLine($"Query [{commandType}]: {query}");
			sb.AppendLine($"Parameters: {Json.Serialise(parameters)}");
			log.Debug(sb.ToString());
		}

		/// <inheritdoc/>
		private IError<T> Fail<T>(IOk r, Exception ex, string query, object? parameters = null)
		{
			// Rollback transaction
			Rollback();

			// Log error
			var message = new Jm.Data.QueryExceptionMsg(ex, query, parameters);
			log.Error(ex, message.ToString());

			// Return error
			return r.Error<T>().AddMsg(message);
		}

		#endregion

		#region R: Query

		/// <inheritdoc/>
		public IR<IEnumerable<dynamic>> Query(IOk r, string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(Query), query, parameters ?? new object(), commandType);

				// Execute and return
				var result = Connection.Query<dynamic>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public async Task<IR<IEnumerable<dynamic>>> QueryAsync(IOk r, string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(QueryAsync), query, parameters ?? new object(), commandType);

				// Execute and return
				var result = await Connection.QueryAsync<dynamic>(query, param: parameters, transaction: Transaction, commandType: commandType).ConfigureAwait(false);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public IR<IEnumerable<T>> Query<T>(IOk r, string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(Query), query, parameters ?? new object(), commandType);

				// Execute and return
				var result = Connection.Query<T>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public async Task<IR<IEnumerable<T>>> QueryAsync<T>(IOk r, string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(QueryAsync), query, parameters ?? new object(), commandType);

				// Execute and return
				var result = await Connection.QueryAsync<T>(query, param: parameters, transaction: Transaction, commandType: commandType).ConfigureAwait(false);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(r, ex, query, parameters);
			}
		}

		#endregion

		#region R: Single

		/// <inheritdoc/>
		public IR<T> Single<T>(IOk r, string query, object parameters, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(Single), query, parameters, commandType);

				// Execute and return
				var result = Connection.QuerySingle<T>(query, param: parameters, transaction: Transaction, commandType: commandType);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public async Task<IR<T>> SingleAsync<T>(IOk r, string query, object parameters, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(SingleAsync), query, parameters, commandType);

				// Execute and return
				var result = await Connection.QuerySingleAsync<T>(query, param: parameters, transaction: Transaction, commandType: commandType).ConfigureAwait(false);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(r, ex, query, parameters);
			}
		}

		#endregion

		#region Execute

		/// <inheritdoc/>
		public IR<int> Execute(IOk r, string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(Execute), query, parameters ?? new object(), commandType);

				// Execute and return
				var affectedRows = Connection.Execute(query, param: parameters, transaction: Transaction, commandType: commandType);
				return r.OkV(affectedRows);
			}
			catch (Exception ex)
			{
				return Fail<int>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public async Task<IR<int>> ExecuteAsync(IOk r, string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteAsync), query, parameters ?? new object(), commandType);

				// Execute and return
				var affectedRows = await Connection.ExecuteAsync(query, param: parameters, transaction: Transaction).ConfigureAwait(false);
				return r.OkV(affectedRows);
			}
			catch (Exception ex)
			{
				return Fail<int>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public IR<T> ExecuteScalar<T>(IOk r, string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteScalar), query, parameters ?? new object(), commandType);

				// Execute and return
				var result = Connection.ExecuteScalar<T>(query, param: parameters, transaction: Transaction);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public async Task<IR<T>> ExecuteScalarAsync<T>(IOk r, string query, object? parameters = null, CommandType commandType = CommandType.Text)
		{
			try
			{
				// Log query
				LogQuery(nameof(ExecuteScalarAsync), query, parameters ?? new object(), commandType);

				// Execute and return
				var result = await Connection.ExecuteScalarAsync<T>(query, param: parameters, transaction: Transaction).ConfigureAwait(false);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(r, ex, query, parameters);
			}
		}

		#endregion
	}
}
