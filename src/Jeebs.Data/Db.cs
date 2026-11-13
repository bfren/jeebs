// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Config.Db;
using Jeebs.Data.Functions;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Logging;
using Microsoft.Extensions.Options;
using Defaults = Jeebs.Constants.PagingValues;

namespace Jeebs.Data;

/// <inheritdoc cref="IDb"/>
public abstract class Db : IDb
{
	/// <inheritdoc/>
	public IDbClient Client { get; private init; }

	/// <inheritdoc/>
	public DbConnectionConfig Config { get; private init; }

	/// <inheritdoc/>
	public ILog Log { get; private init; }

	internal ILog LogTest =>
		Log;

	/// <summary>
	/// Inject database client and configuration.
	/// </summary>
	/// <param name="client">Database client.</param>
	/// <param name="config">Database configuration.</param>
	/// <param name="log">ILog (should be given a context of the implementing class).</param>
	/// <param name="name">Connection name.</param>
	protected Db(IDbClient client, IOptions<DbConfig> config, ILog log, string name) :
		this(client, config.Value.GetConnection(name).Unsafe().Unwrap(), log)
	{ }

	/// <summary>
	/// Inject database client and configuration.
	/// </summary>
	/// <param name="client">Database client.</param>
	/// <param name="config">Database configuration.</param>
	/// <param name="log">ILog (should be given a context of the implementing class).</param>
	protected Db(IDbClient client, DbConnectionConfig config, ILog log) =>
		(Client, Config, Log) = (client, config, log);

	/// <summary>
	/// Use Verbose log by default - override to send elsewhere (or to disable entirely).
	/// </summary>
	protected virtual Action<string, object?[]> WriteToLog =>
		Log.Vrb;

	/// <summary>
	/// Write query to the log.
	/// </summary>
	/// <typeparam name="TReturn">Query return type.</typeparam>
	/// <param name="input">Input values.</param>
	protected virtual void LogQuery<TReturn>((string query, object? parameters) input)
	{
		var (query, parameters) = input;

		// Always log query type, return type, and query
		var message = "Query Returns: {Return} | {Query}";
		object?[] args = [typeof(TReturn), query];

		// Log with or without parameters
		if (parameters is null)
		{
			WriteToLog(message, [.. args]);
		}
		else if (parameters.ToString() is string param)
		{
			message += " | Parameters: {Param}";
			WriteToLog(message, [.. args, param]);
		}
	}

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
		DataF.GetColumnFromExpression(
			table, column
		)
		.Map(
			x => Client.Escape(x, true)
		)
		.Unwrap(
			f => throw new InvalidOperationException($"Could not get column from expression: {column}: {f}")
		);

	/// <inheritdoc/>
	public abstract Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param);

	/// <inheritdoc/>
	public abstract Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts);

	/// <inheritdoc/>
	public virtual Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts) =>
		QueryAsync(page, parts, QuerySingleAsync<ulong>, QueryAsync<T>);

	/// <summary>
	/// Count the items that match <paramref name="parts"/>, then query the requested page of them.
	/// </summary>
	/// <typeparam name="T">Item type.</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="parts">Query parts.</param>
	/// <param name="count">Runs the count query.</param>
	/// <param name="items">Runs the items query.</param>
	protected Task<Result<IPagedList<T>>> QueryAsync<T>(
		ulong page,
		IQueryParts parts,
		Func<string, IQueryParametersDictionary, Task<Result<ulong>>> count,
		Func<string, IQueryParametersDictionary, Task<Result<IEnumerable<T>>>> items
	) =>
		from countQuery in Client.GetCountQuery(parts)
		from countResults in count(countQuery.query, countQuery.param)
		from paging in R.Try(
			() => new PagingValues(countResults, page, parts.Maximum ?? Defaults.ItemsPer, Defaults.PagesPer),
			e => R.Fail(e).Msg("Error creating paging values.", countResults, page, parts.Maximum)
				.Ctx(GetType().Name, nameof(QueryAsync))
		)
		from itemsQuery in Client.GetQuery(new QueryParts(parts) with
		{
			Skip = (paging.Page - 1) * paging.ItemsPer,
			Maximum = paging.ItemsPer
		})
		from itemsResults in items(itemsQuery.query, itemsQuery.param)
		select (IPagedList<T>)new PagedList<T>(paging, itemsResults);

	/// <inheritdoc/>
	public abstract Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder);

	/// <inheritdoc/>
	public abstract Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder);

	/// <inheritdoc/>
	public abstract Task<Result<T>> QuerySingleAsync<T>(string query, object? param);

	/// <inheritdoc/>
	public abstract Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts);

	/// <inheritdoc/>
	public abstract Task<Result<T>> QuerySingleAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder);

	/// <inheritdoc/>
	public abstract Task<Result<bool>> ExecuteAsync(string query, object? param);

	/// <inheritdoc/>
	public abstract Task<Result<TReturn>> InsertAsync<TReturn>(string query, object? param);

	#region Testing

	internal void WriteToLogTest(string message, object[] args) =>
		WriteToLog(message, args);

	internal void LogQueryTest<TReturn>((string query, object? parameters) input) =>
		LogQuery<TReturn>(input);

	#endregion Testing
}
