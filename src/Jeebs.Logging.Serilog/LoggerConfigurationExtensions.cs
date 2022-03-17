// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Config;
using Jeebs.Config.Services;
using Jeebs.Extensions;
using Jeebs.Functions;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog;

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
		// If there are no logging hooks, use default configuration
		if (jeebs.Logging.Hooks.Count == 0)
		{
			return;
		}

		// Set the full application name
		_ = @this.Enrich.WithProperty(JeebsConfig.Key.ToUpperFirst() + nameof(JeebsConfig.App), jeebs.App.FullName);

		// Returns minimum level from nullable value or from default
		var getMinimum = LogEventLevel (LogLevel? level) =>
			(LogEventLevel)(level ?? jeebs.Logging.Minimum);

		// Set the minimum log level
		var overallMinimumLevel = getMinimum(null);
		_ = @this.MinimumLevel.Is(overallMinimumLevel);

		// Get all logging hooks
		var hooks = new Dictionary<string, ILoggingHook>();
		TypeF.GetTypesImplementing<ILoggingHook>().ForEach(t =>
		{
			var i = Activator.CreateInstance(t);
			if (i is ILoggingHook h)
			{
				hooks.Add(h.Type, h);
			}
		});

		// Add logging providers
		foreach (var (service, providerConfig) in jeebs.Logging.Hooks)
		{
			// Don't do anything if this provider is not enabled (!)
			if (!providerConfig.Enabled)
			{
				continue;
			}

			// Get service info
			var (serviceType, serviceName) = ServicesConfig.SplitDefinition(service);

			// Get provider minimum
			var providerMinimumLevel = getMinimum(providerConfig.Minimum);

			// Register provider
			if (hooks.TryGetValue(serviceType, out var provider))
			{
				provider.Configure(@this, jeebs, serviceName, providerMinimumLevel);
			}
		}
	}
}
