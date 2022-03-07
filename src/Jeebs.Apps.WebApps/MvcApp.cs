// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs.Apps.WebApps.Middleware;
using Jeebs.Config;
using Jeebs.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace Jeebs.Apps;

/// <summary>
/// MVC Web Application - see <see cref="WebApp"/>
/// </summary>
public abstract class MvcApp : WebApp
{
	/// <summary>
	/// If true, routing will be set to append a trailing slash
	/// </summary>
	protected readonly bool appendTrailingSlash = true;

	/// <summary>
	/// If true, routing will force URLs to be lowercase
	/// </summary>
	protected readonly bool lowercaseUrls = true;

	/// <summary>
	/// If true, authorisation support will be enabled (default: disabled)
	/// </summary>
	protected readonly bool enableAuthorisation;

	/// <summary>
	/// If true, session support will be enabled (default: disabled)
	/// </summary>
	protected readonly bool enableSession;

	/// <summary>
	/// CookiePolicyOptions
	/// </summary>
	protected readonly CookiePolicyOptions cookiePolicyOptions = new();

	/// <summary>
	/// [Optional] JsonSerializerOptions
	/// </summary>
	protected readonly JsonSerializerOptions? jsonSerialiserOptions;

	/// <summary>
	/// Whether or not static files have been enabled
	/// </summary>
	protected bool staticFilesAreEnabled;

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	protected MvcApp(bool useHsts) : base(useHsts) { }

	#region ConfigureServices

