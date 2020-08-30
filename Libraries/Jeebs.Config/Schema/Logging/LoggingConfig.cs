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
	public class LoggingConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":logging";

		/// <summary>
		/// Minimum LogLevel
		/// </summary>
		public LogLevel MinimumLevel { get; set; }

		/// <summary>
		/// If true, log to console
		/// </summary>
		public bool Console { get; set; }

		/// <summary>
		/// List of providers - dictionary key is a service name
		/// </summary>
		public Dictionary<string, LoggingProviderConfig> Providers { get; set; } = new Dictionary<string, LoggingProviderConfig>();
	}
}
