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
/// API Application - see <see cref="MvcApp"/>.
/// </summary>
public class ApiApp : MvcApp
{
	#region Run

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new void Run(string[] args) =>
		WebAppBuilder.Run<ApiApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new void Run(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Run<ApiApp>(args, configureServices);

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new void Run<T>(string[] args)
		where T : ApiApp, new() =>
		WebAppBuilder.Run<T>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static void RunMinimal(string[] args) =>
		CreateMinimal(args).app.Run();

	/// <inheritdoc cref="WebAppBuilder.Run{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static void RunMinimal(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		CreateMinimal(args, configureServices).app.Run();

	#endregion Run

	#region Create

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication app, ILog<ApiApp> log) Create(string[] args) =>
		WebAppBuilder.Create<ApiApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication app, ILog<ApiApp> log) Create(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Create<ApiApp>(args, configureServices);

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication app, ILog<T> log) Create<T>(string[] args)
		where T : ApiApp, new() =>
		WebAppBuilder.Create<T>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (WebApplication app, ILog<ApiApp> log) CreateMinimal(string[] args) =>
		WebAppBuilder.Create<MinimalApiApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static (WebApplication app, ILog<ApiApp> log) CreateMinimal(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Create<MinimalApiApp>(args, configureServices);

	#endregion Create

	/// <summary>
	/// Create API application with HSTS enabled.
	/// </summary>
	public ApiApp() : this(true) { }

	/// <summary>
	/// Create API application.
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	public ApiApp(bool useHsts) : base(useHsts) { }

	#region ConfigureServices

	/// <inheritdoc/>
	protected override void ConfigureServicesEndpoints(HostBuilderContext ctx, IServiceCollection services) =>
		_ = services.AddControllers(opt => ConfigureServicesMvcOptions(ctx, opt));

	/// <inheritdoc/>
	protected override void ConfigureServicesMvcOptions(HostBuilderContext ctx, MvcOptions opt)
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
		_ = app.MapControllers();

	#endregion Configure
}
