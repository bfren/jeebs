// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Logging;

namespace Jeebs.Config.Logging
{
	/// <summary>
	/// Logging Provider
	/// </summary>
	public record LoggingProviderConfig
	{
		/// <summary>
		/// Whether or not this provider is enabled
		/// </summary>
		public bool Enabled { get; init; }

		/// <summary>
		/// This provider's minimum log level (overrides the default minimum level in main Logging section)
		/// </summary>
		public LogLevel? MinimumLevel { get; init; }
	}
}
