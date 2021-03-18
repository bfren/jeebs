// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps
{
	/// <summary>
	/// MVC Application bootstrapped using IHost - with Data access enabled
	/// </summary>
	/// <typeparam name="TDb">IDb type</typeparam>
	public abstract class MvcAppWithData<TDb> : MvcApp
		where TDb : class, IDb
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
		protected MvcAppWithData(bool useHsts) : base(useHsts) { }

		/// <inheritdoc/>
		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, services);

			// Add data
			services.AddSingleton<TDb>();
		}
	}
}
