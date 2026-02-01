// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Clients.PostgreSql;
using Jeebs.Data.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppConsolePg;

internal sealed class App : Jeebs.Apps.App
{
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);

		services.AddData<Db, PostgreSqlDbClient>();
		//services.AddAuthData<PostgreSqlDbClient>(true);

		services.AddTransient<JsonRepository>();
	}
}
