using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

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
		public bool Enabled { get; set; }

		/// <summary>
		/// This provider's minimum log level (overrides the default minimum level in main Logging section)
		/// </summary>
		public LogLevel? MinimumLevel { get; set; }
	}
}
