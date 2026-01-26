// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;
using Serilog.Events;

namespace Jeebs.Logging.Serilog;

/// <summary>
/// Allows logging providers to hook into the main logging configuration -
/// see <see cref="LoggerConfigurationExtensions.LoadFromJeebsConfig(LoggerConfiguration, JeebsConfig)"/>
/// </summary>
public interface ILoggingProvider
{
	/// <summary>
	/// Type name (e.g. 'slack').
	/// </summary>
	string Type { get; }

	/// <summary>
	/// Configure this provider.
	/// </summary>
	/// <param name="logger">Serilog configuration object..</param>
	/// <param name="jeebs">JeebsConfig.</param>
	/// <param name="name">The service name (e.g. 'slack.dev').</param>
	/// <param name="minimum">Minimum logging level.</param>
	Result<bool> Configure(LoggerConfiguration logger, JeebsConfig jeebs, string name, LogEventLevel minimum);
}
