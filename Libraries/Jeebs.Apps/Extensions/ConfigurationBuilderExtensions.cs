using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps
{
	/// <summary>
	/// IConfigurationBuilder extension methods
	/// </summary>
	public static class ConfigurationBuilderExtensions
	{
		/// <summary>
		/// Add App and Jeebs configuration JSON files to IConfigurationBuilder
		/// </summary>
		/// <param name="builder">IConfigurationBuilder</param>
		/// <param name="env">IHostEnvironment</param>
		public static IConfigurationBuilder AddJeebsConfig(this IConfigurationBuilder builder, in IHostEnvironment env)
		{
			// Add Jeebs config - keeps Jeebs config away from app settings
			builder.AddJsonFile("jeebsconfig.json", optional: false);
			builder.AddJsonFile($"jeebsconfig.{env.EnvironmentName}.json", optional: true);
			builder.AddJsonFile("jeebsconfig-secrets.json", optional: false);

			// Add Environment Variables
			builder.AddEnvironmentVariables();

			// Check for Azure Key Vault section
			var jeebs = builder.Build().GetSection<JeebsConfig>(JeebsConfig.Key);

			// If the config is valid, add Azure Key Vault to IConfigurationBuilder
			if (jeebs.AzureKeyVault.IsValid)
			{
				builder.AddAzureKeyVault(
					$"https://{jeebs.AzureKeyVault.Name}.vault.azure.net/",
					jeebs.AzureKeyVault.ClientId,
					jeebs.AzureKeyVault.ClientSecret
				);
			}

			// Return
			return builder;
		}
	}
}
