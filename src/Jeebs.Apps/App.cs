// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IO;
using Jeebs.Config;
using Jeebs.Config.App;
using Jeebs.Config.Azure.DataProtection;
using Jeebs.Config.Azure.KeyVault;
using Jeebs.Config.Db;
using Jeebs.Config.Logging;
using Jeebs.Config.Services;
using Jeebs.Config.Web;
using Jeebs.Config.Web.Auth;
using Jeebs.Config.Web.Auth.Jwt;
using Jeebs.Config.Web.Redirections;
using Jeebs.Config.Web.Verification;
using Jeebs.Logging;
using Jeebs.Logging.Serilog;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jeebs.Apps;

/// <summary>
/// Configure and run an application using <seealso cref="IHost"/>
/// </summary>
public class App
{
	/// <summary>
	/// Runs when the application is ready to go but before it is run
	/// </summary>
	/// <param name="services">IServiceProvider</param>
	/// <param name="log">ILog</param>
	public virtual void Ready(IServiceProvider services, ILog log)
	{
		// Set Maybe Audit log
		F.DefaultLogger = e => log.Err(e, "Maybe Exception: {Exception}.");

		// Log application is ready
		log.Inf("Application ready.");
	}

	/// <summary>
	/// Configure Host
	/// </summary>
	/// <param name="config">IConfigurationBuilder</param>
	public virtual void ConfigureHost(IConfigurationBuilder config) { }

	/// <summary>
	/// Configure App
	/// </summary>
	/// <param name="ctx">HostBuilderContext</param>
	/// <param name="config">IConfigurationBuilder</param>
	public virtual void ConfigureApp(HostBuilderContext ctx, IConfigurationBuilder config)
	{
		// Shortcut for environment
		var env = ctx.HostingEnvironment;

		// Validate main configuration file
		var path = $"{env.ContentRootPath}/jeebsconfig.json";
		if (File.Exists(path))
		{
			_ = ConfigValidator.Validate(path);
		}

		// Add Jeebs config - keeps Jeebs config away from app settings
		_ = config
			.AddJsonFile($"{env.ContentRootPath}/jeebsconfig.json", optional: true)
			.AddJsonFile($"{env.ContentRootPath}/jeebsconfig-secrets.json", optional: true);

		// Add environment-specific Jeebs config
		_ = config
			.AddJsonFile($"{env.ContentRootPath}/jeebsconfig.{env.EnvironmentName}.json", optional: true)
			.AddJsonFile($"{env.ContentRootPath}/jeebsconfig-secrets.{env.EnvironmentName}.json", optional: true);

		// Add config from Azure Key Vault
		var vault = config.Build().GetSection<KeyVaultConfig>(KeyVaultConfig.Key, false);
		if (vault.IsValid)
		{
			_ = config.AddAzureKeyVault(vault.GetUri(), vault.GetCredential());
		}
	}

	/// <summary>
	/// Configure Services
	/// </summary>
	/// <param name="ctx">HostBuilderContext</param>
	/// <param name="services">IServiceCollection</param>
	public virtual void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		// Shorthand for configuration
		var config = ctx.Configuration;

		// Add Jeebs config classes
		_ = services
			.Configure<AppConfig>(config.GetSection(AppConfig.Key))
			.Configure<AuthConfig>(config.GetSection(AuthConfig.Key))
			.Configure<DataProtectionConfig>(config.GetSection(DataProtectionConfig.Key))
			.Configure<KeyVaultConfig>(config.GetSection(KeyVaultConfig.Key))
			.Configure<DbConfig>(config.GetSection(DbConfig.Key))
			.Configure<JeebsConfig>(config.GetSection(JeebsConfig.Key))
			.Configure<JwtConfig>(config.GetSection(JwtConfig.Key))
			.Configure<LoggingConfig>(config.GetSection(LoggingConfig.Key))
			.Configure<RedirectionsConfig>(config.GetSection(RedirectionsConfig.Key))
			.Configure<ServicesConfig>(config.GetSection(ServicesConfig.Key))
			.Configure<VerificationConfig>(config.GetSection(VerificationConfig.Key))
			.Configure<WebConfig>(config.GetSection(WebConfig.Key));

		// Register Serilog Logger
		_ = services.AddSingleton<ILog, SerilogLogger>();
		_ = services.AddTransient(typeof(ILog<>), typeof(SerilogLogger<>));

		// Add HttpClient
		_ = services.AddHttpClient();

		// Add Azure Data Protection
		var dpc = ctx.Configuration.GetSection<DataProtectionConfig>(DataProtectionConfig.Key);
		var vault = ctx.Configuration.GetSection<KeyVaultConfig>(KeyVaultConfig.Key);
		if (dpc.IsValid && vault.IsValid)
		{
			_ = services.AddDataProtection()
				.PersistKeysToAzureBlobStorage(dpc.StorageAccessKeyConnectionString, dpc.ContainerName, dpc.BlobName)
				.ProtectKeysWithAzureKeyVault(dpc.GetUri(), vault.GetCredential());
		}
	}

	/// <summary>
	/// Configure Serilog
	/// </summary>
	/// <param name="ctx">HostBuilderContext</param>
	/// <param name="loggerConfig">LoggerConfiguration</param>
	public virtual void ConfigureSerilog(HostBuilderContext ctx, LoggerConfiguration loggerConfig)
	{
		// Load Serilog config
		var jeebs = ctx.Configuration.GetSection<JeebsConfig>(JeebsConfig.Key, false);
		loggerConfig.LoadFromJeebsConfig(jeebs);
	}
}
