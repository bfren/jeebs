// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Config;
using Jeebs.Config.Services;
using Jeebs.Extensions;
using Jeebs.Functions;
using Jeebs.Logging.Serilog.Exceptions;
using Jeebs.Logging.Serilog.Functions;
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
	/// <exception cref="LoadFromJeebsConfigException"></exception>
	public static void LoadFromJeebsConfig(this LoggerConfiguration @this, JeebsConfig jeebs)
	{
		// Set the application name
		_ = @this.Enrich.WithProperty(JeebsConfig.Key.ToUpperFirst() + nameof(JeebsConfig.App), jeebs.App.FullName);

		// Set the minimum log level
		_ = @this.MinimumLevel.Is(GetMinimum(null, jeebs.Logging.Minimum));

		// Configure providers
		ConfigureProviders(ref @this, jeebs);

		// Enable connectors
		EnableConnectors(@this, jeebs);
	}

	/// <summary>
	/// Returns <paramref name="testMinimum"/> if it is not null and greater than <paramref name="overallMinimum"/> -
	/// otherwise returns <paramref name="overallMinimum"/>
	/// </summary>
	/// <param name="testMinimum"></param>
	/// <param name="overallMinimum"></param>
	/// <exception cref="LoadFromJeebsConfigException"></exception>
	internal static LogEventLevel GetMinimum(LogLevel? testMinimum, LogLevel overallMinimum)
	{
		var min = testMinimum switch
		{
			LogLevel individualMinimum when individualMinimum > overallMinimum =>
				individualMinimum,

			_ =>
				overallMinimum
		};

		return LevelF
			.ConvertToSerilogLevel(min)
			.Unwrap(r => throw new LoadFromJeebsConfigException(r));
	}

	/// <summary>
	/// Configure all enabled <see cref="ILoggingProvider"/> services
	/// </summary>
	/// <param name="serilog"></param>
	/// <param name="jeebs"></param>
	internal static void ConfigureProviders(ref LoggerConfiguration serilog, JeebsConfig jeebs)
	{
		// Get enabled providers
		var enabledProviders = jeebs.Logging.Providers.Where(h => h.Value.Enabled).ToList();

		// If no providers are enabled, add basic console logging and return
		if (enabledProviders.Count == 0)
		{
			_ = serilog.WriteTo.Console();
			return;
		}

		// Get provider services
		var providers = new Dictionary<string, ILoggingProvider>();
		TypeF.GetTypesImplementing<ILoggingProvider>().ForEach(t =>
		{
			var i = Activator.CreateInstance(t);
			if (i is ILoggingProvider h)
			{
				providers.Add(h.Type, h);
			}
		});

		// Configure enabled providers
		foreach (var (providerInfo, providerConfig) in enabledProviders)
		{
			// Get service definition
			var (serviceType, serviceName) = ServicesConfig.SplitDefinition(providerInfo);

			// Get hook minimum - default minimum will override this if it's higher
			var providerMinimum = GetMinimum(providerConfig.Minimum, jeebs.Logging.Minimum);

			// Configure provider
			if (providers.TryGetValue(serviceType, out var provider))
			{
				provider.Configure(ref serilog, jeebs, serviceName, providerMinimum);
			}
		}
	}

	/// <summary>
	/// Enable all <see cref="ILoggingConnector"/> services
	/// </summary>
	/// <param name="serilog"></param>
	/// <param name="jeebs"></param>
	internal static void EnableConnectors(LoggerConfiguration serilog, JeebsConfig jeebs)
	{
		// Get all connectors
		var connectors = new List<ILoggingConnector>();
		TypeF.GetTypesImplementing<ILoggingConnector>().ForEach(t =>
		{
			var i = Activator.CreateInstance(t);
			if (i is ILoggingConnector h)
			{
				connectors.Add(h);
			}
		});

		// Enable each connector
		foreach (var connector in connectors)
		{
			connector.Enable(serilog, jeebs);
		}
	}
}
