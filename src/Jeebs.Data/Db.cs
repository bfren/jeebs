// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Config.Db;
using Jeebs.Logging;
using Jeebs.Messages;
using Microsoft.Extensions.Options;

namespace Jeebs.Data;

/// <inheritdoc cref="IDb"/>
public abstract class Db : IDb
{
	/// <inheritdoc/>
	public IDbClient Client { get; private init; }

	/// <inheritdoc/>
	public DbConnectionConfig Config { get; private init; }

	/// <summary>
	/// ILog (should be given a context of the implementing class)
	/// </summary>
	protected ILog Log { get; private init; }

	internal ILog LogTest =>
		Log;

	/// <inheritdoc/>
	public virtual IUnitOfWork StartWork()
	{
		// Get a database connection
		Log.Vrb("Getting database connection.");
		var connection = Client.GetConnection(Config.ConnectionString);

		// Open database connection
		if (connection.State != ConnectionState.Open)
		{
			Log.Vrb("Connecting to the database.");
			connection.Open();
		}

		// Begin a new transaction
		Log.Vrb("Beginning a new transaction.");
		var transaction = connection.BeginTransaction();

		// Create Unit of Work
		Log.Vrb("Starting new Unit of Work.");
		return new UnitOfWork(connection, transaction, Log);
	}

	/// <inheritdoc/>
	public virtual async Task<IUnitOfWork> StartWorkAsync()
	{
		// Connect to the database
		Log.Vrb("Getting database connection.");
		var connection = Client.GetConnection(Config.ConnectionString);

		// Open database connection
		if (connection.State != ConnectionState.Open)
		{
			Log.Vrb("Connecting to the database.");
			await connection.OpenAsync();
		}

		// Begin a new transaction
		Log.Vrb("Beginning a new transaction.");
		var transaction = await connection.BeginTransactionAsync();

		// Create Unit of Work
		Log.Vrb("Starting new Unit of Work.");
		return new UnitOfWork(connection, transaction, Log);
	}

	/// <summary>
	/// Inject database client and configuration
	/// </summary>
	/// <param name="client">Database client</param>
	/// <param name="config">Database configuration</param>
	/// <param name="log">ILog (should be given a context of the implementing class)</param>
	/// <param name="name">Connection name</param>
	protected Db(IDbClient client, IOptions<DbConfig> config, ILog log, string name) :
		this(client, config.Value.GetConnection(name), log)
	{ }

	/// <summary>
	/// Inject database client and configuration
	/// </summary>
	/// <param name="client">Database client</param>
	/// <param name="config">Database configuration</param>
	/// <param name="log">ILog (should be given a context of the implementing class)</param>
	protected Db(IDbClient client, DbConnectionConfig config, ILog log) =>
		(Client, Config, Log) = (client, config, log);

	/// <summary>
	/// Use Verbose log by default - override to send elsewhere (or to disable entirely)
	/// </summary>
	protected virtual Action<string, object[]> WriteToLog =>
		Log.Vrb;

	/// <summary>
	/// Write query to the log
	/// </summary>
	/// <typeparam name="TReturn">Query return type</typeparam>
	/// <param name="input">Input values</param>
	private void LogQuery<TReturn>((string query, object? parameters, CommandType type) input)
	{
		var (query, parameters, type) = input;

		// Always log query type, return type, and query
		var message = "Query Type: {Type} | Return: {Return} | {Query}";
		var args = new object[] { type, typeof(TReturn), query };

		// Log with or without parameters
		if (parameters is null)
		{
			WriteToLog(message, args);
		}
		else if (parameters.ToString() is string param)
		{
			message += " | Parameters: {Parameters}";
			WriteToLog(message, args.ExtendWith(param));
		}
	}

	#region Querying

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type)
	{
		using var w = await StartWorkAsync();
		return await QueryAsync<T>(query, param, type, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		F.Some(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			some: LogQuery<T>
		)
		.MapAsync(
			x => transaction.Connection.QueryAsync<T>(x.query, x.parameters, transaction, commandType: x.type),
			e => new M.QueryExceptionMsg(e)
		);

	/// <inheritdoc/>
	public async Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, CommandType type)
	{
		using var w = await StartWorkAsync();
		return await QuerySingleAsync<T>(query, param, type, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		F.Some(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			some: LogQuery<T>
		)
		.MapAsync(
			x => transaction.Connection.QuerySingleOrDefaultAsync<T>(x.query, x.parameters, transaction, commandType: x.type),
			e => new M.QuerySingleExceptionMsg(e)
		)
		.IfNullAsync(
			() => new M.QuerySingleItemNotFoundMsg((query, param))
		);

	/// <inheritdoc/>
	public async Task<Maybe<bool>> ExecuteAsync(string query, object? param, CommandType type)
	{
		using var w = await StartWorkAsync();
		return await ExecuteAsync(query, param, type, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction) =>
		F.Some(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			some: LogQuery<bool>
		)
		.MapAsync(
			x => transaction.Connection.ExecuteAsync(x.query, x.parameters, transaction, commandType: x.type),
			e => new M.ExecuteExceptionMsg(e)
		)
		.MapAsync(
			x => x > 0,
			F.DefaultHandler
		);

	/// <inheritdoc/>
	public async Task<Maybe<T>> ExecuteAsync<T>(string query, object? param, CommandType type)
	{
		using var w = await StartWorkAsync();
		return await ExecuteAsync<T>(query, param, type, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<T>> ExecuteAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		F.Some(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			some: LogQuery<T>
		)
		.MapAsync(
			x => transaction.Connection.ExecuteScalarAsync<T>(x.query, x.parameters, transaction, commandType: x.type),
			e => new M.ExecuteScalarExceptionMsg(e)
		);

	#endregion Querying

	#region Testing

	internal void WriteToLogTest(string message, object[] args) =>
		WriteToLog(message, args);

	#endregion Testing

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Error running QueryAsync</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class QueryExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Error running QuerySingleAsync</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class QuerySingleExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>The query returned no items, or more than one</summary>
		/// <param name="Value">Query parameters</param>
		public sealed record class QuerySingleItemNotFoundMsg((string sql, object? parameters) Value)
			: NotFoundMsg<(string sql, object? parameters)>
		{
			/// <inheritdoc/>
			public override string Name =>
				"Query";
		}

		/// <summary>Error running ExecuteAsync</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ExecuteExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Error running ExecuteScalarAsync</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ExecuteScalarExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
