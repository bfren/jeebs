// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Querying;
using static F.OptionF;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbQuery"/>
	public abstract class DbQuery : IDbQuery
	{
		/// <summary>
		/// IDb
		/// </summary>
		protected IDb Db { get; private init; }

		internal IDb DbTest =>
			Db;

		/// <summary>
		/// ILog (should be given a context of the implementing class)
		/// </summary>
		protected ILog Log { get; private init; }

		internal ILog LogTest =>
			Log;

		/// <summary>
		/// Inject database and log objects
		/// </summary>
		/// <param name="db">IDb</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		protected DbQuery(IDb db, ILog log) =>
			(Db, Log) = (db, log);

		#region Logging

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

		#endregion

		#region QueryAsync

		/// <inheritdoc/>
		public virtual Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Return(
				(query, param)
			)
			.Audit(
				some: x => LogQuery(nameof(QueryAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.QueryAsync<TModel>(x.query, x.param, type, transaction)
			);

		/// <inheritdoc/>
		public virtual Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		) =>
			QueryAsync<TModel>(query, param, CommandType.Text, transaction);

		/// <inheritdoc/>
		public virtual Task<Option<IPagedList<TModel>>> QueryAsync<TModel>(
			long page,
			IQueryParts parts,
			IDbTransaction? transaction = null
		) =>
			Return(
				() => Db.Client.GetCountQuery(parts),
				e => new Msg.ErrorGettingCountQueryFromPartsExceptionMsg(e)
			)
			.BindAsync(
				x => Db.ExecuteAsync<long>(x.query, x.param, CommandType.Text, transaction)
			)
			.MapAsync(
				x => new PagingValues(x, page, parts.Maximum ?? Defaults.PagingValues.ItemsPer),
				DefaultHandler
			)
			.BindAsync(
				pagingValues =>
					Return(
						() => Db.Client.GetQuery(new QueryParts(parts) with
						{
							Skip = (pagingValues.Page - 1) * pagingValues.ItemsPer,
							Maximum = pagingValues.ItemsPer
						}),
						e => new Msg.ErrorGettingQueryFromPartsExceptionMsg(e)
					)
					.Audit(
						some: x => LogQuery(nameof(QueryAsync), x.query, x.param)
					)
					.BindAsync(
						x => Db.QueryAsync<TModel>(x.query, x.param, CommandType.Text, transaction)
					)
					.MapAsync(
						x => (IPagedList<TModel>)new PagedList<TModel>(pagingValues, x),
						DefaultHandler
					)
			);

		/// <inheritdoc/>
		public virtual Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			IQueryParts parts,
			IDbTransaction? transaction = null
		) =>
			Return(
				() => Db.Client.GetQuery(parts),
				e => new Msg.ErrorGettingQueryFromPartsExceptionMsg(e)
			)
			.Audit(
				some: x => LogQuery(nameof(QueryAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.QueryAsync<TModel>(x.query, x.param, CommandType.Text, transaction)
			);

		#endregion

		#region QuerySingleAsync

		/// <inheritdoc/>
		public virtual Task<Option<TModel>> QuerySingleAsync<TModel>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Return(
				(query, param)
			)
			.Audit(
				some: x => LogQuery(nameof(QuerySingleAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.QuerySingleAsync<TModel>(x.query, x.param, type, transaction)
			);

		/// <inheritdoc/>
		public virtual Task<Option<TModel>> QuerySingleAsync<TModel>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		) =>
			QuerySingleAsync<TModel>(query, param, CommandType.Text, transaction);

		/// <inheritdoc/>
		public virtual Task<Option<TModel>> QuerySingleAsync<TModel>(
			IQueryParts parts,
			IDbTransaction? transaction = null
		) =>
			Return(
				() => Db.Client.GetQuery(parts),
				e => new Msg.ErrorGettingQueryFromPartsExceptionMsg(e)
			)
			.Audit(
				some: x => LogQuery(nameof(QueryAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.QuerySingleAsync<TModel>(x.query, x.param, CommandType.Text, transaction)
			);

		#endregion

		#region ExecuteAsync

		/// <inheritdoc/>
		public virtual Task<Option<bool>> ExecuteAsync(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Return(
				(query, param)
			)
			.Audit(
				some: x => LogQuery(nameof(QuerySingleAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.ExecuteAsync(x.query, x.param, type, transaction)
			);

		/// <inheritdoc/>
		public virtual Task<Option<bool>> ExecuteAsync(
			string query,
			object? param,
			IDbTransaction? transaction = null
		) =>
			ExecuteAsync(query, param, CommandType.Text, transaction);

		/// <inheritdoc/>
		public virtual Task<Option<TReturn>> ExecuteAsync<TReturn>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Return(
				(query, param)
			)
			.Audit(
				some: x => LogQuery(nameof(QuerySingleAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.ExecuteAsync<TReturn>(x.query, x.param, type, transaction)
			);

		/// <inheritdoc/>
		public virtual Task<Option<TReturn>> ExecuteAsync<TReturn>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		) =>
			ExecuteAsync<TReturn>(query, param, CommandType.Text, transaction);

		#endregion

		#region Testing

		internal void WriteToLogTest(string message, object[] args) =>
			WriteToLog(message, args);

		#endregion

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Error getting query from parts</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryFromPartsExceptionMsg(Exception Exception) : ExceptionMsg(Exception);

			/// <summary>Error getting count query from parts</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCountQueryFromPartsExceptionMsg(Exception Exception) : ExceptionMsg(Exception);
		}
	}
}
