// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog;

/// <summary>
/// Configure logging to console
/// </summary>
public sealed class ConsoleLoggingHook : ILoggingHook
{
	/// <inheritdoc/>
	public string Type =>
		"console";

	/// <inheritdoc/>
	public void Configure(LoggerConfiguration logger, JeebsConfig jeebs, string name, LogEventLevel minimum)
	{
		var config = jeebs.Services.GetServiceConfig(c => c.Console, name);

		if (config.AddPrefix)
		{
			SerilogLogger.ConsoleMessagePrefix = jeebs.App.FullName;
		}

		_ = logger.WriteTo.Console(
			restrictedToMinimumLevel: minimum,
			outputTemplate: config.Template ?? "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} | {SourceContext}{NewLine}{Exception}"
		);
	}
}
