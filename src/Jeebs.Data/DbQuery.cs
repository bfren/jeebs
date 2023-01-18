// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Data.Query.Functions;
using Jeebs.Logging;
using Jeebs.Messages;
using Defaults = Jeebs.Collections.Defaults.PagingValues;

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
	public Task<IUnitOfWork> StartWorkAsync() =>
		Db.StartWorkAsync();

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
#pragma warning disable CA1707 // Identifiers should not contain underscores
	protected string __<TTable>(TTable table, Expression<Func<TTable, string>> column)
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE1006 // Naming Styles
		where TTable : ITable =>
		QueryF.GetColumnFromExpression(
			table, column
		)
		.Map(
			x => Db.Client.Escape(x, true),
			F.DefaultHandler
		)
		.Unwrap(
			r => throw Msg.CreateException(r)
		);

	#region QueryAsync

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type) =>
		Db.QueryAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.QueryAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param) =>
		Db.QueryAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.QueryAsync<T>(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public async Task<Maybe<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts)
	{
		using var w = await Db.StartWorkAsync();
		return await QueryAsync<T>(page, parts, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IDbTransaction transaction) =>
		F.Some(
			() => Db.Client.GetCountQuery(parts),
			e => new M.ErrorGettingCountQueryFromPartsExceptionMsg(e)
		)
		.BindAsync(
			x => Db.ExecuteAsync<ulong>(x.query, x.param, CommandType.Text, transaction)
		)
		.MapAsync(
			x => new PagingValues(x, page, parts.Maximum ?? Defaults.ItemsPer, Defaults.PagesPer),
			F.DefaultHandler
		)
		.BindAsync(
			pagingValues =>
				F.Some(
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
					F.DefaultHandler
				)
		);

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts)
	{
		using var w = await Db.StartWorkAsync();
		return await QueryAsync<T>(parts, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IDbTransaction transaction) =>
		F.Some(
			() => Db.Client.GetQuery(parts),
			e => new M.ErrorGettingQueryFromPartsExceptionMsg(e)
		)
		.BindAsync(
			x => Db.QueryAsync<T>(x.query, x.param, CommandType.Text, transaction)
		);

	#endregion QueryAsync

	#region QuerySingleAsync

	/// <inheritdoc/>
	public Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, CommandType type) =>
		Db.QuerySingleAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.QuerySingleAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param) =>
		Db.QuerySingleAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.QuerySingleAsync<T>(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public async Task<Maybe<T>> QuerySingleAsync<T>(IQueryParts parts)
	{
		using var w = await Db.StartWorkAsync();
		return await QuerySingleAsync<T>(parts, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<T>> QuerySingleAsync<T>(IQueryParts parts, IDbTransaction transaction) =>
		F.Some(
			() => Db.Client.GetQuery(parts),
			e => new M.ErrorGettingQueryFromPartsExceptionMsg(e)
		)
		.BindAsync(
			x => Db.QuerySingleAsync<T>(x.query, x.param, CommandType.Text, transaction)
		);

	#endregion QuerySingleAsync

	#region ExecuteAsync

	/// <inheritdoc/>
	public Task<Maybe<bool>> ExecuteAsync(string query, object? param, CommandType type) =>
		Db.ExecuteAsync(query, param, type);

	/// <inheritdoc/>
	public Task<Maybe<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.ExecuteAsync(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Maybe<bool>> ExecuteAsync(string query, object? param) =>
		Db.ExecuteAsync(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Maybe<bool>> ExecuteAsync(string query, object? param, IDbTransaction transaction) =>
		Db.ExecuteAsync(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public Task<Maybe<T>> ExecuteAsync<T>(string query, object? param, CommandType type) =>
		Db.ExecuteAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Maybe<T>> ExecuteAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.ExecuteAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Maybe<T>> ExecuteAsync<T>(string query, object? param) =>
		Db.ExecuteAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Maybe<T>> ExecuteAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.ExecuteAsync<T>(query, param, CommandType.Text, transaction);

	#endregion ExecuteAsync

	#region Testing

	internal string EscapeTest<TTable>(TTable table, Expression<Func<TTable, string>> column)
		where TTable : ITable =>
		__(table, column);

	#endregion Testing
}
