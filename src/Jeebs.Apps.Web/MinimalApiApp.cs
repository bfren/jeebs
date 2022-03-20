// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Apps.Web;

/// <summary>
/// Application supporting minimal API syntax
/// </summary>
public class MinimalApiApp : ApiApp
{
	/// <summary>
	/// Create Minimal API application with HSTS enabled
	/// </summary>
	public MinimalApiApp() : this(true) { }

	/// <summary>
	/// Create Minimal API application
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	public MinimalApiApp(bool useHsts) : base(useHsts) { }

	/// <inheritdoc/>
	protected override void ConfigureServicesEndpoints(IServiceCollection services)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureEndpoints(WebApplication app)
	{
		// do nothing
	}
}
