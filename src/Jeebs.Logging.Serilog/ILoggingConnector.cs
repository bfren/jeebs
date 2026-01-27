// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;

namespace Jeebs.Logging.Serilog;

/// <summary>
/// Allows logging users to hook into the main logging configuration -
/// see <see cref="LoggerConfigurationExtensions.LoadFromJeebsConfig(LoggerConfiguration, JeebsConfig)"/>
/// </summary>
public interface ILoggingConnector
{
	/// <summary>
	/// Enable this user.
	/// </summary>
	/// <param name="serilog">Serilog configuration object.</param>
	/// <param name="jeebs">JeebsConfig.</param>
	void Enable(LoggerConfiguration serilog, JeebsConfig jeebs);
}
