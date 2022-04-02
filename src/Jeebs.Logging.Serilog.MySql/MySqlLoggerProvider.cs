// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MySqlConnector.Logging;

namespace Jeebs.Logging.Serilog.MySql;

/// <summary>
/// Create logger instances for MySqlConnector
/// </summary>
public sealed class MySqlLoggerProvider : IMySqlConnectorLoggerProvider
{
	/// <summary>
	/// Create named logger
	/// </summary>
	/// <param name="name">Logger name</param>
	public IMySqlConnectorLogger CreateLogger(string name) =>
		new MySqlLogger(name);
}
