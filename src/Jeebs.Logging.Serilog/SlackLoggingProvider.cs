// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Slack;

namespace Jeebs.Logging.Serilog;

/// <summary>
/// Configure logging to Slack.
/// </summary>
public sealed class SlackLoggingProvider : ILoggingProvider
{
	/// <inheritdoc/>
	public string Type =>
		"slack";

	/// <inheritdoc/>
	public Result<bool> Configure(LoggerConfiguration logger, JeebsConfig jeebs, string name, LogEventLevel minimum) =>
		jeebs.Services.GetServiceConfig(
			c => c.Slack, name
		)
		.IfOk(
			c => _ = logger.WriteTo.Async(
				a => a.Slack(
					webhookUrl: c.Webhook,
					restrictedToMinimumLevel: minimum,
					formatProvider: F.DefaultCulture
				)
			)
		)
		.Map(
			_ => true
		);
}
