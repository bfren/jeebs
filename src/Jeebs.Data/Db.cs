// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Config.Db;
using Jeebs.Logging;
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
	/// ILog (should be given a context of the implementing class).
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
	protected virtual Action<string, object[]> WriteToLog =>
		Log.Vrb;

	/// <summary>
	/// Write query to the log.
	/// </summary>
	/// <typeparam name="TReturn">Query return type</typeparam>
	/// <param name="input">Input values.</param>
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
			WriteToLog(message, [.. args, param]);
		}
	}

	#region Querying

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type)
	{
		using var w = await StartWorkAsync();
		return await QueryAsync<T>(query, param, type, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			ok: LogQuery<T>
		)
		.MapAsync(
			x => transaction.Connection!.QueryAsync<T>(x.query, x.parameters, transaction, commandType: x.type)
		);

	/// <inheritdoc/>
	public async Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type)
	{
		using var w = await StartWorkAsync();
		return await QuerySingleAsync<T>(query, param, type, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			ok: LogQuery<T>
		)
		.MapAsync(
			x => transaction.Connection!.QuerySingleOrDefaultAsync<T>(x.query, x.parameters, transaction, commandType: x.type)
		)
		.BindAsync(
			x => x switch
			{
				T =>
					R.Wrap(x),

				_ =>
					R.Fail(nameof(Db), nameof(QuerySingleAsync),
						"Item not found or multiple items returned.", query, param
					)
			}
		);

	/// <inheritdoc/>
	public async Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type)
	{
		using var w = await StartWorkAsync();
		return await ExecuteAsync(query, param, type, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			ok: LogQuery<bool>
		)
		.MapAsync(
			x => transaction.Connection!.ExecuteAsync(x.query, x.parameters, transaction, commandType: x.type)
		)
		.MapAsync(
			x => x > 0
		);

	/// <inheritdoc/>
	public async Task<Result<T>> ExecuteAsync<T>(string query, object? param, CommandType type)
	{
		using var w = await StartWorkAsync();
		return await ExecuteAsync<T>(query, param, type, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			ok: LogQuery<T>
		)
		.MapAsync(
			x => transaction.Connection!.ExecuteScalarAsync<T>(x.query, x.parameters, transaction, commandType: x.type)
		)
		.BindAsync(
			x => x switch
			{
				T =>
					R.Wrap(x),

				_ =>
					R.Fail(nameof(Db), nameof(ExecuteAsync),
						"Execution returned null value.", query, param
					)
			}
		);

	#endregion Querying

	#region Testing

	internal void WriteToLogTest(string message, object[] args) =>
		WriteToLog(message, args);

	#endregion Testing
}
