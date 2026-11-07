// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog;

public static partial class LoggerConfigurationExtensions
{
	/// <summary>
	/// Source context prefix used by MySqlConnector's loggers.
	/// </summary>
	internal const string MySqlConnectorSource = "MySqlConnector";

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

		// MySqlConnector logs every command at Debug, so only show its warnings unless the config asks for more
		if (!jeebs.Logging.Overrides.ContainsKey(MySqlConnectorSource))
		{
			_ = @this.MinimumLevel.Override(MySqlConnectorSource, LogEventLevel.Warning);
		}

		// Configure providers
		ConfigureProviders(ref @this, jeebs);

		// Enable connectors
		EnableConnectors(@this, jeebs);

		// Set static logger
		StaticLogger.Factory = new(() => new SerilogLogger());
	}
}
