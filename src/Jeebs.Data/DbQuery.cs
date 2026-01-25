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
using Wrap.Exceptions;
using Defaults = Jeebs.Collections.Defaults.PagingValues;

namespace Jeebs.Data;

/// <inheritdoc cref="IDbQuery"/>
public abstract class DbQuery<TDb> : IDbQuery
	where TDb : IDb
{
	/// <inheritdoc/>
	public Task<IUnitOfWork> StartWorkAsync() =>
		Db.StartWorkAsync();

	/// <summary>
	/// TDb.
	/// </summary>
	protected TDb Db { get; private init; }

	internal TDb DbTest =>
		Db;

	/// <summary>
	/// ILog (should be given a context of the implementing class).
	/// </summary>
	protected ILog Log { get; private init; }

	internal ILog LogTest =>
		Log;

	/// <summary>
	/// Inject database and log objects.
	/// </summary>
	/// <param name="db">TDb.</param>
	/// <param name="log">ILog (should be given a context of the implementing class).</param>
	protected DbQuery(TDb db, ILog log) =>
		(Db, Log) = (db, log);

	/// <summary>
	/// Shorthand for escaping a column with its table name and alias.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <param name="table">Table object.</param>
	/// <param name="column">Column selector.</param>
#pragma warning disable CA1707 // Identifiers should not contain underscores
	protected string __<TTable>(TTable table, Expression<Func<TTable, string>> column)
#pragma warning restore CA1707 // Identifiers should not contain underscores
		where TTable : ITable =>
		QueryF.GetColumnFromExpression(
			table, column
		)
		.Map(
			x => Db.Client.Escape(x, true)
		)
		.Unwrap(
			f => throw new InvalidOperationException($"Could not get column from expression: {column}.", new FailureException(f))
		);

	#region QueryAsync

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type) =>
		Db.QueryAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.QueryAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param) =>
		Db.QueryAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.QueryAsync<T>(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public async Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts)
	{
		using var w = await Db.StartWorkAsync();
		return await QueryAsync<T>(page, parts, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IDbTransaction transaction) =>
		from count in Db.Client.GetCountQuery(parts)
		from countResults in Db.ExecuteAsync<ulong>(
			count.query, count.param, CommandType.Text, transaction
		)
		from paging in R.Try(
			() => new PagingValues(countResults, page, parts.Maximum ?? Defaults.ItemsPer, Defaults.PagesPer),
			e => R.Fail(e).Msg("Error creating paging values.", countResults, page, parts.Maximum)
				.Ctx(GetType().Name, nameof(QueryAsync))
		)
		from items in Db.Client.GetQuery(new QueryParts(parts) with
		{
			Skip = (paging.Page - 1) * paging.ItemsPer,
			Maximum = paging.ItemsPer
		})
		from itemsResults in Db.QueryAsync<T>(
			items.query, items.param, CommandType.Text, transaction
		)
		select (IPagedList<T>)new PagedList<T>(paging, itemsResults);

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts)
	{
		using var w = await Db.StartWorkAsync();
		return await QueryAsync<T>(parts, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IDbTransaction transaction) =>
		from q in Db.Client.GetQuery(parts)
		from r in Db.QueryAsync<T>(q.query, q.param, CommandType.Text, transaction)
		select r;

	#endregion QueryAsync

	#region QuerySingleAsync

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type) =>
		Db.QuerySingleAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.QuerySingleAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(string query, object? param) =>
		Db.QuerySingleAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.QuerySingleAsync<T>(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public async Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts)
	{
		using var w = await Db.StartWorkAsync();
		return await QuerySingleAsync<T>(parts, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts, IDbTransaction transaction) =>
		from q in Db.Client.GetQuery(parts)
		from r in Db.QuerySingleAsync<T>(q.query, q.param, CommandType.Text, transaction)
		select r;

	#endregion QuerySingleAsync

	#region ExecuteAsync

	/// <inheritdoc/>
	public Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type) =>
		Db.ExecuteAsync(query, param, type);

	/// <inheritdoc/>
	public Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.ExecuteAsync(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Result<bool>> ExecuteAsync(string query, object? param) =>
		Db.ExecuteAsync(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Result<bool>> ExecuteAsync(string query, object? param, IDbTransaction transaction) =>
		Db.ExecuteAsync(query, param, CommandType.Text, transaction);

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(string query, object? param, CommandType type) =>
		Db.ExecuteAsync<T>(query, param, type);

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		Db.ExecuteAsync<T>(query, param, type, transaction);

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(string query, object? param) =>
		Db.ExecuteAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(string query, object? param, IDbTransaction transaction) =>
		Db.ExecuteAsync<T>(query, param, CommandType.Text, transaction);

	#endregion ExecuteAsync

	#region Testing

	internal string EscapeTest<TTable>(TTable table, Expression<Func<TTable, string>> column)
		where TTable : ITable =>
		__(table, column);

	#endregion Testing
}
