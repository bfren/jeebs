// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.IO;
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
		/// Create <see cref="IHost"/> using specified arguments
		/// </summary>
		/// <param name="args">Command Line Arguments</param>
		public virtual IHost CreateHost(string[] args)
		{
			// Create Default Host Builder
			var host = Host.CreateDefaultBuilder(args)

				// Configure Host
				.ConfigureHostConfiguration(ConfigureHost)

				// Configure App
				.ConfigureAppConfiguration(
					(host, config) => ConfigureApp(host.HostingEnvironment, config)
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
				.Build()

			;

			// Ready to go
			Ready(host.Services);

			// Return host
			return host;
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
		protected virtual void ConfigureApp(IHostEnvironment env, IConfigurationBuilder config)
		{
			// Add Jeebs config files
			config.AddJeebsConfig(env);
		}

		/// <summary>
		/// Configure Serilog
		/// </summary>
		/// <param name="config">IConfiguration</param>
		/// <param name="loggerConfig">LoggerConfiguration</param>
		protected virtual void ConfigureSerilog(IConfiguration config, LoggerConfiguration loggerConfig)
		{
			// Load Serilog config
			var jeebs = config.GetJeebsConfig();
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
			// Bind Jeebs config classes
			services.AddJeebsConfig(config);

			// Register Serilog Logger
			services.AddSingleton<ILog, SerilogLogger>();
			services.AddTransient(typeof(ILog<>), typeof(SerilogLogger<>));
		}

		/// <summary>
		/// Runs when host configuration is completed and it is ready to start
		/// </summary>
		/// <param name="services">IServiceProvider</param>
		protected virtual void Ready(IServiceProvider services)
		{
			// Set Option Audit log and output ready message
			if (services.GetService<ILog>() is ILog log)
			{
				Option.LogAuditExceptions = e => log.Error(e, "Error auditing Option");
				log.Information("Application ready.");
			}
		}
	}
}
