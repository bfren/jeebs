using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
			return Host.CreateDefaultBuilder(args)

				// Configure Host
				.ConfigureHostConfiguration(ConfigureHost)

				// Configure App
				.ConfigureAppConfiguration((host, config) => ConfigureApp(host.HostingEnvironment, config))

				// Configure Serilog
				.UseSerilog((host, logger) => ConfigureSerilog(host.Configuration, logger))

				// Configure Services
				.ConfigureServices((host, services) => ConfigureServices(host.HostingEnvironment, host.Configuration, services))

				// Build Host
				.Build()

			;
		}

		/// <summary>
		/// Configure Host
		/// </summary>
		/// <param name="config">IConfigurationBuilder</param>
		protected virtual void ConfigureHost(IConfigurationBuilder config)
		{
			config.SetBasePath(Directory.GetCurrentDirectory());
		}

		/// <summary>
		/// Configure App
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfigurationBuilder</param>
		protected virtual void ConfigureApp(IHostEnvironment env, IConfigurationBuilder config)
		{
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
			// Bind JeebsConfig
			services.Bind<JeebsConfig>().To(JeebsConfig.Key).Using(config);

			// Register Serilog Logger
			services.AddTransient<ILog, SerilogLogger>();
			services.AddTransient(typeof(ILog<>), typeof(SerilogLogger<>));
		}
	}
}
