// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MySqlConnector.Logging;
using Serilog.Events;

namespace Jeebs.Logging.Serilog.MySql;

/// <summary>
/// Create logger instances for MySqlConnector
/// </summary>
public sealed class MySqlLoggerProvider : IMySqlConnectorLoggerProvider
{
	/// <summary>
	/// Minimum log level
	/// </summary>
	internal LogEventLevel Minimum { get; init; }

	/// <summary>
	/// Store minimum log level
	/// </summary>
	/// <param name="minimumLevel">Minimum log level</param>
	public MySqlLoggerProvider(LogEventLevel minimumLevel) =>
		Minimum = minimumLevel;

	/// <summary>
	/// Create named logger, passing minimum log level
	/// </summary>
	/// <param name="name">Logger name</param>
	public IMySqlConnectorLogger CreateLogger(string name) =>
		new MySqlLogger(name, Minimum);
}
