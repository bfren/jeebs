using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Jeebs Configuration
	/// </summary>
	public class JeebsConfig
	{
		/// <summary>
		/// Path to Jeebs settings configuration section
		/// </summary>
		public const string Key = "jeebs";

		/// <summary>
		/// App congiguration
		/// </summary>
		public AppConfig App { get; set; } = new AppConfig();

		/// <summary>
		/// Azure KeyVault congiguration
		/// </summary>
		public AzureKeyVaultConfig AzureKeyVault { get; set; } = new AzureKeyVaultConfig();

		/// <summary>
		/// Data configuration
		/// </summary>
		public DbConfig Db { get; set; } = new DbConfig();

		/// <summary>
		/// Logging congiguration
		/// </summary>
		public LoggingConfig Logging { get; set; } = new LoggingConfig();

		/// <summary>
		/// Notifier congiguration
		/// </summary>
		public NotifierConfig Notifier { get; set; } = new NotifierConfig();

		/// <summary>
		/// Services configuration
		/// </summary>
		public ServicesConfig Services { get; set; } = new ServicesConfig();

		/// <summary>
		/// Web congiguration
		/// </summary>
		public WebConfig Web { get; set; } = new WebConfig();

		/// <summary>
		/// If key starts with ':', add Jeebs config prefix
		/// </summary>
		/// <param name="key">Section key</param>
		/// <returns>Full config key</returns>
		public static string GetKey(string key)
			=> key.StartsWith(":") ? Key + key : key;
	}
}
