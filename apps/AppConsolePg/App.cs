// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Clients.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppConsolePg;

internal sealed class App : Jeebs.Apps.App
{
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);

		_ = services.AddSingleton<Db>();
		_ = services.AddTransient<IDb>(p => p.GetRequiredService<Db>());
		_ = services.AddTransient<IDbClient, PostgreSqlDbClient>();

		_ = services.AddTransient<Repository>();
	}
}
