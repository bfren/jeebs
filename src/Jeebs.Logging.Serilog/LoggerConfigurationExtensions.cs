// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
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
		// Get enabled hooks
		var enabledHooks = jeebs.Logging.Hooks.Where(h => h.Value.Enabled).ToList();
		if (enabledHooks.Count == 0)
		{
			return;
		}

		// Set the full application name
		_ = @this.Enrich.WithProperty(JeebsConfig.Key.ToUpperFirst() + nameof(JeebsConfig.App), jeebs.App.FullName);

		// Returns minimum level from nullable value or from default
		var getMinimum = LogEventLevel (LogLevel? level) =>
			(LogEventLevel)(level ?? jeebs.Logging.Minimum);

		// Set the minimum log level
		_ = @this.MinimumLevel.Is(getMinimum(null));

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

		// Add logging hooks
		foreach (var (service, hookConfig) in enabledHooks)
		{
			// Get service info
			var (serviceType, serviceName) = ServicesConfig.SplitDefinition(service);

			// Get hook minimum - will override @this.MinimumLevel if it's a higher value
			var hookMinimumLevel = getMinimum(hookConfig.Minimum);

			// Configure hook
			if (hooks.TryGetValue(serviceType, out var hook))
			{
				hook.Configure(@this, jeebs, serviceName, hookMinimumLevel);
			}
		}
	}
}
