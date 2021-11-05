// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Config;
using Microsoft.Extensions.Options;
using static F.OptionF;

namespace Jeebs.Data;

/// <inheritdoc cref="IDb"/>
public abstract class Db : IDb
{
	#region Static

	private static bool mapped;

	/// <summary>
	/// Add custom data type handlers
	/// </summary>
	/// <param name="map">Mapper action</param>
	protected static void AddTypeHandlers(Action<DbMapper> map)
	{
		// Only allow mapping once
		if (mapped)
		{
			return;
		}

		mapped = true;

		// Perform map
		map(new());
	}

	#endregion

	/// <inheritdoc/>
	public IDbClient Client { get; private init; }

	/// <inheritdoc/>
	public IUnitOfWork UnitOfWork
	{
		get
		{
			// Get a database connection
			Log.Verbose("Getting database connection.");
			var connection = Client.Connect(Config.ConnectionString);
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			// Create Unit of Work
			Log.Verbose("Starting new Unit of Work.");
			return new UnitOfWork(connection, Log.ForContext<UnitOfWork>());
		}
	}

	/// <summary>
	/// Configuration for this database connection
	/// </summary>
	public DbConnectionConfig Config { get; private init; }

	/// <summary>
	/// ILog (should be given a context of the implementing class)
	/// </summary>
	protected ILog Log { get; private init; }

	internal ILog LogTest =>
		Log;

	/// <summary>
	/// Inject database connection and connect to client
	/// </summary>
	/// <param name="client">Database client</param>
	/// <param name="config">Database configuration</param>
	/// <param name="log">ILog (should be given a context of the implementing class)</param>
	/// <param name="name">Connection name</param>
	protected Db(IDbClient client, IOptions<DbConfig> config, ILog log, string name) :
		this(client, config.Value.GetConnection(name), log)
	{ }

	/// <summary>
	/// Inject database connection and connect to client
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
		Log.Verbose;

	/// <summary>
	/// Write query to the log
	/// </summary>
	/// <param name="input">Input values</param>
	private void LogQuery((string query, object? parameters, CommandType type) input)
	{
		var (query, parameters, type) = input;

		// Always log operation, entity, and query
		var message = "{Type}: {Query}";
		var args = new object[] { type, query };

		// Log with or without parameters
		if (parameters == null)
		{
			WriteToLog(message, args);
		}
		else if (parameters.ToString() is string param)
		{
			message += " Parameters: {@Parameters}";
			WriteToLog(message, args.ExtendWith(param));
		}
	}

	#region Querying

	/// <inheritdoc/>
	public async Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters, CommandType type)
	{
		using var w = UnitOfWork;
		return await QueryAsync<T>(query, parameters, type, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? parameters, CommandType type, IDbTransaction transaction) =>
		Some(
			(query, parameters: parameters ?? new object(), type)
		)
		.Audit(
			some: LogQuery
		)
		.MapAsync(
			x => transaction.Connection.QueryAsync<T>(x.query, x.parameters, transaction, commandType: x.type),
			e => new Msg.QueryExceptionMsg(e)
		);

	/// <inheritdoc/>
	public async Task<Option<T>> QuerySingleAsync<T>(string query, object? parameters, CommandType type)
	{
		using var w = UnitOfWork;
		return await QuerySingleAsync<T>(query, parameters, type, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Option<T>> QuerySingleAsync<T>(string query, object? parameters, CommandType type, IDbTransaction transaction) =>
		Some(
			(query, parameters: parameters ?? new object(), type)
		)
		.Audit(
			some: LogQuery
		)
		.MapAsync(
			x => transaction.Connection.QuerySingleOrDefaultAsync<T>(x.query, x.parameters, transaction, commandType: x.type),
			e => new Msg.QuerySingleExceptionMsg(e)
		)
		.IfNullAsync(
			() => new Msg.QuerySingleItemNotFoundMsg()
		);

	/// <inheritdoc/>
	public async Task<Option<bool>> ExecuteAsync(string query, object? parameters, CommandType type)
	{
		using var w = UnitOfWork;
		return await ExecuteAsync(query, parameters, type, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Option<bool>> ExecuteAsync(string query, object? parameters, CommandType type, IDbTransaction transaction) =>
		Some(
			(query, parameters: parameters ?? new object(), type)
		)
		.Audit(
			some: LogQuery
		)
		.MapAsync(
			x => transaction.Connection.ExecuteAsync(x.query, x.parameters, transaction, commandType: x.type),
			e => new Msg.ExecuteExceptionMsg(e)
		)
		.MapAsync(
			x => x > 0,
			DefaultHandler
		);

	/// <inheritdoc/>
	public async Task<Option<T>> ExecuteAsync<T>(string query, object? parameters, CommandType type)
	{
		using var w = UnitOfWork;
		return await ExecuteAsync<T>(query, parameters, type, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Option<T>> ExecuteAsync<T>(string query, object? parameters, CommandType type, IDbTransaction transaction) =>
		Some(
			(query, parameters: parameters ?? new object(), type)
		)
		.Audit(
			some: LogQuery
		)
		.MapAsync(
			x => transaction.Connection.ExecuteScalarAsync<T>(x.query, x.parameters, transaction, commandType: x.type),
			e => new Msg.ExecuteScalarExceptionMsg(e)
		);

	#endregion

	#region Testing

	internal void WriteToLogTest(string message, object[] args) =>
		WriteToLog(message, args);

	#endregion

	/// <summary>Messages</summary>
	public static class Msg
	{
		/// <summary>Error running QueryAsync</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class QueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>Error running QuerySingleAsync</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class QuerySingleExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>The query returned no items, or more than one</summary>
		public sealed record class QuerySingleItemNotFoundMsg : NotFoundMsg { }

		/// <summary>Error running ExecuteAsync</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class ExecuteExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>Error running ExecuteScalarAsync</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class ExecuteScalarExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
	}
}
