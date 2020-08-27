using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Jeebs Application Configuraiton
	/// </summary>
	public class AppConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":app";

		/// <summary>
		/// Project Name
		/// </summary>
		public string Project { get; set; } = string.Empty;

		/// <summary>
		/// Project Version
		/// </summary>
		public Version Version { get; set; } = new Version(0, 1, 0, 0);
	}
}
