// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps
{
	/// <summary>
	/// Hosted Service Application (for background tasks) - see <see cref="ConsoleApp"/>
	/// </summary>
	public abstract class ServiceApp<T> : ConsoleApp
		where T : class, IHostedService
	{
		/// <inheritdoc/>
		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, services);

			// Add Hosted Service
			services.AddHostedService<T>();
		}
	}
}
