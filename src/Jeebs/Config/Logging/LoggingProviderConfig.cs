// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Wrap.Logging;

namespace Jeebs.Config.Logging;

/// <summary>
/// Logging Provider.
/// </summary>
public sealed record class LoggingProviderConfig
{
	/// <summary>
	/// Whether or not this provider is enabled.
	/// </summary>
	public bool Enabled { get; init; }

	/// <summary>
	/// This provider's minimum log level (overrides the default minimum level in main Logging section, if it's higher).
	/// </summary>
	public LogLevel? Minimum { get; init; }
}
