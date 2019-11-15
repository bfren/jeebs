using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs
{
	/// <summary>
	/// Jeebs Configuration
	/// </summary>
	public sealed class JeebsConfig
	{
		/// <summary>
		/// Default path to Jeebs settings configuration section
		/// </summary>
		internal const string Key = "jeebs";

		/// <summary>
		/// AppConfig object
		/// </summary>
		public AppConfig App { get; set; }

		/// <summary>
		/// LoggingConfig object
		/// </summary>
		public LoggingConfig Logging { get; set; }

		/// <summary>
		/// Create blank objects
		/// </summary>
		public JeebsConfig()
		{
			App = new AppConfig();
			Logging = new LoggingConfig();
		}
	}
}
