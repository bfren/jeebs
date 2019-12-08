using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Apps.Config
{
	/// <summary>
	/// Azure Key Vault Configuration
	/// These values should only ever be set in jeebsconfig-secrets.json to avoid them being checked into version control
	/// </summary>
	public sealed class AzureKeyVaultConfig
	{
		/// <summary>
		/// Azure Key Vault Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Client ID
		/// </summary>
		public string ClientId { get; set; }

		/// <summary>
		/// Client Secret
		/// </summary>
		public string ClientSecret { get; set; }

		/// <summary>
		/// Only returns True if <see cref="Name"/>, <see cref="ClientId"/> and <see cref="ClientSecret"/> are all not null
		/// </summary>
		public bool IsValid { get => !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientSecret)); }

		/// <summary>
		/// Initialise values
		/// </summary>
		public AzureKeyVaultConfig()
		{
			Name = string.Empty;
			ClientId = string.Empty;
			ClientSecret = string.Empty;
		}
	}
}
