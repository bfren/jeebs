// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps;

/// <summary>
/// API Application - see <see cref="MvcApp"/>
/// </summary>
public abstract class ApiApp : MvcApp
{
	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	protected ApiApp(bool useHsts) : base(useHsts) { }

	#region ConfigureServices

	/// <inheritdoc/>
	protected override void ConfigureServicesEndpoints(IServiceCollection services)
	{
		_ = services.AddControllers(ConfigureServicesMvcOptions);
	}

	/// <inheritdoc/>
	public override void ConfigureServicesMvcOptions(MvcOptions opt)
	{
		// do nothing
	}

	#endregion ConfigureServices

	#region Configure

	/// <inheritdoc/>
	protected override void ConfigureProductionExceptionHandling(IApplicationBuilder app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureStaticFiles(IHostEnvironment env, IApplicationBuilder app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureCookiePolicy(IApplicationBuilder app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureRedirections(IApplicationBuilder app, IConfiguration config)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureAuthorisation(IApplicationBuilder app, IConfiguration config)
	{
		_ = app.UseAuthorization();
	}

	/// <inheritdoc/>
	protected override void ConfigureEndpoints(IApplicationBuilder app)
	{
		_ = app.UseEndpoints(endpoints => endpoints.MapControllers());
	}

	#endregion Configure
}
