// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Azure.Identity;
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
		/// <param name="this">IConfigurationBuilder</param>
		/// <param name="env">IHostEnvironment</param>
		public static IConfigurationBuilder AddJeebsConfig(this IConfigurationBuilder @this, IHostEnvironment env)
		{
			// Validate main configuration file
			var path = $"{env.ContentRootPath}/jeebsconfig.json";
			ConfigValidator.Validate(path);

			// Add Jeebs config - keeps Jeebs config away from app settings
			@this
				.AddJsonFile($"{env.ContentRootPath}/jeebsconfig.json", optional: false)
				.AddJsonFile($"{env.ContentRootPath}/jeebsconfig-secrets.json", optional: false);

			// Add environment-specific Jeebs config
			@this
				.AddJsonFile($"{env.ContentRootPath}/jeebsconfig.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile($"{env.ContentRootPath}/jeebsconfig-secrets.{env.EnvironmentName}.json", optional: true);

			// Add Environment Variables
			@this.AddEnvironmentVariables();

			// Check for Azure Key Vault section
			var jeebs = @this.Build().GetSection<JeebsConfig>(JeebsConfig.Key, false);

			// If the config is valid, add Azure Key Vault to IConfigurationBuilder
			if (jeebs.AzureKeyVault is var vault && vault.IsValid)
			{
				@this.AddAzureKeyVault(
					new Uri($"https://{vault.Name}.vault.azure.net/"),
					new ClientSecretCredential(vault.TenantId, vault.ClientId, vault.ClientSecret)
				);
			}

			// Return
			return @this;
		}
	}
}
