// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data.SQLite;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Jeebs.Logging.Serilog.Sqlite;

/// <summary>
/// Sends System.Data.SQLite's log and connection events to an <see cref="ILogger"/>.
/// </summary>
/// <remarks>
/// System.Data.SQLite has no ILogger support: it raises static (process-wide) events instead,
/// so the handlers are added once and write to the most recently enabled logger factory.
/// </remarks>
internal static partial class SqliteLogging
{
	/// <summary>
	/// Logger category, used in Jeebs logging overrides.
	/// </summary>
	internal const string Category = "System.Data.SQLite";

	private static Lazy<ILogger> logger = new(() => Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance);

	private static int subscribed;

	/// <summary>
	/// Start sending System.Data.SQLite events to a logger from <paramref name="factory"/>.
	/// </summary>
	/// <remarks>
	/// The logger is created on the first event, not now: connectors are enabled while Serilog is still
	/// being configured, and a Serilog logger created before the host sets <c>Log.Logger</c> writes nothing.
	/// </remarks>
	/// <param name="factory">ILoggerFactory.</param>
	internal static void Enable(ILoggerFactory factory)
	{
		logger = new(() => factory.CreateLogger(Category));

		if (Interlocked.Exchange(ref subscribed, 1) == 0)
		{
			SQLiteLog.Log += (_, e) => Write(logger.Value, e);
			SQLiteConnection.Changed += (_, e) => Write(logger.Value, e);
		}
	}

	/// <summary>
	/// Write a message from the SQLite engine's own log.
	/// </summary>
	/// <param name="log">ILogger.</param>
	/// <param name="e">LogEventArgs.</param>
	internal static void Write(ILogger log, LogEventArgs e)
	{
		var code = e.ErrorCode switch
		{
			int i =>
				i,

			SQLiteErrorCode c =>
				(int)c,

			_ =>
				-1
		};

		// the low byte is the primary result code (27 = SQLITE_NOTICE, 28 = SQLITE_WARNING)
		var level = (code & 0xFF) switch
		{
			0 or 100 or 101 =>
				LogLevel.Debug,

			27 =>
				LogLevel.Information,

			28 =>
				LogLevel.Warning,

			_ =>
				LogLevel.Error
		};

		LogEngineMessage(log, level, e.Message, code);
	}

	/// <summary>
	/// Write a connection event - opening connections, and the SQL of each statement run.
	/// </summary>
	/// <remarks>
	/// Other events are ignored: most are noise, and some carry the connection string (which may contain a password).
	/// Closed isn't logged because System.Data.SQLite also raises it at the start of every Open.
	/// Statements are logged as written, with parameter names but not their values.
	/// </remarks>
	/// <param name="log">ILogger.</param>
	/// <param name="e">ConnectionEventArgs.</param>
	internal static void Write(ILogger log, ConnectionEventArgs e)
	{
		if (e.EventType == SQLiteConnectionEventType.Opened)
		{
			LogOpened(log);
		}
		else if (e.EventType == SQLiteConnectionEventType.NewDataReader && e.Command is not null)
		{
			LogExecuting(log, e.Command.CommandText);
		}
	}

	[LoggerMessage(Message = "{Message} (SQLite result code {Code})")]
	private static partial void LogEngineMessage(ILogger log, LogLevel level, string message, int code);

	[LoggerMessage(Level = LogLevel.Debug, Message = "Opened connection.")]
	private static partial void LogOpened(ILogger log);

	[LoggerMessage(Level = LogLevel.Debug, Message = "Executing {Sql}")]
	private static partial void LogExecuting(ILogger log, string sql);
}
