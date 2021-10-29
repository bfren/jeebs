// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Clients.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppConsolePg;

internal sealed class App : Jeebs.Apps.ConsoleApp
{
	protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
	{
		base.ConfigureServices(env, config, services);

		services.AddSingleton<Db>();
		services.AddTransient<IDb>(p => p.GetRequiredService<Db>());
		services.AddTransient<IDbClient, PostgreSqlDbClient>();

		services.AddTransient<Repository>();
	}
}
