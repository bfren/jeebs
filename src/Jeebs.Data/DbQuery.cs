// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using static F.DataF.QueryF;
using static F.OptionF;

namespace Jeebs.Data;

/// <inheritdoc cref="IDbQuery"/>
public abstract class DbQuery
{
	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Error getting query from parts</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingQueryFromPartsExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Error getting count query from parts</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingCountQueryFromPartsExceptionMsg(Exception Value) : ExceptionMsg;
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
	public Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type) =>
		Db.QueryAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.QueryAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param) =>
		Db.QueryAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.QueryAsync<T>(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public async Task<Option<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts)
	{
		using var w = UnitOfWork;
		return await QueryAsync<T>(page, parts, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Option<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IDbTransaction transaction) =>
		Some(
			() => Db.Client.GetCountQuery(parts),
			e => new M.ErrorGettingCountQueryFromPartsExceptionMsg(e)
		)
		.BindAsync(
			x => Db.ExecuteAsync<ulong>(x.query, x.param, CommandType.Text, transaction)
		)
		.MapAsync(
			x => new PagingValues(x, page, parts.Maximum ?? Defaults.PagingValues.ItemsPer, Defaults.PagingValues.PagesPer),
			DefaultHandler
		)
		.BindAsync(
			pagingValues =>
				Some(
					() => Db.Client.GetQuery(new QueryParts(parts) with
					{
						Skip = (pagingValues.Page - 1) * pagingValues.ItemsPer,
						Maximum = pagingValues.ItemsPer
					}),
					e => new M.ErrorGettingQueryFromPartsExceptionMsg(e)
				)
				.BindAsync(
					x => Db.QueryAsync<T>(x.query, x.param, CommandType.Text, transaction)
				)
				.MapAsync(
					x => (IPagedList<T>)new PagedList<T>(pagingValues, x),
					DefaultHandler
				)
		);

	/// <inheritdoc/>
	public async Task<Option<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts)
	{
		using var w = UnitOfWork;
		return await QueryAsync<T>(parts, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Option<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IDbTransaction transaction) =>
		Some(
			() => Db.Client.GetQuery(parts),
			e => new M.ErrorGettingQueryFromPartsExceptionMsg(e)
		)
		.BindAsync(
			x => Db.QueryAsync<T>(x.query, x.param, CommandType.Text, transaction)
		);

	#endregion

	#region QuerySingleAsync

	/// <inheritdoc/>
	public Task<Option<T>> QuerySingleAsync<T>(string query, object? param, CommandType type) =>
		Db.QuerySingleAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Option<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.QuerySingleAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Option<T>> QuerySingleAsync<T>(string query, object? param) =>
		Db.QuerySingleAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Option<T>> QuerySingleAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.QuerySingleAsync<T>(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public async Task<Option<T>> QuerySingleAsync<T>(IQueryParts parts)
	{
		using var w = UnitOfWork;
		return await QuerySingleAsync<T>(parts, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Option<T>> QuerySingleAsync<T>(IQueryParts parts, IDbTransaction transaction) =>
		Some(
			() => Db.Client.GetQuery(parts),
			e => new M.ErrorGettingQueryFromPartsExceptionMsg(e)
		)
		.BindAsync(
			x => Db.QuerySingleAsync<T>(x.query, x.param, CommandType.Text, transaction)
		);

	#endregion

	#region ExecuteAsync

	/// <inheritdoc/>
	public Task<Option<bool>> ExecuteAsync(string query, object? param, CommandType type) =>
		Db.ExecuteAsync(query, param, type);

	/// <inheritdoc/>
	public Task<Option<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.ExecuteAsync(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Option<bool>> ExecuteAsync(string query, object? param) =>
		Db.ExecuteAsync(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Option<bool>> ExecuteAsync(string query, object? param, IDbTransaction transaction) =>
		Db.ExecuteAsync(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public Task<Option<T>> ExecuteAsync<T>(string query, object? param, CommandType type) =>
		Db.ExecuteAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Option<T>> ExecuteAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.ExecuteAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Option<T>> ExecuteAsync<T>(string query, object? param) =>
		Db.ExecuteAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Option<T>> ExecuteAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.ExecuteAsync<T>(query, param, CommandType.Text, transaction);

	#endregion

	#region Testing

	internal string EscapeTest<TTable>(TTable table, Expression<Func<TTable, string>> column)
		where TTable : ITable =>
		__(table, column);

	#endregion
}
