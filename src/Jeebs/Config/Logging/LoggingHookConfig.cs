// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Config.Logging;

/// <summary>
/// Logging Hook
/// </summary>
public sealed record class LoggingHookConfig
{
	/// <summary>
	/// Whether or not this hook is enabled
	/// </summary>
	public bool Enabled { get; init; }

	/// <summary>
	/// This hook's minimum log level (overrides the default minimum level in main Logging section, if it's higher)
	/// </summary>
	public LogLevel? Minimum { get; init; }
}
