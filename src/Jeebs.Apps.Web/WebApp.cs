// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Apps.Web.Middleware;
using Jeebs.Config;
using Jeebs.Config.Web.Auth;
using Jeebs.Config.Web.Verification;
using Jeebs.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps.Web;

/// <summary>
/// Web Application - see <see cref="App"/>.
/// </summary>
/// <remarks>
/// Create web application.
/// </remarks>
public class WebApp : App
{
	#region Run

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static void Run(string[] args) =>
		WebAppBuilder.Run<WebApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static void Run(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Run<WebApp>(args, configureServices);

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static void Run<T>(string[] args)
		where T : WebApp, new() =>
		WebAppBuilder.Run<T>(args, (_, _) => { });

	#endregion Run

	#region Create

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (WebApplication app, ILog<WebApp> log) Create(string[] args) =>
		WebAppBuilder.Create<WebApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (WebApplication app, ILog<WebApp> log) Create(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Create<WebApp>(args, configureServices);

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (WebApplication app, ILog<T> log) Create<T>(string[] args)
		where T : WebApp, new() =>
		WebAppBuilder.Create<T>(args, (_, _) => { });

	#endregion Create

	/// <summary>
	/// Whether or not to use HSTS.
	/// </summary>
	private readonly bool useHsts;

	/// <summary>
	/// Create web application with HSTS enabled.
	/// </summary>
	public WebApp() : this(true) { }

	/// <summary>
	/// Create web application.
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy.</param>
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
	/// Configure a WebApplication.
	/// </summary>
	/// <param name="app">WebApplication.</param>
	public virtual void Configure(WebApplication app)
	{
		// Logging, Site Verification, Exception Handling and Security Headers
		ConfigureStart(app);

		// Configure Authorisation and Authentication
		if (app.Configuration.GetSection<AuthConfig>(AuthConfig.Key) is AuthConfig auth && auth.Enabled)
		{
			ConfigureAuth(app, app.Configuration);
		}

		// Do NOT use HTTPS redirection - this should be handled by the web server / reverse proxy
	}

	/// <summary>
	/// Add the middleware every web application needs at the start of the pipeline:
	/// request logging, site verification, exception handling and security headers.
	/// </summary>
	/// <param name="app">WebApplication.</param>
	protected void ConfigureStart(WebApplication app)
	{
		// Logging
		_ = app.UseMiddleware<LoggerMiddleware>();

		// Site Verification
		ConfigureSiteVerification(app, app.Configuration);

		if (app.Environment.IsDevelopment())
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
	}

	/// <summary>
	/// Override to configure site verification.
	/// </summary>
	/// <param name="app">WebApplication.</param>
	/// <param name="config">IConfiguration.</param>
	protected virtual void ConfigureSiteVerification(WebApplication app, IConfiguration config)
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
	/// Override to configure production exception handling.
	/// </summary>
	/// <param name="app">WebApplication.</param>
	protected virtual void ConfigureProductionExceptionHandling(WebApplication app) =>
		_ = app.UseExceptionHandler("/Error");

	/// <summary>
	/// Override to configure security headers.
	/// </summary>
	/// <param name="app">WebApplication.</param>
	protected virtual void ConfigureSecurityHeaders(WebApplication app)
	{
		if (useHsts) // check for Development Environment happens in Configure()
		{
			_ = app.UseHsts();
		}
	}

	/// <summary>
	/// Override to configure authentication and authorisation - it is only called if Auth is enabled.
	/// </summary>
	/// <remarks>
	/// Calls to app.UseAuthentication() should come *before* app.UseAuthorization()
	/// </remarks>
	/// <param name="app">WebApplication.</param>
	/// <param name="config">IConfiguration.</param>
	protected virtual void ConfigureAuth(WebApplication app, IConfiguration config) =>
		_ = app.UseAuthorization();
}
