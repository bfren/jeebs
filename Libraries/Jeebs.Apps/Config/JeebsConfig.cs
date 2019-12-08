using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Apps.Config
{
	/// <summary>
	/// Jeebs Configuration
	/// </summary>
	public sealed class JeebsConfig
	{
		/// <summary>
		/// Default path to Jeebs settings configuration section
		/// </summary>
		public const string Key = "jeebs";

		/// <summary>
		/// AppConfig object
		/// </summary>
		public AppConfig App { get; set; }

		/// <summary>
		/// AzureKeyVault object
		/// </summary>
		public AzureKeyVaultConfig AzureKeyVault { get; set; }

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
			AzureKeyVault = new AzureKeyVaultConfig();
			Logging = new LoggingConfig();
		}

		/// <summary>
		/// If key starts with ':', add Jeebs config prefix
		/// </summary>
		/// <param name="key">Section key</param>
		/// <returns>Full config key</returns>
		public static string GetKey(string key ) => key.StartsWith(":") ? Key + key : key;
	}
}
