using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeebs.Apps
{
	///// <summary>
	///// Service Application
	///// </summary>
	///// <typeparam name="T">IHostedService</typeparam>
	//public abstract class ServiceHostBuilder<T> : ConsoleHostBuilder where T : class, IHostedService
	//{
	//	/// <summary>
	//	/// Required Services
	//	/// </summary>
	//	/// <param name="env">IHostEnvironment</param>
	//	/// <param name="config">IConfiguration</param>
	//	/// <param name="services">IServiceCollection</param>
	//	protected override void Services(IHostEnvironment env, IConfiguration config, IServiceCollection services)
	//	{
	//		// Base
	//		base.Services(env, config, services);

	//		// Add Hosted Service
	//		services.AddHostedService<T>();
	//	}
	//}
}
