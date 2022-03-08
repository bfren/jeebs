// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Apps;

/// <summary>
/// Razor Pages Web Application - see <see cref="MvcApp"/>
/// </summary>
public abstract class RazorApp : MvcApp
{
	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	protected RazorApp(bool useHsts) : base(useHsts) { }

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
	protected override void ConfigureEndpoints(IApplicationBuilder app) =>
		_ = app.UseEndpoints(endpoints => endpoints.MapRazorPages());
}
