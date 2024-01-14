// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Logging;
using Microsoft.Extensions.Options;

namespace Jeebs.Config.Logging;

/// <summary>
/// Logging configuration
/// </summary>
public sealed record class LoggingConfig : IOptions<LoggingConfig>
{
	/// <summary>
	/// Path to this configuration section
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":logging";

	/// <summary>
	/// Overall Minimum LogLevel - no log event below this will be logged
	/// </summary>
	public LogLevel Minimum { get; init; }

	/// <summary>
	/// Override minimum levels for sources beginning with dictionary key
	/// </summary>
	public Dictionary<string, LogLevel> Overrides { get; init; } = [];

	/// <summary>
	/// List of providers - dictionary key is a service name
	/// </summary>
	public Dictionary<string, LoggingProviderConfig> Providers { get; init; } = [];

	/// <inheritdoc/>
	LoggingConfig IOptions<LoggingConfig>.Value =>
		this;
}