	/// <inheritdoc/>
	protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
	{
		// Base
		base.ConfigureServices(env, config, services);

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
	protected virtual void ConfigureServicesResponseCaching(IServiceCollection services)
	{
		_ = services.AddResponseCaching();
	}

	/// <summary>
	/// Override to configure response compression
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesResponseCompression(IServiceCollection services)
	{
		_ = services
			.Configure<GzipCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Optimal)
			.Configure<BrotliCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Optimal)
			.AddResponseCompression(opt =>
			{
				opt.EnableForHttps = true;
				opt.Providers.Add<GzipCompressionProvider>();
				opt.Providers.Add<BrotliCompressionProvider>();
			});
	}

	/// <summary>
	/// Override to configure authorisation
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesAuthorisation(IServiceCollection services)
	{
		if (enableAuthorisation)
		{
			_ = services.AddAuthorization();
		}
	}

	/// <summary>
	/// Override to configure routing
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesRouting(IServiceCollection services)
	{
		_ = services.AddRouting(opt =>
		  {
			  opt.AppendTrailingSlash = appendTrailingSlash;
			  opt.LowercaseUrls = lowercaseUrls;
		  });
	}

	/// <summary>
	/// Override to configure session options
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesSession(IServiceCollection services)
	{
		if (enableSession)
		{
			_ = services.AddSession();
		}
	}

	/// <summary>
	/// Override to configure endpoints - default is MVC
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	protected virtual void ConfigureServicesEndpoints(IServiceCollection services)
	{
		_ = services
			.AddControllersWithViews(ConfigureServicesMvcOptions)
			.AddRazorRuntimeCompilation(ConfigureServicesRuntimeCompilation)
			.AddJsonOptions(ConfigureServicesEndpointsJson);
	}

	/// <summary>
	/// Override to configure MVC options
	/// </summary>
	/// <param name="opt">MvcOptions</param>
	public virtual void ConfigureServicesMvcOptions(MvcOptions opt)
	{
		opt.CacheProfiles.Add(CacheProfiles.None, new() { NoStore = true });
		opt.CacheProfiles.Add(CacheProfiles.Default, new() { Duration = 600, VaryByQueryKeys = new[] { "*" } });
	}

	/// <summary>
	/// Override to configure Razor Runtime Compilation
	/// </summary>
	/// <param name="opt">MvcRazorRuntimeCompilationOptions</param>
	public virtual void ConfigureServicesRuntimeCompilation(MvcRazorRuntimeCompilationOptions opt) { }

	/// <summary>
	/// Override to configure endpoints JSON
	/// </summary>
	/// <param name="opt">JsonOptions</param>
	public virtual void ConfigureServicesEndpointsJson(JsonOptions opt)
	{
		// Get default options
		var defaultOptions = F.JsonF.CopyOptions();

		// Set options
		opt.JsonSerializerOptions.DefaultIgnoreCondition = (jsonSerialiserOptions ?? defaultOptions).DefaultIgnoreCondition;
		opt.JsonSerializerOptions.PropertyNamingPolicy = (jsonSerialiserOptions ?? defaultOptions).PropertyNamingPolicy;
		opt.JsonSerializerOptions.DictionaryKeyPolicy = (jsonSerialiserOptions ?? defaultOptions).DictionaryKeyPolicy;
		opt.JsonSerializerOptions.NumberHandling = (jsonSerialiserOptions ?? defaultOptions).NumberHandling;

		// Set converters
		opt.JsonSerializerOptions.Converters.Clear();
		if (jsonSerialiserOptions?.Converters.Count > 0)
		{
			add(jsonSerialiserOptions.Converters);
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
	protected override void Configure(IHostEnvironment env, IApplicationBuilder app, IConfiguration config)
	{
		// Base
		base.Configure(env, app, config);

		// Compression
		ConfigureResponseCompression(app);

		// Static Files
		ConfigureStaticFiles(env, app);

		// Cookie Policy
		ConfigureCookiePolicy(app);

		// Response Caching
		ConfigureResponseCaching(app);

		// Redirections
		ConfigureRedirections(app, config);

		// Routing
		ConfigureRouting(app);

		// Authorisation
		ConfigureAuthorisation(app, config);

		// Session
		ConfigureSession(app);

		// Endpoint Routing
		ConfigureEndpoints(app);
	}

	/// <summary>
	/// Override to send all errors to the Error Controller
	/// </summary>
	/// <param name="app">IApplicationBuilder</param>
	protected override void ConfigureProductionExceptionHandling(IApplicationBuilder app)
	{
		base.ConfigureProductionExceptionHandling(app);

		// Use Error Controller to handle all other errors
		_ = app.UseStatusCodePagesWithReExecute("/Error/{0}");
	}

	/// <summary>
	/// Override to configure response compression
	/// </summary>
	/// <param name="app">IApplicationBuilder</param>
	protected virtual void ConfigureResponseCompression(IApplicationBuilder app)
	{
		_ = app.UseResponseCompression();
	}

	/// <summary>
	/// Override to configure static files - they must be enabled BEFORE any MVC routing
	/// </summary>
	/// <param name="env">IHostEnvironment</param>
	/// <param name="app">IApplicationBuilder</param>
	protected virtual void ConfigureStaticFiles(IHostEnvironment env, IApplicationBuilder app)
	{
		// Check whether or not they have already been enabled
		if (staticFilesAreEnabled)
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

		staticFilesAreEnabled = true;
	}

	/// <summary>
	/// Override to configure cookie policy
	/// </summary>
	/// <param name="app"></param>
	protected virtual void ConfigureCookiePolicy(IApplicationBuilder app)
	{
		_ = app.UseCookiePolicy(cookiePolicyOptions);
	}

	/// <summary>
	/// Override to configure response caching
	/// </summary>
	/// <param name="app">IApplicationBuilder</param>
	protected virtual void ConfigureResponseCaching(IApplicationBuilder app)
	{
		_ = app.UseResponseCaching();
	}

	/// <summary>
	/// Override to configure redirections
	/// </summary>
	/// <param name="app">IApplicationBuilder</param>
	/// <param name="config">IConfiguration</param>
	protected virtual void ConfigureRedirections(IApplicationBuilder app, IConfiguration config)
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
	/// <param name="app">IApplicationBuilder</param>
	protected virtual void ConfigureRouting(IApplicationBuilder app)
	{
		_ = app.UseRouting();
	}

	/// <summary>
	/// Override to configure authorisation
	/// </summary>
	/// <param name="app">IApplicationBuilder</param>
	/// <param name="config">IConfiguration</param>
	protected override void ConfigureAuthorisation(IApplicationBuilder app, IConfiguration config)
	{
		if (enableAuthorisation)
		{
			_ = app.UseAuthorization();
		}
	}

	/// <summary>
	/// Override to configure session
	/// </summary>
	/// <param name="app">IApplicationBuilder</param>
	protected virtual void ConfigureSession(IApplicationBuilder app)
	{
		if (enableSession)
		{
			_ = app.UseSession();
		}
	}

	/// <summary>
	/// Override to configure endpoints
	/// </summary>
	/// <param name="app">IApplicationBuilder</param>
	protected virtual void ConfigureEndpoints(IApplicationBuilder app)
	{
		_ = app.UseEndpoints(endpoints =>
		  {
			  _ = endpoints.MapControllerRoute(
				  name: "default",
				  pattern: "{controller=Home}/{action=Index}/{id?}"
			  );
		  });
	}

	#endregion Configure
}
