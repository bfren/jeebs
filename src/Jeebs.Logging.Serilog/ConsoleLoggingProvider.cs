// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;
using Jeebs.Config;
using Jeebs.Config.Services.Console;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog;

/// <summary>
/// Configure logging to console.
/// </summary>
public sealed class ConsoleLoggingProvider : ILoggingProvider
{
	/// <inheritdoc/>
	public string Type =>
		"console";

	/// <inheritdoc/>
	public Result<bool> Configure(LoggerConfiguration logger, JeebsConfig jeebs, string name, LogEventLevel minimum) =>
		jeebs.Services.GetServiceConfig(
			c => c.Console, name
		)
		.IfOk(
			c => AddConsoleConfig(logger, c, jeebs.App.Name)
		)
		.Map(
			_ => true
		);

	internal static void AddDefaultConsoleConfig(LoggerConfiguration logger) =>
		AddConsoleConfig(logger, new() { AddPrefix = false });

	internal static void AddConsoleConfig(LoggerConfiguration logger, ConsoleConfig config, string? name = null)
	{
		if (config.AddPrefix && !string.IsNullOrWhiteSpace(name))
		{
			SerilogLogger.ConsoleMessagePrefix = name;
		}

		_ = logger.WriteTo.Console(
			outputTemplate: config.Template,
			formatProvider: F.DefaultCulture
		);
	}
}
