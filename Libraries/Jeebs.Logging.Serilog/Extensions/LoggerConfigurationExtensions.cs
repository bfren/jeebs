using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Jeebs.Config;
using Jeebs.Config.Logging;
using Microsoft.Extensions.Logging;
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
		/// <param name="configuration">LoggerConfiguration</param>
		/// <param name="jeebs">JeebsConfig</param>
		public static void LoadFromJeebsConfig(this LoggerConfiguration configuration, JeebsConfig jeebs)
		{
			// If there are no logging providers, return default configuration
			if (jeebs.Logging.Providers is null)
			{
				return;
			}

			LogEventLevel getMinimum(LogLevel? level = null)
				=> (LogEventLevel)(level ?? jeebs.Logging.MinimumLevel);

			// Set the minimum log level
			var overallMinimumLevel = getMinimum();
			configuration.MinimumLevel.Is(overallMinimumLevel);

			// Check for console provider
			if (jeebs.Logging.Console)
			{
				configuration.WriteTo.Console(restrictedToMinimumLevel: overallMinimumLevel);
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
				var split = service.Split('.');
				var serviceType = split[0];
				var serviceName = split[1];

				// Get provider minimum
				var providerMinimumLevel = getMinimum(providerConfig.MinimumLevel);

				// Get service config
				if (serviceType == "seq")
				{
					var seq = jeebs.Services.Seq[serviceName];
					configuration.WriteTo.Async(a => a.Seq(seq.Server, apiKey: seq.ApiKey, compact: true, restrictedToMinimumLevel: providerMinimumLevel));
				}
				else if (serviceType == "slack")
				{
					var slack = jeebs.Services.Slack[serviceName];
					configuration.WriteTo.Async(a => a.Slack(slack.Webhook, restrictedToMinimumLevel: providerMinimumLevel));
				}
			}
		}
	}
}
