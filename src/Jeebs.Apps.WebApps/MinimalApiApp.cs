// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Jeebs.Apps
{
	/// <summary>
	/// Application supporting minimal API syntax
	/// </summary>
	internal class MinimalApiApp : ApiApp
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
		internal MinimalApiApp(bool useHsts) : base(useHsts) { }

		/// <summary>
		/// Create and configure a <see cref="WebApplication"/>
		/// </summary>
		/// <param name="args">Commandline arguments</param>
		/// <param name="configure">[Optional] Configure builder before app is built</param>
		internal WebApplication Create(string[] args, Action<WebApplicationBuilder>? configure)
		{
			// Create builder
			var builder = WebApplication.CreateBuilder(args);

			// Configure host
			builder.Host
				.ConfigureHostConfiguration(
					config => ConfigureHost(config)
				);

			// Configure web host
			builder.WebHost
				.ConfigureAppConfiguration(
					(host, config) => ConfigureApp(host.HostingEnvironment, config, args)
				)
				.UseSerilog(
					(host, logger) => ConfigureSerilog(host.Configuration, logger)
				)
				.ConfigureServices(
					(host, services) => ConfigureServices(host.HostingEnvironment, host.Configuration, services)
				);

			// Configure builder
			configure?.Invoke(builder);

			// Build and configure app
			var app = builder.Build();
			Configure(app.Environment, app, app.Configuration);

			// Return app
			return app;
		}

		/// <inheritdoc/>
		public override WebApplication BuildHost(string[] args) =>
			Create(args, null);

		/// <inheritdoc/>
		protected override void ConfigureServices_Endpoints(IServiceCollection services)
		{
			// do nothing
		}

		/// <inheritdoc/>
		protected override void Configure_Endpoints(IApplicationBuilder app)
		{
			// do nothing
		}
	}
}
