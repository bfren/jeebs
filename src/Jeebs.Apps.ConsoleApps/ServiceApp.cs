// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps.ConsoleApps;

/// <summary>
/// Hosted Service Application (for background tasks) - see <see cref="ConsoleApp"/>
/// </summary>
/// <typeparam name="T">Service type</typeparam>
public abstract class ServiceApp<T> : ConsoleApp
	where T : class, IHostedService
{
	/// <inheritdoc/>
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);

		// Add Hosted Service
		_ = services.AddHostedService<T>();
	}
}
