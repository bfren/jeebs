using System;
using System.Collections.Generic;
using System.Text;
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
		public MvcAppWithData(bool useHsts) : base(useHsts) { }

		/// <summary>
		/// Configure services to include data
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="services">IServiceCollection</param>
		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, services);

			// Add data
			services.AddData().Using(config);
			services.AddSingleton<TDb>();
		}
	}
}
