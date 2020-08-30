using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Jeebs.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
		public static IConfigurationBuilder AddJeebsConfig(this IConfigurationBuilder builder, IHostEnvironment env)
		{
			// Add Jeebs config - keeps Jeebs config away from app settings
			builder.AddJsonFile("jeebsconfig.json".Verify(), optional: false);
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

		private static string Verify(this string file, bool optional = false)
		{
			var configPath = $"{Directory.GetCurrentDirectory()}\\{file}";
			var schemaPath = $"{Directory.GetCurrentDirectory()}\\schema.json";

			if (!File.Exists(configPath))
			{
				if (!optional)
				{
					throw new FileNotFoundException("Jeebs configuration file not found.", configPath);
				}

				return file;
			}

			if (!File.Exists(schemaPath))
			{
				throw new FileNotFoundException("Jeebs schema file not found.", schemaPath);
			}

			using var configFile = File.OpenText(configPath);
			using var schemaFile = File.OpenText(schemaPath);

			using var configReader = new JsonTextReader(configFile);
			using var schemaReader = new JsonTextReader(schemaFile);

			var config = JToken.ReadFrom(configReader);
			var schema = JSchema.Load(schemaReader);

			if (!config.IsValid(schema, out IList<string> errors))
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine($"Invalid Jeebs configuration file: {configPath}.");
				foreach (var item in errors)
				{
					sb.AppendLine(item);
				}

				throw new Jx.ConfigException(sb.ToString());
			}

			return file;
		}
	}
}
