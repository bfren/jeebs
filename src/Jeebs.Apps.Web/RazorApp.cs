// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps.Web;

/// <summary>
/// Razor Pages Web Application - see <see cref="MvcApp"/>
/// </summary>
public class RazorApp : MvcApp
{
	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication, ILog<RazorApp>) Create(string[] args) =>
		WebAppBuilder.Create<RazorApp>(args, (_, _) => { });

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication, ILog<RazorApp>) Create(string[] args, Action<HostBuilderContext, IServiceCollection> configureServices) =>
		WebAppBuilder.Create<RazorApp>(args, configureServices);

	/// <inheritdoc cref="WebAppBuilder.Create{T}(string[], Action{HostBuilderContext, IServiceCollection})"/>
	public static new (WebApplication, ILog<T>) Create<T>(string[] args)
		where T : RazorApp, new() =>
		WebAppBuilder.Create<T>(args, (_, _) => { });

	/// <summary>
	/// Create Razor application with HSTS enabled
	/// </summary>
	public RazorApp() : this(true) { }

	/// <summary>
	/// Create Razor application
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	public RazorApp(bool useHsts) : base(useHsts) { }

	/// <inheritdoc/>
	protected override void ConfigureServicesEndpoints(IServiceCollection services) =>
		_ = services
			.AddRazorPages(ConfigureServicesRazorPagesOptions)
			.AddMvcOptions(ConfigureServicesMvcOptions)
			.AddRazorRuntimeCompilation(ConfigureServicesRuntimeCompilation)
			.AddJsonOptions(ConfigureServicesEndpointsJson);

	/// <summary>
	/// Override to configure Razor Pages options
	/// </summary>
	/// <param name="opt">RazorPagesOptions</param>
	public virtual void ConfigureServicesRazorPagesOptions(RazorPagesOptions opt) { }

	/// <inheritdoc/>
	protected override void ConfigureEndpoints(WebApplication app) =>
		_ = app.UseEndpoints(endpoints => endpoints.MapRazorPages());
}
