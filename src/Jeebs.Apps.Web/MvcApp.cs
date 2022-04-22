// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Apps.Web.Constants;
using Jeebs.Apps.Web.Middleware;
using Jeebs.Config;
using Jeebs.Config.Web.Redirections;
using Jeebs.Functions;
using Jeebs.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace Jeebs.Apps.Web;

/// <summary>
/// MVC Web Application - see <see cref="WebApp"/>
/// </summary>
public class MvcApp : WebApp
{
	#region Run

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new void Run(string[] args) =>
		WebAppBuilder.Run<MvcApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new void Run(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Run<MvcApp>(args, configureServices);

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new void Run<T>(string[] args)
		where T : MvcApp, new() =>
		WebAppBuilder.Run<T>(args, (_, _) => { });

	#endregion Run

	#region Create

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication app, ILog<MvcApp> log) Create(string[] args) =>
		WebAppBuilder.Create<MvcApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication app, ILog<MvcApp> log) Create(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Create<MvcApp>(args, configureServices);

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication app, ILog<T> log) Create<T>(string[] args)
		where T : MvcApp, new() =>
		WebAppBuilder.Create<T>(args, (_, _) => { });

	#endregion Create

	/// <summary>
	/// If true, routing will be set to append a trailing slash
	/// </summary>
	protected bool AppendTrailingSlash { get; init; } = true;

	/// <summary>
	/// If true, routing will force URLs to be lowercase
	/// </summary>
	protected bool LowercaseUrls { get; init; } = true;

	/// <summary>
	/// If true, session support will be enabled (default: disabled)
	/// </summary>
	protected bool EnableSession { get; init; }

	/// <summary>
	/// CookiePolicyOptions
	/// </summary>
	protected CookiePolicyOptions CookiePolicyOptions { get; init; } = new();

	/// <summary>
	/// [Optional] JsonSerializerOptions
	/// </summary>
	protected JsonSerializerOptions? JsonSerialiserOptions { get; init; }

	/// <summary>
	/// Whether or not static files have been enabled
	/// </summary>
	protected bool StaticFilesAreEnabled { get; private set; }

	/// <summary>
	/// Create MVC application with HSTS enabled
	/// </summary>
	public MvcApp() : this(true) { }

	/// <summary>
	/// Create MVC application
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	public MvcApp(bool useHsts) : base(useHsts) { }

	#region ConfigureServices

	/// <inheritdoc/>
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		// Base
		base.ConfigureServices(ctx, services);

		// Response Caching
		ConfigureServicesResponseCaching(services);

		// Response Compression
		ConfigureServicesResponseCompression(services);

		// Routing
		ConfigureServicesRouting(services);

		// Authorisation
		ConfigureServicesAuthorisation(services);

		// Session
		ConfigureServicesSession(services);

		// Endpoints
		ConfigureServicesEndpoints(services);
	}

	/// <summary>
	/// Override to configure response caching
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesResponseCaching(IServiceCollection services) =>
		_ = services.AddResponseCaching();

	/// <summary>
	/// Override to configure response compression
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesResponseCompression(IServiceCollection services) =>
		_ = services
			.Configure<GzipCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Optimal)
			.Configure<BrotliCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Optimal)
			.AddResponseCompression(opt =>
			{
				opt.EnableForHttps = true;
				opt.Providers.Add<GzipCompressionProvider>();
				opt.Providers.Add<BrotliCompressionProvider>();
			});

	/// <summary>
	/// Override to configure authorisation
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesAuthorisation(IServiceCollection services) =>
		_ = services.AddAuthorization();

	/// <summary>
	/// Override to configure routing
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesRouting(IServiceCollection services) =>
		_ = services.AddRouting(opt =>
		{
			opt.AppendTrailingSlash = AppendTrailingSlash;
			opt.LowercaseUrls = LowercaseUrls;
		});

	/// <summary>
	/// Override to configure session options
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesSession(IServiceCollection services)
	{
		if (EnableSession)
		{
			_ = services.AddSession();
		}
	}

	/// <summary>
	/// Override to configure endpoints - default is MVC
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesEndpoints(IServiceCollection services) =>
		_ = services
			.AddControllersWithViews(ConfigureServicesMvcOptions)
			.AddRazorRuntimeCompilation(ConfigureServicesRuntimeCompilation)
			.AddJsonOptions(ConfigureServicesEndpointsJson);

	/// <summary>
	/// Override to configure MVC options
	/// </summary>
	/// <param name="opt">MvcOptions</param>
	protected virtual void ConfigureServicesMvcOptions(MvcOptions opt)
	{
		opt.CacheProfiles.Add(CacheProfiles.None, new() { NoStore = true });
		opt.CacheProfiles.Add(CacheProfiles.Default, new() { Duration = 600, VaryByQueryKeys = new[] { "*" } });
	}

	/// <summary>
	/// Override to configure Razor Runtime Compilation
	/// </summary>
	/// <param name="opt">MvcRazorRuntimeCompilationOptions</param>
	protected virtual void ConfigureServicesRuntimeCompilation(MvcRazorRuntimeCompilationOptions opt) { }

	/// <summary>
	/// Override to configure endpoints JSON
	/// </summary>
	/// <param name="opt">JsonOptions</param>
	protected virtual void ConfigureServicesEndpointsJson(JsonOptions opt)
	{
		// Get default options
		var defaultOptions = JsonF.CopyOptions();

		// Set options
		opt.JsonSerializerOptions.DefaultIgnoreCondition = (JsonSerialiserOptions ?? defaultOptions).DefaultIgnoreCondition;
		opt.JsonSerializerOptions.PropertyNamingPolicy = (JsonSerialiserOptions ?? defaultOptions).PropertyNamingPolicy;
		opt.JsonSerializerOptions.DictionaryKeyPolicy = (JsonSerialiserOptions ?? defaultOptions).DictionaryKeyPolicy;
		opt.JsonSerializerOptions.NumberHandling = (JsonSerialiserOptions ?? defaultOptions).NumberHandling;

		// Set converters
		opt.JsonSerializerOptions.Converters.Clear();
		if (JsonSerialiserOptions?.Converters.Count > 0)
		{
			add(JsonSerialiserOptions.Converters);
		}
		else
		{
			add(defaultOptions.Converters);
		}

		// Add converters
		void add(IList<JsonConverter> converters)
		{
			foreach (var c in converters)
			{
				opt.JsonSerializerOptions.Converters.Add(c);
			}
		}
	}

	#endregion ConfigureServices

	#region Configure

	/// <inheritdoc/>
	public override void Configure(WebApplication app)
	{
		// Compression
		ConfigureResponseCompression(app);

		// Static Files
		ConfigureStaticFiles(app.Environment, app);

		// Cookie Policy
		ConfigureCookiePolicy(app);

		// Response Caching
		ConfigureResponseCaching(app);

		// Redirections
		ConfigureRedirections(app, app.Configuration);

		// Routing
		ConfigureRouting(app);

		// Authorisation
		ConfigureAuth(app, app.Configuration);

		// Session
		ConfigureSession(app);

		// Endpoint Routing
		ConfigureEndpoints(app);
	}

	/// <summary>
	/// Override to send all errors to the Error Controller
	/// </summary>
	/// <param name="app">WebApplication</param>
	protected override void ConfigureProductionExceptionHandling(WebApplication app)
	{
		base.ConfigureProductionExceptionHandling(app);

		// Use Error Controller to handle all other errors
		_ = app.UseStatusCodePagesWithReExecute("/Error/{0}");
	}

	/// <summary>
	/// Override to configure response compression
	/// </summary>
	/// <param name="app">WebApplication</param>
	protected virtual void ConfigureResponseCompression(WebApplication app) =>
		_ = app.UseResponseCompression();

	/// <summary>
	/// Override to configure static files - they must be enabled BEFORE any MVC routing
	/// </summary>
	/// <param name="env">IHostEnvironment</param>
	/// <param name="app">WebApplication</param>
	protected virtual void ConfigureStaticFiles(IHostEnvironment env, WebApplication app)
	{
		// Check whether or not they have already been enabled
		if (StaticFilesAreEnabled)
		{
			return;
		}

		// Enable static files with default options
		if (env.IsDevelopment())
		{
			_ = app.UseStaticFiles();
		}

		// Set static file cache to 365 days on production
		else
		{
			_ = app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = ctx =>
					ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={60 * 60 * 24 * 365}"
			});
		}

		StaticFilesAreEnabled = true;
	}

	/// <summary>
	/// Override to configure cookie policy
	/// </summary>
	/// <param name="app"></param>
	protected virtual void ConfigureCookiePolicy(WebApplication app) =>
		_ = app.UseCookiePolicy(CookiePolicyOptions);

	/// <summary>
	/// Override to configure response caching
	/// </summary>
	/// <param name="app">WebApplication</param>
	protected virtual void ConfigureResponseCaching(WebApplication app) =>
		_ = app.UseResponseCaching();

	/// <summary>
	/// Override to configure redirections
	/// </summary>
	/// <param name="app">WebApplication</param>
	/// <param name="config">IConfiguration</param>
	protected virtual void ConfigureRedirections(WebApplication app, IConfiguration config)
	{
		if (
			config.GetSection<RedirectionsConfig>(RedirectionsConfig.Key) is RedirectionsConfig redirections
			&& redirections.Count > 0
		)
		{
			_ = app.UseMiddleware<RedirectExactMiddleware>();
		}
	}

	/// <summary>
	/// Override to configure routing
	/// </summary>
	/// <param name="app">WebApplication</param>
	protected virtual void ConfigureRouting(WebApplication app) =>
		_ = app.UseRouting();

	/// <summary>
	/// Override to configure session
	/// </summary>
	/// <param name="app">WebApplication</param>
	protected virtual void ConfigureSession(WebApplication app)
	{
		if (EnableSession)
		{
			_ = app.UseSession();
		}
	}

	/// <summary>
	/// Override to configure endpoints
	/// </summary>
	/// <param name="app">WebApplication</param>
	protected virtual void ConfigureEndpoints(WebApplication app) =>
		app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}"
		));

	#endregion Configure
}
