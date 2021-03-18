// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppConsole
{
	public class App : Jeebs.Apps.ConsoleApp
	{
		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			base.ConfigureServices(env, config, services);
			services.AddDrivers();
		}
	}
}
