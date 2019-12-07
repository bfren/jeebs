using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using Jeebs.Apps.WebApps.Config;
using Jeebs.Apps.WebApps.Middleware;
using Jeebs.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace Jeebs.Apps.WebApps
{
	/// <summary>
	/// MVC Application bootstrapped using IHost
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

		#region ConfigureServices

		/// <summary>
		/// Configure Services
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="services">IServiceCollection</param>
		protected override void ConfigureServices(in IHostEnvironment env, in IConfiguration config, ref IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, ref services);

			// Response Caching
			ConfigureServices_ResponseCaching(ref services);

			// Response Compression
			ConfigureServices_ResponseCompression(ref services);

			// Routing
			ConfigureServices_Routing(ref services);

			// Endpoints
			ConfigureServices_Endpoints(ref services);
		}

		/// <summary>
		/// Override to configure response caching
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		protected virtual void ConfigureServices_ResponseCaching(ref IServiceCollection services)
		{
			services.AddResponseCaching();
		}

		/// <summary>
		/// Override to configure response compression
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		protected virtual void ConfigureServices_ResponseCompression(ref IServiceCollection services)
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
		protected virtual void ConfigureServices_Routing(ref IServiceCollection services)
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
		protected virtual void ConfigureServices_Endpoints(ref IServiceCollection services)
		{
			services
				.AddControllersWithViews(ConfigureServices_MvcOptions)
				.AddJsonOptions(ConfigureServices_EndpointsJson);
		}

		/// <summary>
		/// Override to configure MVC options
		/// </summary>
		/// <param name="opt">MvcOptions</param>
		public virtual void ConfigureServices_MvcOptions(MvcOptions opt)
		{
			opt.CacheProfiles.Add(Constants.CacheProfiles.None, new CacheProfile() { NoStore = true });
			opt.CacheProfiles.Add(Constants.CacheProfiles.Default, new CacheProfile() { Duration = 600, VaryByQueryKeys = new[] { "*" } });
		}

		/// <summary>
		/// Override to configure endpoints JSON
		/// </summary>
		/// <param name="opt">JsonOptions</param>
		public virtual void ConfigureServices_EndpointsJson(JsonOptions opt)
		{
			opt.JsonSerializerOptions.IgnoreNullValues = (jsonSerialiserSettings ?? Json.DefaultSettings).IgnoreNullValues;
			opt.JsonSerializerOptions.PropertyNamingPolicy = (jsonSerialiserSettings ?? Json.DefaultSettings).PropertyNamingPolicy;
			opt.JsonSerializerOptions.DictionaryKeyPolicy = (jsonSerialiserSettings ?? Json.DefaultSettings).PropertyNamingPolicy;

			opt.JsonSerializerOptions.Converters.Clear();
			foreach (var item in (jsonSerialiserSettings ?? Json.DefaultSettings).Converters)
			{
				opt.JsonSerializerOptions.Converters.Add(item);
			}
		}

		#endregion

		#region Configure

		/// <summary>
		/// Configure Application
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="app">IApplicationBuilder</param>
		protected override void Configure(in IHostEnvironment env, in IConfiguration config, ref IApplicationBuilder app)
		{
			// Base
			base.Configure(env, config, ref app);

			// Compression
			Configure_ResponseCompression(ref app);

			// Static Files
			Configure_StaticFiles(env, ref app);

			// Cookie Policy
			Configure_CookiePolicy(ref app);

			// Response Caching
			Configure_ResponseCaching(ref app);

			// Redirections
			Configure_Redirections(config, ref app);

			// Routing
			Configure_Routing(ref app);

			// Authorisation
			Configure_Authorisation(ref app);

			// Endpoint Routing
			Configure_Endpoints(ref app);
		}

		/// <summary>
		/// Override to configure response compression
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_ResponseCompression(ref IApplicationBuilder app)
		{
			app.UseResponseCompression();
		}

		/// <summary>
		/// Override to configure static files - they must be enabled BEFORE any MVC routing
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_StaticFiles(in IHostEnvironment env, ref IApplicationBuilder app)
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
		protected virtual void Configure_CookiePolicy(ref IApplicationBuilder app)
		{
			app.UseCookiePolicy(cookiePolicyOptions);
		}

		/// <summary>
		/// Override to configure response caching
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_ResponseCaching(ref IApplicationBuilder app)
		{
			app.UseResponseCaching();
		}

		/// <summary>
		/// Override to configure redirections
		/// </summary>
		/// <param name="config">IConfiguration</param>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_Redirections(in IConfiguration config, ref IApplicationBuilder app)
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
		protected virtual void Configure_Routing(ref IApplicationBuilder app)
		{
			app.UseRouting();
		}

		/// <summary>
		/// Override to configure authorisation
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_Authorisation(ref IApplicationBuilder app)
		{
			app.UseAuthorization();
		}

		/// <summary>
		/// Override to configure endpoints
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected virtual void Configure_Endpoints(ref IApplicationBuilder app)
		{
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		#endregion
	}
}
