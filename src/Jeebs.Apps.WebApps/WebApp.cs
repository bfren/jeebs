// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Apps.WebApps.Middleware;
using Jeebs.Config;
using Jeebs.Config.Web.Auth;
using Jeebs.Config.Web.Verification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS = Microsoft.AspNetCore.Builder;

namespace Jeebs.Apps.WebApps;

/// <summary>
/// Web Application - see <see cref="App"/>
/// </summary>
public class WebApp : App
{
	/// <summary>
	/// Whether or not to use HSTS
	/// </summary>
	private readonly bool useHsts;

	/// <summary>
	/// Create web application with HSTS enabled
	/// </summary>
	public WebApp() : this(true) { }

	/// <summary>
	/// Create web application
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	public WebApp(bool useHsts) =>
		this.useHsts = useHsts;

	/// <inheritdoc/>
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		// Base
		base.ConfigureServices(ctx, services);

		// Register middleware
		_ = services.AddScoped<LoggerMiddleware>();
		_ = services.AddScoped<RedirectExactMiddleware>();
		_ = services.AddScoped<SiteVerificationMiddleware>();

		// Specify HSTS options
		if (!ctx.HostingEnvironment.IsDevelopment() && useHsts)
		{
			_ = services.AddHsts(opt =>
			{
				opt.Preload = true;
				opt.IncludeSubDomains = true;
				opt.MaxAge = TimeSpan.FromDays(60);
			});
		}
	}

	/// <summary>
	/// Configure a WebApplication
	/// </summary>
	/// <param name="app">WebApplication</param>
	public virtual void Configure(MS.WebApplication app)
	{
		// Shorthands
		var config = app.Configuration;
		var env = app.Environment;

		// Logging
		_ = app.UseMiddleware<LoggerMiddleware>();

		// Site Verification
		ConfigureSiteVerification(app, config);

		if (env.IsDevelopment())
		{
			// Useful exception page
			_ = app.UseDeveloperExceptionPage();
		}
		else
		{
			// Pretty exception page
			ConfigureProductionExceptionHandling(app);

			// Add security headers
			ConfigureSecurityHeaders(app);
		}

		// Authentication and authorisation
		if (config.GetSection<AuthConfig>(AuthConfig.Key) is AuthConfig auth && auth.Enabled)
		{
			ConfigureAuthorisation(app, config);
		}

		// Do NOT use HTTPS redirection - this should be handled by the web server / reverse proxy
	}

	/// <summary>
	/// Override to configure site verification
	/// </summary>
	/// <param name="app">WebApplication</param>
	/// <param name="config">IConfiguration</param>
	protected virtual void ConfigureSiteVerification(MS.WebApplication app, IConfiguration config)
	{
		if (
			config.GetSection<VerificationConfig>(VerificationConfig.Key) is VerificationConfig verification
			&& verification.Any
		)
		{
			_ = app.UseMiddleware<SiteVerificationMiddleware>();
		}
	}

	/// <summary>
	/// Override to configure production exception handling
	/// </summary>
	/// <param name="app">WebApplication</param>
	protected virtual void ConfigureProductionExceptionHandling(MS.WebApplication app) =>
		_ = app.UseExceptionHandler("/Error");

	/// <summary>
	/// Override to configure security headers
	/// </summary>
	/// <param name="app">WebApplication</param>
	protected virtual void ConfigureSecurityHeaders(MS.WebApplication app)
	{
		if (useHsts) // check for Development Environment happens in Configure()
		{
			_ = app.UseHsts();
		}
	}

	/// <summary>
	/// Override to configure authentication and authorisation - it is only called if Auth is enabled
	/// </summary>
	/// <param name="app">WebApplication</param>
	/// <param name="config">IConfiguration</param>
	protected virtual void ConfigureAuthorisation(MS.WebApplication app, IConfiguration config) =>
		_ = app.UseAuthorization();
}
