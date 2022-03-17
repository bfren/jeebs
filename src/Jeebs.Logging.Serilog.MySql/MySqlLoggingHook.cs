// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using MySqlConnector.Logging;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog.MySql;

/// <summary>
/// Connects MySqlConnector log to Serilog
/// </summary>
public sealed class MySqlLoggingHook : ILoggingHook
{
	/// <inheritdoc/>
	public string Type =>
		"mysql";

	/// <inheritdoc/>
	public void Configure(LoggerConfiguration logger, JeebsConfig jeebs, string name, LogEventLevel minimum) =>
		MySqlConnectorLogManager.Provider = new MySqlLoggerProvider(minimum);
}
