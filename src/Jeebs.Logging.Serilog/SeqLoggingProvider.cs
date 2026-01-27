// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog;

/// <summary>
/// Configure logging to Seq.
/// </summary>
public sealed class SeqLoggingProvider : ILoggingProvider
{
	/// <inheritdoc/>
	public string Type =>
		"seq";

	/// <inheritdoc/>
	public Result<bool> Configure(LoggerConfiguration logger, JeebsConfig jeebs, string name, LogEventLevel minimum) =>
		jeebs.Services.GetServiceConfig(
			c => c.Seq, name
		)
		.IfOk(
			c => _ = logger.WriteTo.Async(a =>
				a.Seq(
					serverUrl: c.Server,
					apiKey: c.ApiKey,
					restrictedToMinimumLevel: minimum,
					formatProvider: F.DefaultCulture
				)
			)
		)
		.Map(
			_ => true
		);
}
