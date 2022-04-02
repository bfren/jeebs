// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Clients.PostgreSql;
using Jeebs.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppConsolePg;

internal sealed class App : Jeebs.Apps.App
{
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);

		services.AddSingleton<Db>();
		services.AddTransient<IDb>(p => p.GetRequiredService<Db>());

		services.AddAuthData<PostgreSqlDbClient>(true);

		services.AddTransient<Repository>();
	}
}
