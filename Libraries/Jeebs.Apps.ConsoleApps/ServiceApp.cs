using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps.ConsoleApps
{
	/// <summary>
	/// Service Application bootstrapped using IHost
	/// </summary>
	/// <typeparam name="T">IHostedService</typeparam>
	public abstract class ServiceApp<T> : ConsoleApp
		where T : class, IHostedService
	{
		/// <summary>
		/// Configure Services
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="services">IServiceCollection</param>
		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, services);

			// Add Hosted Service
			services.AddHostedService<T>();
		}
	}
}
