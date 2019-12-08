﻿using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Apps.Config;
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
			var azureSection = builder.Build().GetSection("azureKeyVault");
			if (azureSection.Value == null)
			{
				return builder;
			}

			// Bind config to object
			var azure = new AzureKeyVaultConfig();
			azureSection.Bind(azure);

			// If the config is valid, add Azure Key Vault to IConfigurationBuilder
			if (azure.IsValid)
			{
				builder.AddAzureKeyVault($"https://{azure.Name}.vault.azure.net/", azure.ClientId, azure.ClientSecret);
			}

			// Return
			return builder;
		}
	}
}
