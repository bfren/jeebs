using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Apps.Config.Logging;
using Microsoft.Extensions.Logging;

namespace Jeebs.Apps.Config
{
	public sealed class LoggingConfig
	{
		/// <summary>
		/// Minimum LogLevel
		/// </summary>
		public LogLevel? MinimumLevel { get; set; }

		/// <summary>
		/// Providers Configuration
		/// </summary>
		public LoggingProviders Providers { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		public LoggingConfig()
		{
			Providers = new LoggingProviders();
		}
	}
}
