// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using static F.DataF.QueryF;
using static F.OptionF;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbQuery"/>
	public abstract class DbQuery
	{
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

	/// <inheritdoc cref="IDbQuery"/>
	public abstract class DbQuery<TDb> : DbQuery, IDbQuery
		where TDb : IDb
	{
		/// <inheritdoc/>
		public IUnitOfWork UnitOfWork =>
			Db.UnitOfWork;

		/// <summary>
		/// TDb
		/// </summary>
		protected TDb Db { get; private init; }

		internal TDb DbTest =>
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
		/// <param name="db">TDb</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		protected DbQuery(TDb db, ILog log) =>
			(Db, Log) = (db, log);

		/// <summary>
		/// Shorthand for escaping a column with its table name and alias
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Table object</param>
		/// <param name="column">Column selector</param>
#pragma warning disable IDE1006 // Naming Styles
		protected string __<TTable>(TTable table, Expression<Func<TTable, string>> column)
#pragma warning restore IDE1006 // Naming Styles
			where TTable : ITable =>
			GetColumnFromExpression(
				table, column
			)
			.Map(
				x => Db.Client.Escape(x, true),
				DefaultHandler
			)
			.Unwrap(
				r => throw new Exception(r.ToString())
			);

		#region QueryAsync

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Db.QueryAsync<TModel>(query, param, type, transaction);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		) =>
			Db.QueryAsync<TModel>(query, param, CommandType.Text, transaction);

		/// <inheritdoc/>
		public Task<Option<IPagedList<TModel>>> QueryAsync<TModel>(
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
					.BindAsync(
						x => Db.QueryAsync<TModel>(x.query, x.param, CommandType.Text, transaction)
					)
					.MapAsync(
						x => (IPagedList<TModel>)new PagedList<TModel>(pagingValues, x),
						DefaultHandler
					)
			);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			IQueryParts parts,
			IDbTransaction? transaction = null
		) =>
			Return(
				() => Db.Client.GetQuery(parts),
				e => new Msg.ErrorGettingQueryFromPartsExceptionMsg(e)
			)
			.BindAsync(
				x => Db.QueryAsync<TModel>(x.query, x.param, CommandType.Text, transaction)
			);

		#endregion

		#region QuerySingleAsync

		/// <inheritdoc/>
		public Task<Option<TModel>> QuerySingleAsync<TModel>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Db.QuerySingleAsync<TModel>(query, param, type, transaction);

		/// <inheritdoc/>
		public Task<Option<TModel>> QuerySingleAsync<TModel>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		) =>
			Db.QuerySingleAsync<TModel>(query, param, CommandType.Text, transaction);

		/// <inheritdoc/>
		public Task<Option<TModel>> QuerySingleAsync<TModel>(
			IQueryParts parts,
			IDbTransaction? transaction = null
		) =>
			Return(
				() => Db.Client.GetQuery(parts),
				e => new Msg.ErrorGettingQueryFromPartsExceptionMsg(e)
			)
			.BindAsync(
				x => Db.QuerySingleAsync<TModel>(x.query, x.param, CommandType.Text, transaction)
			);

		#endregion

		#region ExecuteAsync

		/// <inheritdoc/>
		public Task<Option<bool>> ExecuteAsync(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Db.ExecuteAsync(query, param, type, transaction);

		/// <inheritdoc/>
		public Task<Option<bool>> ExecuteAsync(
			string query,
			object? param,
			IDbTransaction? transaction = null
		) =>
			Db.ExecuteAsync(query, param, CommandType.Text, transaction);

		/// <inheritdoc/>
		public Task<Option<TReturn>> ExecuteAsync<TReturn>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Db.ExecuteAsync<TReturn>(query, param, type, transaction);

		/// <inheritdoc/>
		public Task<Option<TReturn>> ExecuteAsync<TReturn>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		) =>
			Db.ExecuteAsync<TReturn>(query, param, CommandType.Text, transaction);

		#endregion

		#region Testing

		internal string EscapeTest<TTable>(TTable table, Expression<Func<TTable, string>> column)
			where TTable : ITable =>
			__(table, column);

		#endregion
	}
}
