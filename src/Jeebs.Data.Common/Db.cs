// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
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
