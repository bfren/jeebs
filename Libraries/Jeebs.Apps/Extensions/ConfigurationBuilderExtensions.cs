using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
			if (jeebs.AzureKeyVault.IsValid)
			{
				@this.AddAzureKeyVault(
					$"https://{jeebs.AzureKeyVault.Name}.vault.azure.net/",
					jeebs.AzureKeyVault.ClientId,
					jeebs.AzureKeyVault.ClientSecret
				);
			}

			// Return
			return @this;
		}
	}
}
