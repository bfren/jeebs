// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using System.IO;
using Azure.Identity;
using Jeebs.Config;
using Jeebs.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jeebs.Apps
{
	/// <summary>
	/// Configure and run an application using <seealso cref="IHost"/>
	/// </summary>
	public abstract class App
	{
		/// <summary>
		/// Build <see cref="IHost"/> using specified arguments
		/// </summary>
		/// <param name="args">Command Line Arguments</param>
		public virtual IHost BuildHost(string[] args) =>
			// Create Default Host Builder
			Host.CreateDefaultBuilder(
				args
			)

			// Configure Host
			.ConfigureHostConfiguration(
				config => ConfigureHost(config)
			)

			// Configure App
			.ConfigureAppConfiguration(
				(host, config) => ConfigureApp(host.HostingEnvironment, config, args)
			)

			// Configure Serilog
			.UseSerilog(
				(host, logger) => ConfigureSerilog(host.Configuration, logger)
			)

			// Configure Services
			.ConfigureServices(
				(host, services) => ConfigureServices(host.HostingEnvironment, host.Configuration, services)
			)

			// Build Host
			.Build();

		/// <summary>
		/// Runs when the application is ready to go but before it is run
		/// </summary>
		/// <param name="services">IServiceProvider</param>
		/// <param name="log">ILog</param>
		public virtual void Ready(IServiceProvider services, ILog log)
		{
			// Set Option Audit log
			F.OptionF.LogAuditExceptions = e => log.Error(e, "Error auditing Option");

			// Log application is ready
			log.Information("Application ready.");
		}

		/// <summary>
		/// Configure Host
		/// </summary>
		/// <param name="config">IConfigurationBuilder</param>
		protected virtual void ConfigureHost(IConfigurationBuilder config)
		{
			// Set base path to be directory of running assembly
			config.SetBasePath(Directory.GetCurrentDirectory());
		}

		/// <summary>
		/// Configure App
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfigurationBuilder</param>
		/// <param name="args">Command Line arguments</param>
		protected virtual void ConfigureApp(IHostEnvironment env, IConfigurationBuilder config, string[] args)
		{
			// Validate main configuration file
			var path = $"{env.ContentRootPath}/jeebsconfig.json";
			ConfigValidator.Validate(path);

			// Add Jeebs config - keeps Jeebs config away from app settings
			config
				.AddJsonFile($"{env.ContentRootPath}/jeebsconfig.json", optional: false)
				.AddJsonFile($"{env.ContentRootPath}/jeebsconfig-secrets.json", optional: false);

			// Add environment-specific Jeebs config
			config
				.AddJsonFile($"{env.ContentRootPath}/jeebsconfig.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile($"{env.ContentRootPath}/jeebsconfig-secrets.{env.EnvironmentName}.json", optional: true);

			// Add Environment Variables
			config.AddEnvironmentVariables();

			// Check for Azure Key Vault section
			var vault = config.Build().GetSection<AzureKeyVaultConfig>(AzureKeyVaultConfig.Key, false);

			// If the config is valid, add Azure Key Vault to IConfigurationBuilder
			if (vault.IsValid)
			{
				config.AddAzureKeyVault(
					new Uri($"https://{vault.Name}.vault.azure.net/"),
					new ClientSecretCredential(vault.TenantId, vault.ClientId, vault.ClientSecret)
				);
			}

			// Add command line arguments
			config.AddCommandLine(args);
		}

		/// <summary>
		/// Configure Serilog
		/// </summary>
		/// <param name="config">IConfiguration</param>
		/// <param name="loggerConfig">LoggerConfiguration</param>
		protected virtual void ConfigureSerilog(IConfiguration config, LoggerConfiguration loggerConfig)
		{
			// Load Serilog config
			var jeebs = config.GetSection<JeebsConfig>(JeebsConfig.Key, false);
			loggerConfig.LoadFromJeebsConfig(jeebs);
		}

		/// <summary>
		/// Configure Services
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="services">IServiceCollection</param>
		protected virtual void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			// Add Jeebs config classes
			services
				.Configure<AppConfig>(config.GetSection(AppConfig.Key))
				.Configure<AuthConfig>(config.GetSection(AuthConfig.Key))
				.Configure<AzureKeyVaultConfig>(config.GetSection(AzureKeyVaultConfig.Key))
				.Configure<DbConfig>(config.GetSection(DbConfig.Key))
				.Configure<JeebsConfig>(config.GetSection(JeebsConfig.Key))
				.Configure<JwtConfig>(config.GetSection(JwtConfig.Key))
				.Configure<LoggingConfig>(config.GetSection(LoggingConfig.Key))
				.Configure<RedirectionsConfig>(config.GetSection(RedirectionsConfig.Key))
				.Configure<ServicesConfig>(config.GetSection(ServicesConfig.Key))
				.Configure<VerificationConfig>(config.GetSection(VerificationConfig.Key))
				.Configure<WebConfig>(config.GetSection(WebConfig.Key));

			// Register Serilog Logger
			services.AddSingleton<ILog, SerilogLogger>();
			services.AddTransient(typeof(ILog<>), typeof(SerilogLogger<>));
		}
	}
}
