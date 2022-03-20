// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS = Microsoft.AspNetCore.Builder;

namespace Jeebs.Apps.Web;

/// <summary>
/// API Application - see <see cref="MvcApp"/>
/// </summary>
public class ApiApp : MvcApp
{
	/// <summary>
	/// Create API application with HSTS enabled
	/// </summary>
	public ApiApp() : this(true) { }

	/// <summary>
	/// Create API application
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	public ApiApp(bool useHsts) : base(useHsts) =>
		EnableAuthorisation = true;

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
	protected override void ConfigureProductionExceptionHandling(MS.WebApplication app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureStaticFiles(IHostEnvironment env, MS.WebApplication app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureCookiePolicy(MS.WebApplication app)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureRedirections(MS.WebApplication app, IConfiguration config)
	{
		// do nothing
	}

	/// <inheritdoc/>
	protected override void ConfigureEndpoints(MS.WebApplication app) =>
		_ = app.UseEndpoints(endpoints => endpoints.MapControllers());

	#endregion Configure
}
