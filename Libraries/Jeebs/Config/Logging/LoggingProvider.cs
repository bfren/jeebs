using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Jeebs.Config.Logging
{
	/// <summary>
	/// Abstract Logging Provider
	/// </summary>
	public abstract class LoggingProvider
	{
		/// <summary>
		/// Whether or not this provider is enabled
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// This provider's minimum log level
		/// </summary>
		public LogLevel? MinimumLevel { get; set; }

		/// <summary>
		/// Whether or not this provider's configuraiton is valid
		/// </summary>
		/// <returns>True if configuration is valid</returns>
		public abstract bool IsValid();
	}
}
