// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Microsoft.Extensions.Logging;
using Npgsql;
using Serilog;

namespace Jeebs.Logging.Serilog.PostgreSql;

/// <summary>
/// Connects Npgsql log to Serilog.
/// </summary>
public sealed class PostgreSqlLoggingConnector : ILoggingConnector
{
	/// <inheritdoc/>
	public void Enable(LoggerConfiguration serilog, JeebsConfig jeebs) =>
		NpgsqlLoggingConfiguration.InitializeLogging(
			loggerFactory: LoggerFactory.Create(builder => builder.AddSerilog()),
			parameterLoggingEnabled: true
		);
}
