// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.Config
{
	/// <summary>
	/// Jeebs Configuration
	/// </summary>
	public record class JeebsConfig
	{
		/// <summary>
		/// Path to Jeebs settings configuration section
		/// </summary>
		public const string Key = "jeebs";

		/// <summary>
		/// App congiguration
		/// </summary>
		public AppConfig App { get; init; } = new();

		/// <summary>
		/// Azure KeyVault congiguration
		/// </summary>
		public AzureKeyVaultConfig AzureKeyVault { get; init; } = new();

		/// <summary>
		/// Data configuration
		/// </summary>
		public DbConfig Db { get; init; } = new();

		/// <summary>
		/// Logging congiguration
		/// </summary>
		public LoggingConfig Logging { get; init; } = new();

		/// <summary>
		/// Services configuration
		/// </summary>
		public ServicesConfig Services { get; init; } = new();

		/// <summary>
		/// Web congiguration
		/// </summary>
		public WebConfig Web { get; init; } = new();

		/// <summary>
		/// WordPress configurations
		/// </summary>
		public Dictionary<string, WpConfig> Wp { get; init; } = new();

		/// <summary>
		/// If key starts with ':', add Jeebs config prefix
		/// </summary>
		/// <param name="key">Section key</param>
		/// <returns>Full config key</returns>
		public static string GetKey(string key) =>
			key.StartsWith(":") ? Key + key : key;
	}
}
