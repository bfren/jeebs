using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Jeebs.Logging.Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jeebs.Apps
{
	/// <summary>
	/// Application bootstrapped using IHost
	/// </summary>
	public abstract class App
	{
		/// <summary>
		/// Create IHost
		/// </summary>
		/// <param name="args">Command Line Arguments</param>
		public virtual IHost CreateHost(string[] args)
		{
			// Create Default Host Builder
			return Host.CreateDefaultBuilder(args)

				// Configure Host
				.ConfigureHostConfiguration(config =>
				{
					ConfigureHost(ref config);
				})

				// Configure App
				.ConfigureAppConfiguration((host, config) =>
				{
					ConfigureApp(host.HostingEnvironment, ref config);
				})

				// Configure Serilog
				.UseSerilog((host, logger) =>
				{
					ConfigureSerilog(host.Configuration, ref logger);
				})

				// Configure Services
				.ConfigureServices((host, services) =>
				{
					ConfigureServices(host.HostingEnvironment, host.Configuration, ref services);
				})

				// Build Host
				.Build()

			;
		}

		/// <summary>
		/// Configure Host
		/// </summary>
		/// <param name="config">IConfigurationBuilder</param>
		protected virtual void ConfigureHost(ref IConfigurationBuilder config)
		{
			config.SetBasePath(Directory.GetCurrentDirectory());
			config.AddEnvironmentVariables();
		}

		/// <summary>
		/// Configure App
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfigurationBuilder</param>
		protected virtual void ConfigureApp(in IHostEnvironment env, ref IConfigurationBuilder config)
		{
			config.AddJeebsConfig(env);
		}

		/// <summary>
		/// Configure Serilog
		/// </summary>
		/// <param name="config">IConfiguration</param>
		/// <param name="logger">LoggerConfiguration</param>
		protected virtual void ConfigureSerilog(in IConfiguration config, ref LoggerConfiguration logger)
		{
			// Load Serilog config
			var jeebs = config.GetJeebsConfig();
			logger.LoadFromJeebsConfig(jeebs);
		}

		/// <summary>
		/// Configure Services
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="services">IServiceCollection</param>
		protected virtual void ConfigureServices(in IHostEnvironment env, in IConfiguration config, ref IServiceCollection services)
		{
			// Bind JeebsConfig
			services.Bind<JeebsConfig>().To(JeebsConfig.Key).Using(config);

			// Register Serilog Logger
			services.AddTransient<ILog, SerilogLogger>();
		}
	}
}
