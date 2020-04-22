﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.Apps.WebApps.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jeebs.Apps
{
	/// <summary>
	/// Web Application - see <see cref="App"/>
	/// </summary>
	public abstract class WebApp : App
	{
		/// <summary>
		/// Whether or not to use HSTS
		/// </summary>
		private readonly bool useHsts;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
		protected WebApp(bool useHsts) => this.useHsts = useHsts;

		/// <inheritdoc/>
		public override IHost CreateHost(string[] args)
		{
			return Host.CreateDefaultBuilder(args)

				// Use Web Host Defaults
				.ConfigureWebHostDefaults(builder => builder

					// App Configuration
					.ConfigureAppConfiguration((host, config) =>
					{
						ConfigureApp(host.HostingEnvironment, config);
					})

					// Serilog
					.UseSerilog((host, logger) =>
					{
						ConfigureSerilog(host.Configuration, logger);
					})

					// Services
					.ConfigureServices((host, services) =>
					{
						ConfigureServices(host.HostingEnvironment, host.Configuration, services);
					})

					// Configure
					.Configure((host, app) =>
					{
						Configure(host.HostingEnvironment, host.Configuration, app);
					})

					// Alter ApplicationKey - forces app to look for Controllers in the App rather than this library
					.UseSetting(WebHostDefaults.ApplicationKey, GetType().Assembly.FullName)
				)

			.Build();
		}

		/// <inheritdoc/>
		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, services);

			// Specify HSTS options
			if (!env.IsDevelopment() && useHsts)
			{
				services.AddHsts(opt =>
				{
					opt.Preload = true;
					opt.IncludeSubDomains = true;
					opt.MaxAge = TimeSpan.FromDays(60);
				});
			}
		}

		/// <summary>
		/// Configure Application
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure(IHostEnvironment env, IConfiguration config, IApplicationBuilder app)
		{
			// Logging
			app.UseMiddleware<LoggerMiddleware>();

			if (env.IsDevelopment())
			{
				// Useful exception page
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// Pretty exception page
				Configure_ProductionExceptionHandling(app);

				// Add security headers
				Configure_SecurityHeaders(app);
			}

			// Do NOT use HTTPS redirection - this should be handled by the web server / reverse proxy
		}

		/// <summary>
		/// Override to configure production exception handling
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_ProductionExceptionHandling(IApplicationBuilder app)
		{
			app.UseExceptionHandler("/Error");
		}

		/// <summary>
		/// Override to configure security headers
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_SecurityHeaders(IApplicationBuilder app)
		{
			if (useHsts) // check for Development Environment happens in Configure()
			{
				app.UseHsts();
			}
		}
	}
}
