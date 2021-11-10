// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppConsoleAuthDb;

internal sealed class App : Jeebs.Apps.ConsoleApp
{
	protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
	{
		base.ConfigureServices(env, config, services);

		// do something
	}
}
