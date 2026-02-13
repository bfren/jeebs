// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Config;
using Jeebs.Config.Services;
using Jeebs.Functions;
using Serilog;

namespace Jeebs.Logging.Serilog;

public static partial class LoggerConfigurationExtensions
{
	/// <summary>
	/// Configure all enabled <see cref="ILoggingProvider"/> services.
	/// </summary>
	/// <param name="serilog">Seilog configuration object.</param>
	/// <param name="jeebs">JeebsConfig.</param>
	internal static void ConfigureProviders(ref LoggerConfiguration serilog, JeebsConfig jeebs)
	{
		// Get enabled providers
		var enabledProviders = jeebs.Logging.Providers.Where(h => h.Value.Enabled).ToList();

		// If no providers are enabled, add basic console logging and return
		if (enabledProviders.Count == 0)
		{
			ConsoleLoggingProvider.AddDefaultConsoleConfig(serilog);
			return;
		}

		// Get provider services
		var providers = new Dictionary<string, ILoggingProvider>();
		TypeF.GetTypesImplementing<ILoggingProvider>().ForEach(t =>
		{
			if (Activator.CreateInstance(t) is ILoggingProvider p)
			{
				providers.Add(p.Type, p);
			}
		});

		// Configure enabled providers
		var atLeastOneEnabled = false;
		foreach (var (providerInfo, providerConfig) in enabledProviders)
		{
			// Get hook minimum - default minimum will override this if it's higher
			var providerMinimum = GetMinimum(providerConfig.Minimum, jeebs.Logging.Minimum);

			// Get service definition
			var service = from d in ServicesConfig.SplitDefinition(providerInfo)
						  from p in providers.GetValueOrNone(d.type)
						  select (p, d.name);

			if (service is Some<(ILoggingProvider provider, string name)> s)
			{
				// Configure the provider and keep track of whether at least one is successfully configured
				var configure = s.Value.provider.Configure(serilog, jeebs, s.Value.name, providerMinimum);
				atLeastOneEnabled = atLeastOneEnabled || configure.IsTrue();

				// Output any errors to the console
				_ = configure.IfFailed(
					f => Console.WriteLine(f.Message.FormatWith(f.Args))
				);
			}
		}

		// If nothing is configured, add default console config
		if (!atLeastOneEnabled)
		{
			ConsoleLoggingProvider.AddDefaultConsoleConfig(serilog);
		}
	}
}
