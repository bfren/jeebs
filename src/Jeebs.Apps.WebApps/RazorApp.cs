// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using MS = Microsoft.AspNetCore.Builder;

namespace Jeebs.Apps.WebApps;

/// <summary>
/// Razor Pages Web Application - see <see cref="MvcApp"/>
/// </summary>
public class RazorApp : MvcApp
{
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
			.AddRazorRuntimeCompilation(ConfigureServicesRuntimeCompilation)
			.AddJsonOptions(ConfigureServicesEndpointsJson);

	/// <summary>
	/// Override to configure Razor Pages options
	/// </summary>
	/// <param name="opt">RazorPagesOptions</param>
	public virtual void ConfigureServicesRazorPagesOptions(RazorPagesOptions opt) { }

	/// <inheritdoc/>
	protected override void ConfigureEndpoints(MS.WebApplication app) =>
		_ = app.UseEndpoints(endpoints => endpoints.MapRazorPages());
}
