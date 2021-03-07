// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IUnitOfWork"/>
	public sealed class UnitOfWork : IUnitOfWork
	{
		/// <inheritdoc/>
		public IAdapter Adapter { get; }

		/// <inheritdoc/>
		public IDbConnection Connection =>
			Transaction.Connection ?? throw new InvalidOperationException("Transaction Connection cannot be null.");

		/// <inheritdoc/>
		public IDbTransaction Transaction { get; }

		/// <inheritdoc/>
		public IQueryDriver Driver { get; }

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
		/// <param name="driver">[Optional] IQueryDriver</param>
		internal UnitOfWork(IDbConnection connection, IAdapter adapter, ILog log, IQueryDriver? driver = null) =>
			(Adapter, Transaction, this.log, Driver) = (adapter, connection.BeginTransaction(), log, driver ?? new DapperQueryDriver());

		/// <inheritdoc/>
		public string Escape(object element) =>
			Adapter.SplitAndEscape(element.ToString() ?? string.Empty);

		/// <inheritdoc/>
		public string Escape(params string?[] elements) =>
			Adapter.EscapeAndJoin(elements);

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

		#region Failure

		/// <inheritdoc/>
		private IError<T> Fail<T>(IOk r, Exception ex, string query, object? parameters)
		{
			// Rollback transaction
			Rollback();

			// Log error
			var message = new Jm.Data.QueryExceptionMsg(ex, query, parameters);
			log.Error(ex, message.Format, message.ParamArray);

			// Return error
			return r.Error<T>().AddMsg(message);
		}

		#endregion

		#region R: Query

		/// <inheritdoc/>
		public IR<IEnumerable<dynamic>> Query(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(Query), query, parameters, commandType));

				// Execute and return
				var result = Driver.Query(Connection, query, parameters, Transaction, commandType);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public IR<IEnumerable<dynamic>> Query(IOk r, string query, object? parameters) =>
			 Query(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public IR<IEnumerable<dynamic>> Query(IOk r, string query) =>
			 Query(r, query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<IR<IEnumerable<dynamic>>> QueryAsync(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(QueryAsync), query, parameters, commandType));

				// Execute and return
				var result = await Driver.QueryAsync(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<IR<IEnumerable<dynamic>>> QueryAsync(IOk r, string query, object? parameters) =>
			 QueryAsync(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<IR<IEnumerable<dynamic>>> QueryAsync(IOk r, string query) =>
			 QueryAsync(r, query, null, CommandType.Text);

		/// <inheritdoc/>
		public IR<IEnumerable<T>> Query<T>(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(Query), query, parameters, commandType));

				// Execute and return
				var result = Driver.Query<T>(Connection, query, parameters, Transaction, commandType);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public IR<IEnumerable<T>> Query<T>(IOk r, string query, object? parameters) =>
			 Query<T>(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public IR<IEnumerable<T>> Query<T>(IOk r, string query) =>
			 Query<T>(r, query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<IR<IEnumerable<T>>> QueryAsync<T>(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(QueryAsync), query, parameters, commandType));

				// Execute and return
				var result = await Driver.QueryAsync<T>(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<IR<IEnumerable<T>>> QueryAsync<T>(IOk r, string query, object? parameters) =>
			 QueryAsync<T>(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<IR<IEnumerable<T>>> QueryAsync<T>(IOk r, string query) =>
			 QueryAsync<T>(r, query, null, CommandType.Text);

		#endregion

		#region R: Single

		/// <inheritdoc/>
		public IR<T> Single<T>(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(Single), query, parameters, commandType));

				// Execute and return
				var result = Driver.QuerySingle<T>(Connection, query, parameters, Transaction, commandType);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public IR<T> Single<T>(IOk r, string query, object? parameters) =>
			 Single<T>(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public IR<T> Single<T>(IOk r, string query) =>
			 Single<T>(r, query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<IR<T>> SingleAsync<T>(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(SingleAsync), query, parameters, commandType));

				// Execute and return
				var result = await Driver.QuerySingleAsync<T>(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<IR<T>> SingleAsync<T>(IOk r, string query, object? parameters) =>
			 SingleAsync<T>(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<IR<T>> SingleAsync<T>(IOk r, string query) =>
			 SingleAsync<T>(r, query, null, CommandType.Text);

		#endregion

		#region Execute

		/// <inheritdoc/>
		public IR<int> Execute(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(Execute), query, parameters, commandType));

				// Execute and return
				var affectedRows = Driver.Execute(Connection, query, parameters, Transaction, commandType);
				return r.OkV(affectedRows);
			}
			catch (Exception ex)
			{
				return Fail<int>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public IR<int> Execute(IOk r, string query, object? parameters) =>
			 Execute(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public IR<int> Execute(IOk r, string query) =>
			 Execute(r, query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<IR<int>> ExecuteAsync(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(ExecuteAsync), query, parameters, commandType));

				// Execute and return
				var affectedRows = await Driver.ExecuteAsync(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
				return r.OkV(affectedRows);
			}
			catch (Exception ex)
			{
				return Fail<int>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<IR<int>> ExecuteAsync(IOk r, string query, object? parameters) =>
			 ExecuteAsync(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<IR<int>> ExecuteAsync(IOk r, string query) =>
			 ExecuteAsync(r, query, null, CommandType.Text);

		/// <inheritdoc/>
		public IR<T> ExecuteScalar<T>(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(ExecuteScalar), query, parameters, commandType));

				// Execute and return
				var result = Driver.ExecuteScalar<T>(Connection, query, parameters, Transaction, commandType);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public IR<T> ExecuteScalar<T>(IOk r, string query, object? parameters) =>
			 ExecuteScalar<T>(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public IR<T> ExecuteScalar<T>(IOk r, string query) =>
			 ExecuteScalar<T>(r, query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<IR<T>> ExecuteScalarAsync<T>(IOk r, string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				r.AddMsg(new Jm.Data.QueryMsg(nameof(ExecuteScalarAsync), query, parameters, commandType));

				// Execute and return
				var result = await Driver.ExecuteScalarAsync<T>(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return Fail<T>(r, ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<IR<T>> ExecuteScalarAsync<T>(IOk r, string query, object? parameters) =>
			 ExecuteScalarAsync<T>(r, query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<IR<T>> ExecuteScalarAsync<T>(IOk r, string query) =>
			 ExecuteScalarAsync<T>(r, query, null, CommandType.Text);

		#endregion
	}
}
