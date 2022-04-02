// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Logging.Serilog.Exceptions;

/// <summary>
/// See <see cref="LoggerConfigurationExtensions.LoadFromJeebsConfig(Serilog.LoggerConfiguration, Config.JeebsConfig)"/>
/// </summary>
public class LoadFromJeebsConfigException : Exception
{
	/// <summary>
	/// Create
	/// </summary>
	public LoadFromJeebsConfigException() { }

	/// <summary>
	/// Create with reason message
	/// </summary>
	/// <param name="reason"></param>
	public LoadFromJeebsConfigException(IMsg reason) : this(reason.ToString() ?? string.Empty) { }

	/// <summary>
	/// Create with message
	/// </summary>
	/// <param name="message"></param>
	public LoadFromJeebsConfigException(string message) : base(message) { }

	/// <summary>
	/// Create with message and inner exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public LoadFromJeebsConfigException(string message, Exception inner) : base(message, inner) { }
}
