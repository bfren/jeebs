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
	protected override void ConfigureServices_Endpoints(IServiceCollection services)
	{
		services.AddControllers(ConfigureServices_MvcOptions);
	}

	/// <inheritdoc/>
	public override void ConfigureServices_MvcOptions(MvcOptions opt)
	{
		// do nothing
	}

	#endregion

	#region Configure

	/// <inheritdoc/>
	protected override void Configure_ProductionExceptionHandling(IApplicationBuilder app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void Configure_StaticFiles(IHostEnvironment env, IApplicationBuilder app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void Configure_CookiePolicy(IApplicationBuilder app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void Configure_Redirections(IApplicationBuilder app, IConfiguration config)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void Configure_Auth(IApplicationBuilder app, IConfiguration config)
	{
		app.UseAuthorization();
	}

	/// <inheritdoc/>
	protected override void Configure_Endpoints(IApplicationBuilder app)
	{
		app.UseEndpoints(endpoints => endpoints.MapControllers());
	}

	#endregion
}
