// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Slack;

namespace Jeebs.Logging
{
	/// <summary>
	/// LoggerConfiguration extension methods
	/// </summary>
	public static class LoggerConfigurationExtensions
	{
		/// <summary>
		/// Load Serilog configuration from JeebsConfig object
		/// </summary>
		/// <param name="this">LoggerConfiguration</param>
		/// <param name="jeebs">JeebsConfig</param>
		public static void LoadFromJeebsConfig(this LoggerConfiguration @this, JeebsConfig jeebs)
		{
			// If there are no logging providers, use default configuration
			if (jeebs.Logging.Providers.Count == 0)
			{
				return;
			}

			// Set the full application name
			@this.Enrich.WithProperty(JeebsConfig.Key.ToUpperFirst() + nameof(JeebsConfig.App), jeebs.App.FullName);
			if (jeebs.Logging.AddPrefixToConsoleMessages)
			{
				SerilogLogger.ConsoleMessagePrefix = jeebs.App.FullName;
			}

			// Set the minimum log level
			var overallMinimumLevel = getMinimum();
			@this.MinimumLevel.Is(overallMinimumLevel);

			// Check for console provider
			if (jeebs.Logging.Console)
			{
				@this.WriteTo.Console(
					restrictedToMinimumLevel: overallMinimumLevel,
					outputTemplate: jeebs.Logging.ConsoleOutputTemplate
				);
			}

			// Add providers
			foreach (var (service, providerConfig) in jeebs.Logging.Providers)
			{
				// Don't do anything if this provider is not enabled (!)
				if (!providerConfig.Enabled)
				{
					continue;
				}

				// Get service info
				var (serviceType, serviceName) = ServicesConfig.SplitDefinition(service);

				// Get provider minimum
				var providerMinimumLevel = getMinimum(providerConfig.MinimumLevel);

				// Get service config
				switch (serviceType)
				{
					case "seq":
						var seq = jeebs.Services.GetServiceConfig(c => c.Seq, serviceName);
						@this.WriteTo.Async(a => a.Seq(seq.Server, apiKey: seq.ApiKey, restrictedToMinimumLevel: providerMinimumLevel));
						break;

					case "slack":
						var slack = jeebs.Services.GetServiceConfig(c => c.Slack, serviceName);
						@this.WriteTo.Async(a => a.Slack(slack.Webhook, restrictedToMinimumLevel: providerMinimumLevel));
						break;
				}
			}

			// Returns minimum level from nullable value or from default
			LogEventLevel getMinimum(LogLevel? level = null) =>
				(LogEventLevel)(level ?? jeebs.Logging.MinimumLevel);
		}
	}
}
