// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppMvc.EfCore;
using Jeebs;
using Jeebs.Auth;
using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Mvc.Auth;
using Jeebs.Services.Drawing;
using Jeebs.Services.Drivers.Drawing.Skia;
using Microsoft.EntityFrameworkCore;

namespace AppMvc;

public sealed class App : Jeebs.Apps.MvcAppWithData
{
	public App() : base(false) { }

	protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
	{
		base.ConfigureServices(env, config, services);

		services.AddAuthentication(config)
			.WithData<MySqlDbClient>()
			.WithJwt();

		services.AddDbContext<EfCoreContext>(
			options => options.UseMySQL("server=192.168.1.104;port=18793;user id=test;password=test;database=test;convert zero datetime=True;sslmode=none")
		);

		services.AddTransient<IImageDriver, ImageDriver>();
	}

	protected override void Configure_Authorisation(IApplicationBuilder app, IConfiguration config)
	{
		app.UseAuthentication();

		base.Configure_Authorisation(app, config);
	}

	public override void Ready(IServiceProvider services, ILog log)
	{
		base.Ready(services, log);

		var db = services.GetRequiredService<AuthDb>();
		db.MigrateToLatest();
	}
}
