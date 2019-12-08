using System;
using System.Collections.Generic;
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
		/// <param name="config">LoggerConfiguration</param>
		/// <param name="jeebs">JeebsConfig</param>
		public static void LoadFromJeebsConfig(this LoggerConfiguration config, JeebsConfig jeebs)
		{
			// If there are no logging providers, return default configuration
			if (jeebs.Logging.Providers == null)
			{
				return;
			}

			// Set the minimum log level
			const LogLevel defaultLevel = LogLevel.Information;
			var minimumLevel = F.EnumF.Convert(jeebs.Logging.MinimumLevel ?? defaultLevel).To<LogEventLevel>();
			config.MinimumLevel.Is(minimumLevel);

			// Add a provider to the Serilog configuration
			void AddProvider<T>(Func<LoggingProviders, T> get, Action<T> configure)
				where T : LoggingProvider
			{
				if (get(jeebs.Logging.Providers) is T provider && provider.Enabled && provider.IsValid())
				{
					configure(provider);
				}
			}

			// Add each provider
			AddProvider(
				providers => providers.Console,
				_ => config.WriteTo.Console(restrictedToMinimumLevel: minimumLevel)
			);

			AddProvider(
				providers => providers.File,
				file => config.WriteTo.Async(a => a.File(file.Path, restrictedToMinimumLevel: minimumLevel, rollingInterval: RollingInterval.Day))
			);

			AddProvider(
				providers => providers.Slack,
				slack => config.WriteTo.Async(a => a.Slack(slack.Webhook, restrictedToMinimumLevel: minimumLevel))
			);
		}
	}
}
