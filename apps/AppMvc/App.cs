// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppMvc.EfCore;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Logging;
using Jeebs.Mvc.Auth;
using Jeebs.Mvc.Data;
using Jeebs.Services.Drawing;
using Jeebs.Services.Drivers.Drawing.Skia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMvc;

public sealed class App : Jeebs.Apps.Web.MvcApp
{
	public App() : base(false) { }

	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);

		services.AddAuthentication(ctx.Configuration)
			.WithData<MySqlDbClient>(true)
			.WithJwt();

		services.AddDbContext<EfCoreContext>(
			options => options.UseMySQL("server=192.168.1.104;port=18793;user id=test;password=test;database=test;convert zero datetime=True;sslmode=none")
		);

		services.AddTransient<IImageDriver, ImageDriver>();
	}

	protected override void ConfigureAuthorisation(WebApplication app, IConfiguration config)
	{
		app.UseAuthentication();
		base.ConfigureAuthorisation(app, config);
	}

	protected override void ConfigureServicesMvcOptions(MvcOptions opt)
	{
		base.ConfigureServicesMvcOptions(opt);

		opt.AddStrongIdModelBinding();
	}

	public override void Ready(IServiceProvider services, ILog log)
	{
		base.Ready(services, log);

		var db = services.GetRequiredService<AuthDb>();
		db.MigrateToLatest();
	}
}
