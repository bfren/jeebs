// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

#if NET6_0
using System;
using System.Collections.Generic;
using Jeebs.Logging.Serilog.PostgreSql.Functions;
using Npgsql.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Parsing;

namespace Jeebs.Logging.Serilog.PostgreSql;

/// <summary>
/// Logger implementation for Npgsql
/// See https://github.com/mysql-net/MySqlConnector/blob/master/src/MySqlConnector.Logging.Serilog/SerilogLoggerProvider.cs
/// </summary>
public sealed class PostgreSqlLogger : NpgsqlLogger
{
	/// <summary>
	/// Logger instance
	/// </summary>
	internal ILogger Logger { get; private init; }

	/// <summary>
	/// Create log instance by name
	/// </summary>
	/// <param name="name">Log instance name</param>
	public PostgreSqlLogger(string name) =>
		Logger = global::Serilog.Log.ForContext("SourceContext", name);

	/// <summary>
	/// Returns true if the log is enabled for <paramref name="level"/>
	/// </summary>
	/// <param name="level">Requested level</param>
	public override bool IsEnabled(NpgsqlLogLevel level) =>
		LevelF.ConvertToSerilogLevel(level).Switch(
			some: x => Logger.IsEnabled(x),
			none: false
		);

	/// <summary>
	/// Send a message to the log
	/// </summary>
	/// <param name="level">Event level</param>
	/// <param name="connectorId">Connector ID</param>
	/// <param name="msg">Log message</param>
	/// <param name="exception">[Optional] Exception</param>
	public override void Log(NpgsqlLogLevel level, int connectorId, string msg, Exception? exception = null) =>
		LevelF.ConvertToSerilogLevel(level).Switch(
			some: x => Log(x, connectorId, msg, exception),
			none: r => Log(NpgsqlLogLevel.Fatal, connectorId, $"Unable to log: '{r}'.", exception)
		);

	private void Log(LogEventLevel level, int connectorId, string msg, Exception? exception = null)
	{
		var token = new TextToken(msg);
		var properties = new List<LogEventProperty>();
		if (connectorId != 0)
		{
			properties.Add(new("ConnectorId", new ScalarValue(connectorId)));
		}

		var ev = new LogEvent(DateTimeOffset.Now, level, exception, new(new[] { token }), properties);

		Logger.Write(ev);
	}
}
#endif
