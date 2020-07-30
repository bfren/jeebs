using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
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
		public AppConfig App { get; set; } = new AppConfig();

		/// <summary>
		/// AzureKeyVault object
		/// </summary>
		public AzureKeyVaultConfig AzureKeyVault { get; set; } = new AzureKeyVaultConfig();

		/// <summary>
		/// LoggingConfig object
		/// </summary>
		public LoggingConfig Logging { get; set; } = new LoggingConfig();

		/// <summary>
		/// If key starts with ':', add Jeebs config prefix
		/// </summary>
		/// <param name="key">Section key</param>
		/// <returns>Full config key</returns>
		public static string GetKey(string key)
			=> key.StartsWith(":") ? Key + key : key;
	}
}
