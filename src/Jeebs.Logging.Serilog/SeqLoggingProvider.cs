// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;
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
	public void Configure(LoggerConfiguration logger, JeebsConfig jeebs, string name, LogEventLevel minimum)
	{
		var config = jeebs.Services.GetServiceConfig(c => c.Seq, name);
		_ = logger.WriteTo.Async(a => a.Seq(
			serverUrl: config.Server,
			apiKey: config.ApiKey,
			restrictedToMinimumLevel: minimum,
			formatProvider: CultureInfo.InvariantCulture
		));
	}
}
