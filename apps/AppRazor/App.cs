// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Mvc.Auth;

namespace AppRazor;

public sealed class App : Jeebs.Apps.Web.RazorApp
{
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);

		services.AddAuthentication(ctx.Configuration)
			.WithData<MySqlDbClient>(true);
	}
}
