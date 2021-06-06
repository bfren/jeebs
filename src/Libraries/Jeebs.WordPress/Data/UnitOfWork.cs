// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Logging;
using static F.OptionF;

namespace Jeebs.WordPress.Data
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

		/// <inheritdoc/>
		public ILog Log { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="connection">IDbConnection</param>
		/// <param name="adapter">IAdapter</param>
		/// <param name="log">ILog</param>
		/// <param name="driver">[Optional] IQueryDriver</param>
		internal UnitOfWork(IDbConnection connection, IAdapter adapter, ILog<UnitOfWork> log, IQueryDriver? driver = null) =>
			(Adapter, Transaction, Log, Driver) = (adapter, connection.BeginTransaction(), log, driver ?? new DapperQueryDriver());

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
				Log.Error(ex, "Error committing transaction.");
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
				Log.Error(ex, "Error rolling back transaction.");
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
		private None<T> Fail<T>(Exception ex, string query, object? parameters)
		{
			// Rollback transaction
			Rollback();

			// Return error
			return None<T>(new Msg.QueryExceptionMsg(ex, query, parameters));
		}

		#endregion

		#region R: Query

		/// <inheritdoc/>
		public Option<IEnumerable<dynamic>> Query(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(Query), query, parameters, commandType));

				// Execute and return
				var result = Driver.Query(Connection, query, parameters, Transaction, commandType);
				return Return(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Option<IEnumerable<dynamic>> Query(string query, object? parameters) =>
			 Query(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Option<IEnumerable<dynamic>> Query(string query) =>
			 Query(query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<Option<IEnumerable<dynamic>>> QueryAsync(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(QueryAsync), query, parameters, commandType));

				// Execute and return
				var result = await Driver.QueryAsync(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
				return Return(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<dynamic>>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<Option<IEnumerable<dynamic>>> QueryAsync(string query, object? parameters) =>
			 QueryAsync(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<dynamic>>> QueryAsync(string query) =>
			 QueryAsync(query, null, CommandType.Text);

		/// <inheritdoc/>
		public Option<IEnumerable<T>> Query<T>(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(Query), query, parameters, commandType));

				// Execute and return
				var result = Driver.Query<T>(Connection, query, parameters, Transaction, commandType);
				return Return(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Option<IEnumerable<T>> Query<T>(string query, object? parameters) =>
			 Query<T>(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Option<IEnumerable<T>> Query<T>(string query) =>
			 Query<T>(query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(QueryAsync), query, parameters, commandType));

				// Execute and return
				var result = await Driver.QueryAsync<T>(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
				return Return(result);
			}
			catch (Exception ex)
			{
				return Fail<IEnumerable<T>>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters) =>
			 QueryAsync<T>(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<T>>> QueryAsync<T>(string query) =>
			 QueryAsync<T>(query, null, CommandType.Text);

		#endregion

		#region R: Single

		/// <inheritdoc/>
		public Option<T> Single<T>(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(Single), query, parameters, commandType));

				// Execute and return
				return Driver.QuerySingle<T>(Connection, query, parameters, Transaction, commandType);
			}
			catch (Exception ex)
			{
				return Fail<T>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Option<T> Single<T>(string query, object? parameters) =>
			 Single<T>(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Option<T> Single<T>(string query) =>
			 Single<T>(query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<Option<T>> SingleAsync<T>(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(SingleAsync), query, parameters, commandType));

				// Execute and return
				return await Driver.QuerySingleAsync<T>(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				return Fail<T>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<Option<T>> SingleAsync<T>(string query, object? parameters) =>
			 SingleAsync<T>(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<Option<T>> SingleAsync<T>(string query) =>
			 SingleAsync<T>(query, null, CommandType.Text);

		#endregion

		#region Execute

		/// <inheritdoc/>
		public Option<int> Execute(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(Execute), query, parameters, commandType));

				// Execute and return
				return Driver.Execute(Connection, query, parameters, Transaction, commandType);
			}
			catch (Exception ex)
			{
				return Fail<int>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Option<int> Execute(string query, object? parameters) =>
			 Execute(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Option<int> Execute(string query) =>
			 Execute(query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<Option<int>> ExecuteAsync(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(ExecuteAsync), query, parameters, commandType));

				// Execute and return
				return await Driver.ExecuteAsync(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				return Fail<int>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<Option<int>> ExecuteAsync(string query, object? parameters) =>
			 ExecuteAsync(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<Option<int>> ExecuteAsync(string query) =>
			 ExecuteAsync(query, null, CommandType.Text);

		/// <inheritdoc/>
		public Option<T> ExecuteScalar<T>(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(ExecuteScalar), query, parameters, commandType));

				// Execute and return
				return Driver.ExecuteScalar<T>(Connection, query, parameters, Transaction, commandType);
			}
			catch (Exception ex)
			{
				return Fail<T>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Option<T> ExecuteScalar<T>(string query, object? parameters) =>
			 ExecuteScalar<T>(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Option<T> ExecuteScalar<T>(string query) =>
			 ExecuteScalar<T>(query, null, CommandType.Text);

		/// <inheritdoc/>
		public async Task<Option<T>> ExecuteScalarAsync<T>(string query, object? parameters, CommandType commandType)
		{
			try
			{
				// Log query
				Log.Message(new Msg.QueryMsg(nameof(ExecuteScalarAsync), query, parameters, commandType));

				// Execute and return
				return await Driver.ExecuteScalarAsync<T>(Connection, query, parameters, Transaction, commandType).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				return Fail<T>(ex, query, parameters);
			}
		}

		/// <inheritdoc/>
		public Task<Option<T>> ExecuteScalarAsync<T>(string query, object? parameters) =>
			 ExecuteScalarAsync<T>(query, parameters, CommandType.Text);

		/// <inheritdoc/>
		public Task<Option<T>> ExecuteScalarAsync<T>(string query) =>
			 ExecuteScalarAsync<T>(query, null, CommandType.Text);

		#endregion

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Query message</summary>
			/// <param name="Method">The name of the UnitOfWork method executing this query</param>
			/// <param name="Query">Query text</param>
			/// <param name="Parameters">Query parameters</param>
			/// <param name="CommandType">Command Type</param>
			public sealed record QueryMsg(string Method, string Query, object? Parameters, CommandType CommandType) :
				LogMsg(LogLevel.Debug, "{Method}: {CommandType} {Query} ({@Parameters})")
			{
				/// <inheritdoc/>
				public override Func<object[]> Args =>
					() => new object[] { Method, CommandType, Query, Parameters ?? new object() };
			}

			/// <summary>Query Exception message</summary>
			/// <param name="Exception">The exception caught while the query was executing</param>
			/// <param name="Query">Query text</param>
			/// <param name="Parameters">Query parameters</param>
			public sealed record QueryExceptionMsg(Exception Exception, string Query, object? Parameters) :
				ExceptionMsg(Exception, "{Query} ({@Parameters}")
			{
				/// <inheritdoc/>
				public override Func<object[]> Args =>
					() => new object[] { Query, Parameters ?? new object() };
			}
		}
	}
}
