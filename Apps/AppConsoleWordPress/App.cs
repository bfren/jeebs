using System;
using System.Collections.Generic;
using System.Text;
using AppConsoleWordPress.Bcg;
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
	internal sealed class App : Jeebs.Apps.ConsoleApps.ConsoleApp
	{
		/// <summary>
		/// Register WordPress instances
		/// </summary>
		/// <param name="env">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		/// <param name="services">IServiceCollection</param>
		protected override void ConfigureServices(in IHostEnvironment env, in IConfiguration config, ref IServiceCollection services)
		{
			// Base
			base.ConfigureServices(env, config, ref services);

			// Add Data
			services.AddData().Using(config);

			// Add WordPress
			services.AddWordPressInstance(":wp:bcg").Using<WpBcg, WpBcgConfig>(config);
			services.AddWordPressInstance(":wp:usa").Using<WpUsa, WpUsaConfig>(config);
		}
	}
}
