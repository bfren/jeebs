﻿using AppConsoleWordPress.Bcg;
using AppConsoleWordPress.Usa;
using Jeebs.Data;
using Jeebs.WordPress;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppConsoleWordPress
{
	/// <summary>
	/// WordPress Console Application
	/// </summary>
	internal sealed class App : Jeebs.Apps.ConsoleApp
	{
		/// <summary>
		/// Register WordPress instances
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="services">IServiceCollection</param>
		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, services);

			// Add Data
			services.AddData().Using(config);

			// Add WordPress
			services.AddWordPressInstance("bcg").Using<WpBcg, WpBcgConfig>(config);
			services.AddWordPressInstance("usa").Using<WpUsa, WpUsaConfig>(config);
		}
	}
}
