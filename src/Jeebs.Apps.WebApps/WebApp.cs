﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Apps.WebApps.Middleware;
using Jeebs.Config;
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
		protected WebApp(bool useHsts) =>
			this.useHsts = useHsts;

		/// <inheritdoc/>
		public override IHost BuildHost(string[] args) =>
			// Create Default Host Builder
			Host.CreateDefaultBuilder(
				args
			)

			// Configure Host
			.ConfigureHostConfiguration(
				config => ConfigureHost(config)
			)

			// Use Web Host Defaults
			.ConfigureWebHostDefaults(builder => builder

				// App Configuration
				.ConfigureAppConfiguration(
					(host, config) => ConfigureApp(host.HostingEnvironment, config, args)
				)

				// Serilog
				.UseSerilog(
					(host, logger) => ConfigureSerilog(host.Configuration, logger)
				)

				// Services
				.ConfigureServices(
					(host, services) => ConfigureServices(host.HostingEnvironment, host.Configuration, services)
				)

				// Configure
				.Configure(
					(host, app) => Configure(host.HostingEnvironment, app, host.Configuration)
				)

				// Alter ApplicationKey - forces app to look for Controllers in the App rather than this library
				.UseSetting(
					WebHostDefaults.ApplicationKey, GetType().Assembly.FullName
				)
			)

			// Build Web Host
			.Build();

		/// <inheritdoc/>
		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, services);

			// Register middleware
			services.AddScoped<LoggerMiddleware>();
			services.AddScoped<RedirectExactMiddleware>();
			services.AddScoped<SiteVerificationMiddleware>();

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
		/// <param name="app">IApplicationBuilder</param>
		/// <param name="config">IConfiguration</param>
		protected virtual void Configure(IHostEnvironment env, IApplicationBuilder app, IConfiguration config)
		{
			// Logging
			app.UseMiddleware<LoggerMiddleware>();

			// Site Verification
			Configure_SiteVerification(app, config);

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

			// Authentication and authorisation
			if (config.GetSection<AuthConfig>(AuthConfig.Key) is AuthConfig auth && auth.Enabled)
			{
				Configure_Auth(app, config);
			}

			// Do NOT use HTTPS redirection - this should be handled by the web server / reverse proxy
		}

		/// <summary>
		/// Override to configure site verification
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		/// <param name="config">IConfiguration</param>
		protected virtual void Configure_SiteVerification(IApplicationBuilder app, IConfiguration config)
		{
			if (
				config.GetSection<VerificationConfig>(VerificationConfig.Key) is VerificationConfig verification
				&& verification.Any
			)
			{
				app.UseMiddleware<SiteVerificationMiddleware>();
			}
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

		/// <summary>
		/// Override to configure authentication and authorisation - it is only called if Auth is enabled
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		/// <param name="config">IConfiguration</param>
		protected virtual void Configure_Auth(IApplicationBuilder app, IConfiguration config)
		{
			app.UseAuthorization();
		}
	}
}
