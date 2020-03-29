using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config.Logging;
using Microsoft.Extensions.Logging;

namespace Jeebs.Config
{
	/// <summary>
	/// Logging configuration
	/// </summary>
	public sealed class LoggingConfig
	{
		/// <summary>
		/// Minimum LogLevel
		/// </summary>
		public LogLevel? MinimumLevel { get; set; }

		/// <summary>
		/// Providers Configuration
		/// </summary>
		public LoggingProviders Providers { get; set; } = new LoggingProviders();
	}
}
