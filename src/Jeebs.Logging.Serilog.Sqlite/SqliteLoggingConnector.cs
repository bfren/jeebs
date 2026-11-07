// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Jeebs.Logging.Serilog.Sqlite;

/// <summary>
/// Connects System.Data.SQLite log to Serilog.
/// </summary>
public sealed class SqliteLoggingConnector : ILoggingConnector
{
	/// <inheritdoc/>
	public void Enable(LoggerConfiguration serilog, JeebsConfig jeebs) =>
		SqliteLogging.Enable(
			LoggerFactory.Create(builder => builder.AddSerilog()).CreateLogger(SqliteLogging.Category)
		);
}
