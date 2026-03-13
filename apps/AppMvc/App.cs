// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Data.Adapters.Dapper;
using Jeebs.Logging;
using Jeebs.Mvc.Auth;
using Jeebs.Services.Drawing;
using Jeebs.Services.Drivers.Drawing.Skia;
using Microsoft.AspNetCore.Mvc;
using Wrap.Mvc;

namespace AppMvc;

public sealed class App : Jeebs.Apps.Web.MvcApp
{
	public App() : base(false) { }

	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);

		services.AddAuthentication(ctx.Configuration)
			.WithData<MySqlDbClient, DapperAdapter>(true)
			.WithJwt();

		services.AddTransient<IImageDriver, ImageDriver>();
	}

	protected override void ConfigureAuth(WebApplication app, IConfiguration config)
	{
		app.UseAuthentication();
		base.ConfigureAuth(app, config);
	}

	protected override void ConfigureServicesMvcOptions(HostBuilderContext ctx, MvcOptions opt)
	{
		base.ConfigureServicesMvcOptions(ctx, opt);
		opt.ModelBinderProviders.AddWrapModelBinders();
	}

	public override void Ready(IServiceProvider services, ILog log)
	{
		base.Ready(services, log);

		var db = services.GetRequiredService<AuthDb>();
		db.MigrateToLatest();
	}
}
