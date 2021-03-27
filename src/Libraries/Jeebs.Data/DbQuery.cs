// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbQuery"/>
	public abstract class DbQuery : IDbQuery
	{
		/// <summary>
		/// IDb
		/// </summary>
		protected IDb Db { get; }

		/// <summary>
		/// ILog (should be given a context of the implementing class)
		/// </summary>
		protected ILog Log { get; }

		/// <summary>
		/// Inject database and log objects
		/// </summary>
		/// <param name="db">IDb</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		protected DbQuery(IDb db, ILog log) =>
			(Db, Log) = (db, log);

		/// <summary>
		/// Use Debug log by default - override to send elsewhere (or to disable entirely)
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Log message arguments</param>
		protected virtual void WriteToLog(string message, object[] args) =>
			Log.Debug(message, args);

		/// <summary>
		/// Log the query for a function
		/// </summary>
		/// <param name="method">Method name</param>
		/// <param name="query">Query text</param>
		/// <param name="parameters">[Optional] Query parameters</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		protected void LogQuery(string method, string query, object? parameters)
		{
			// Always log operation, entity, and query
			var message = "{Method}: {Query}";
			var args = new object[] { method, query };

			// Log with or without parameters
			if (parameters == null)
			{
				WriteToLog(message, args);
			}
			else
			{
				message += " {@Parameters}";
				WriteToLog(message, args.ExtendWith(parameters));
			}
		}

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(string query, object? param) =>
			QueryAsync<TModel>(query, param, CommandType.Text);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(string query, object? param, CommandType commandType) =>
			Return(
				(query, param)
			)
			.AuditSwitch(
				some: x => LogQuery(nameof(QueryAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.QueryAsync<TModel>(x.query, x.param, commandType)
			);

		/// <inheritdoc/>
		public Task<Option<TModel>> QuerySingleAsync<TModel>(string query, object? param) =>
			QuerySingleAsync<TModel>(query, param, CommandType.Text);

		/// <inheritdoc/>
		public Task<Option<TModel>> QuerySingleAsync<TModel>(string query, object? param, CommandType commandType) =>
			Return(
				(query, param)
			)
			.AuditSwitch(
				some: x => LogQuery(nameof(QuerySingleAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.QuerySingleAsync<TModel>(x.query, x.param, commandType)
			);

		#region Testing

		internal void WriteToLogTest(string message, object[] args) =>
			WriteToLog(message, args);

		#endregion
	}
}
