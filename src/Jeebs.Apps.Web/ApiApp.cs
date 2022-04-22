// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps.Web;

/// <summary>
/// API Application - see <see cref="MvcApp"/>
/// </summary>
public class ApiApp : MvcApp
{
	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication, ILog<ApiApp>) Create(string[] args) =>
		WebAppBuilder.Create<ApiApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication, ILog<ApiApp>) Create(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Create<ApiApp>(args, configureServices);

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication, ILog<T>) Create<T>(string[] args)
		where T : ApiApp, new() =>
		WebAppBuilder.Create<T>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (WebApplication, ILog<ApiApp>) CreateMinimal(string[] args) =>
		WebAppBuilder.Create<MinimalApiApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (WebApplication, ILog<ApiApp>) CreateMinimal(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Create<MinimalApiApp>(args, configureServices);

	/// <summary>
	/// Create API application with HSTS enabled
	/// </summary>
	public ApiApp() : this(true) { }

	/// <summary>
	/// Create API application
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	public ApiApp(bool useHsts) : base(useHsts) { }

	#region ConfigureServices

	/// <inheritdoc/>
	protected override void ConfigureServicesEndpoints(IServiceCollection services) =>
		_ = services.AddControllers(ConfigureServicesMvcOptions);

	/// <inheritdoc/>
	protected override void ConfigureServicesMvcOptions(MvcOptions opt)
	{
		// do nothing
	}

	#endregion ConfigureServices

	#region Configure

	/// <inheritdoc/>
	protected override void ConfigureProductionExceptionHandling(WebApplication app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureStaticFiles(IHostEnvironment env, WebApplication app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureCookiePolicy(WebApplication app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureRedirections(WebApplication app, IConfiguration config)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureEndpoints(WebApplication app) =>
		_ = app.UseEndpoints(endpoints => endpoints.MapControllers());

	#endregion Configure
}
