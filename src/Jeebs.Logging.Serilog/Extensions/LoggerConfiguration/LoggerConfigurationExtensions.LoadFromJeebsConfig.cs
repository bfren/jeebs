// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;

namespace Jeebs.Logging.Serilog;

public static partial class LoggerConfigurationExtensions
{
	/// <summary>
	/// Load Serilog configuration from JeebsConfig object.
	/// </summary>
	/// <param name="this">Serilog configuration object.</param>
	/// <param name="jeebs">JeebsConfig.</param>
	public static void LoadFromJeebsConfig(this LoggerConfiguration @this, JeebsConfig jeebs)
	{
		// Set the application name
		_ = @this.Enrich.WithProperty(JeebsConfig.Key.ToUpperFirst() + nameof(JeebsConfig.App), jeebs.App.FullName);

		// Set the minimum log level
		_ = @this.MinimumLevel.Is(GetMinimum(null, jeebs.Logging.Minimum));

		// Set custom minimum levels
		foreach (var (key, level) in jeebs.Logging.Overrides)
		{
			_ = @this.MinimumLevel.Override(key, GetMinimum(level, jeebs.Logging.Minimum));
		}

		// Configure providers
		ConfigureProviders(ref @this, jeebs);

		// Enable connectors
		EnableConnectors(@this, jeebs);

		// Set static logger
		StaticLogger.Factory = new(() => new SerilogLogger());
	}
}
