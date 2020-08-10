using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using Jeebs.Apps.WebApps.Config;
using Jeebs.Apps.WebApps.Middleware;
using Jeebs.Constants;
using Jeebs.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace Jeebs.Apps
{
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
		/// CookiePolicyOptions
		/// </summary>
		protected readonly CookiePolicyOptions cookiePolicyOptions = new CookiePolicyOptions();

		/// <summary>
		/// [Optional] MVC Compatibility Version
		/// </summary>
		protected readonly CompatibilityVersion? compatibilityVersion;

		/// <summary>
		/// [Optional] JsonSerializerOptions
		/// </summary>
		protected readonly JsonSerializerOptions? jsonSerialiserSettings;

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
			ConfigureServices_ResponseCaching(services);

			// Response Compression
			ConfigureServices_ResponseCompression(services);

			// Routing
			ConfigureServices_Routing(services);

			// Endpoints
			ConfigureServices_Endpoints(services);
		}

		/// <summary>
		/// Override to configure response caching
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		protected virtual void ConfigureServices_ResponseCaching(IServiceCollection services)
		{
			services.AddResponseCaching();
		}

		/// <summary>
		/// Override to configure response compression
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		protected virtual void ConfigureServices_ResponseCompression(IServiceCollection services)
		{
			services
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
		/// Override to configure routing
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		protected virtual void ConfigureServices_Routing(IServiceCollection services)
		{
			services.AddRouting(opt =>
			{
				opt.AppendTrailingSlash = appendTrailingSlash;
				opt.LowercaseUrls = lowercaseUrls;
			});
		}

		/// <summary>
		/// Override to configure endpoints - default is MVC
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		protected virtual void ConfigureServices_Endpoints(IServiceCollection services)
		{
			services
				.AddControllersWithViews(ConfigureServices_MvcOptions)
				.AddRazorRuntimeCompilation(ConfigureServices_RuntimeCompilation)
				.AddJsonOptions(ConfigureServices_EndpointsJson);
		}

		/// <summary>
		/// Override to configure MVC options
		/// </summary>
		/// <param name="opt">MvcOptions</param>
		public virtual void ConfigureServices_MvcOptions(MvcOptions opt)
		{
			opt.CacheProfiles.Add(CacheProfiles.None, new CacheProfile() { NoStore = true });
			opt.CacheProfiles.Add(CacheProfiles.Default, new CacheProfile() { Duration = 600, VaryByQueryKeys = new[] { "*" } });
		}

		/// <summary>
		/// Override to configure Razor Runtime Compilation
		/// </summary>
		/// <param name="opt">MvcRazorRuntimeCompilationOptions</param>
		public virtual void ConfigureServices_RuntimeCompilation(MvcRazorRuntimeCompilationOptions opt) { }

		/// <summary>
		/// Override to configure endpoints JSON
		/// </summary>
		/// <param name="opt">JsonOptions</param>
		public virtual void ConfigureServices_EndpointsJson(JsonOptions opt)
		{
			opt.JsonSerializerOptions.DefaultIgnoreCondition = (jsonSerialiserSettings ?? Json.DefaultSettings).DefaultIgnoreCondition;
			opt.JsonSerializerOptions.PropertyNamingPolicy = (jsonSerialiserSettings ?? Json.DefaultSettings).PropertyNamingPolicy;
			opt.JsonSerializerOptions.DictionaryKeyPolicy = (jsonSerialiserSettings ?? Json.DefaultSettings).DictionaryKeyPolicy;

			opt.JsonSerializerOptions.Converters.Clear();
			foreach (var item in (jsonSerialiserSettings ?? Json.DefaultSettings).Converters)
			{
				opt.JsonSerializerOptions.Converters.Add(item);
			}
		}

		#endregion

		#region Configure

		/// <inheritdoc/>
		protected override void Configure(IHostEnvironment env, IConfiguration config, IApplicationBuilder app)
		{
			// Base
			base.Configure(env, config, app);

			// Compression
			Configure_ResponseCompression(app);

			// Static Files
			Configure_StaticFiles(env, app);

			// Cookie Policy
			Configure_CookiePolicy(app);

			// Response Caching
			Configure_ResponseCaching(app);

			// Redirections
			Configure_Redirections(config, app);

			// Routing
			Configure_Routing(app);

			// Authorisation
			Configure_Authorisation(app);

			// Endpoint Routing
			Configure_Endpoints(app);
		}

		/// <summary>
		/// Override to send all errors to the Error Controller
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected override void Configure_ProductionExceptionHandling(IApplicationBuilder app)
		{
			base.Configure_ProductionExceptionHandling(app);

			// Use Error Controller to handle all other errors
			app.UseStatusCodePagesWithReExecute("/Error/{0}");
		}

		/// <summary>
		/// Override to configure response compression
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_ResponseCompression(IApplicationBuilder app)
		{
			app.UseResponseCompression();
		}

		/// <summary>
		/// Override to configure static files - they must be enabled BEFORE any MVC routing
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_StaticFiles(IHostEnvironment env, IApplicationBuilder app)
		{
			// Check whether or not they have already been enabled
			if (staticFilesAreEnabled)
			{
				return;
			}

			// Enable static files with default options
			if (env.IsDevelopment())
			{
				app.UseStaticFiles();
			}

			// Set static file cache to 365 days on production
			else
			{
				app.UseStaticFiles(new StaticFileOptions
				{
					OnPrepareResponse = ctx => ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={60 * 60 * 24 * 365}"
				});
			}

			staticFilesAreEnabled = true;
		}

		/// <summary>
		/// Override to configure cookie policy
		/// </summary>
		/// <param name="app"></param>
		protected virtual void Configure_CookiePolicy(IApplicationBuilder app)
		{
			app.UseCookiePolicy(cookiePolicyOptions);
		}

		/// <summary>
		/// Override to configure response caching
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_ResponseCaching(IApplicationBuilder app)
		{
			app.UseResponseCaching();
		}

		/// <summary>
		/// Override to configure redirections
		/// </summary>
		/// <param name="config">IConfiguration</param>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_Redirections(IConfiguration config, IApplicationBuilder app)
		{
			if (config.GetSection<RedirectionsConfig>(":redirections") is RedirectionsConfig redirectRules)
			{
				app.UseMiddleware<RedirectExactMiddleware>(redirectRules);
			}
		}

		/// <summary>
		/// Override to configure routing
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_Routing(IApplicationBuilder app)
		{
			app.UseRouting();
		}

		/// <summary>
		/// Override to configure authorisation
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_Authorisation(IApplicationBuilder app)
		{
			app.UseAuthorization();
		}

		/// <summary>
		/// Override to configure endpoints
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_Endpoints(IApplicationBuilder app)
		{
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}"
				);
			});
		}

		#endregion
	}
}
