// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps;

/// <summary>
/// Hosted Service Application (for background tasks) - see <see cref="App"/>.
/// </summary>
/// <typeparam name="T">Service type.</typeparam>
public class ServiceApp<T> : App
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
