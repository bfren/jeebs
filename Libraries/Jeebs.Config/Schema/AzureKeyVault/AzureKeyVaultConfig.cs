using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Azure Key Vault Configuration
	/// These values should only ever be set in jeebsconfig-secrets.json to avoid them being checked into version control
	/// </summary>
	public class AzureKeyVaultConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":azureKeyVault";

		/// <summary>
		/// Azure Key Vault Name
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Client ID
		/// </summary>
		public string ClientId { get; set; } = string.Empty;

		/// <summary>
		/// Client Secret
		/// </summary>
		public string ClientSecret { get; set; } = string.Empty;

		/// <summary>
		/// Only returns True if <see cref="Name"/>, <see cref="ClientId"/> and <see cref="ClientSecret"/> are all not null or empty
		/// </summary>
		public bool IsValid
			=> !string.IsNullOrWhiteSpace(Name)
			&& !string.IsNullOrWhiteSpace(ClientId)
			&& !string.IsNullOrWhiteSpace(ClientSecret);
	}
}
