// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using MySqlConnector.Logging;
using Serilog;

namespace Jeebs.Logging.Serilog.MySql;

/// <summary>
/// Connects MySqlConnector log to Serilog
/// </summary>
public sealed class MySqlLoggingConnector : ILoggingConnector
{
	/// <inheritdoc/>
	public string Type =>
		"mysql";

	/// <inheritdoc/>
	public void Enable(LoggerConfiguration serilog, JeebsConfig jeebs) =>
		MySqlConnectorLogManager.Provider = new MySqlLoggerProvider();
}
