// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Jeebs.Config.Db;
using Jeebs.Logging;
using Microsoft.Extensions.Options;

namespace Jeebs.Data.Common;

/// <inheritdoc cref="IDb"/>
public abstract partial class Db : Data.Db, IDb
{
	/// <inheritdoc/>
	public new IDbClient Client { get; private init; }

	Data.IDbClient Data.IDb.Client =>
		Client;

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
	protected Db(IDbClient client, DbConnectionConfig config, ILog log) : base(client, config, log) =>
		Client = client;

	/// <summary>
	/// Open a connection and start a new Unit of Work for <see cref="DoWorkAsync"/> - its transaction
	/// is only begun when a write first needs it.
	/// </summary>
	protected virtual async Task<IUnitOfWork> StartWorkAsync()
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

		// Begin a new transaction the first time it is used
		var transaction = new LazyAsync<DbTransaction>(async () =>
		{
			Log.Vrb("Beginning a new transaction.");
			return await connection.BeginTransactionAsync();
		});

		// Create Unit of Work
		Log.Vrb("Starting new Unit of Work.");
		return new UnitOfWork(connection, transaction, Log);
	}

	/// <inheritdoc/>
	public virtual async Task<Result<T>> DoWorkAsync<T>(Func<IUnitOfWork, Task<Result<T>>> work)
	{
		// disposing rolls back anything not committed - so a failure or an exception in work saves nothing
		await using var unitOfWork = await StartWorkAsync();
		return await work(unitOfWork).BindAsync(unitOfWork.CommitAsync);
	}

	/// <summary>
	/// Write query to the log.
	/// </summary>
	/// <typeparam name="TReturn">Query return type.</typeparam>
	/// <param name="input">Input values.</param>
	protected virtual void LogQuery<TReturn>((string query, object? parameters, CommandType type) input)
	{
		var (query, parameters, type) = input;

		// Always log query type, return type, and query
		var message = "Query Type: {Type} | Returns: {Return} | {Query}";
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
}
