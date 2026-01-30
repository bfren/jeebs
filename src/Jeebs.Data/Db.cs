// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Config.Db;
using Jeebs.Data.Map;
using Jeebs.Data.QueryBuilder;
using Jeebs.Logging;
using Microsoft.Extensions.Options;
using Wrap.Exceptions;

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
		this(client, config.Value.GetConnection(name).Unwrap(), log)
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
	protected void LogQuery<TReturn>((string query, object? parameters, CommandType type) input)
	{
		var (query, parameters, type) = input;

		// Always log query type, return type, and query
		var message = "Query Type: {Type} | Return: {Return} | {Query}";
		object?[] args = [type, typeof(TReturn), query];

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
			f => throw new InvalidOperationException($"Could not get column from expression: {column}.", new FailureException(f))
		);

	/// <inheritdoc/>
	public abstract Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param);

	/// <inheritdoc/>
	public abstract Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts);

	/// <inheritdoc/>
	public abstract Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts);

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
	public abstract Task<Result<TReturn>> ExecuteAsync<TReturn>(string query, object? param);

	#region Testing

	internal void WriteToLogTest(string message, object[] args) =>
		WriteToLog(message, args);

	#endregion Testing
}
