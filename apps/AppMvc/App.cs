// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppMvc.EfCore;
using Jeebs.Auth;
using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Logging;
using Jeebs.Mvc.Auth;
using Jeebs.Mvc.Data;
using Jeebs.Services.Drawing;
using Jeebs.Services.Drivers.Drawing.Skia;
using Microsoft.EntityFrameworkCore;

namespace AppMvc;

public sealed class App : MvcAppWithData
{
	public App() : base(false) { }

	protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
	{
		base.ConfigureServices(env, config, services);

		_ = services.AddAuthentication(config)
			.WithData<MySqlDbClient>()
			.WithJwt();

		_ = services.AddDbContext<EfCoreContext>(
			options => options.UseMySQL("server=192.168.1.104;port=18793;user id=test;password=test;database=test;convert zero datetime=True;sslmode=none")
		);

		_ = services.AddTransient<IImageDriver, ImageDriver>();
	}

	protected override void ConfigureAuthorisation(IApplicationBuilder app, IConfiguration config)
	{
		_ = app.UseAuthentication();

		base.ConfigureAuthorisation(app, config);
	}

	public override void Ready(IServiceProvider services, ILog log)
	{
		base.Ready(services, log);

		var db = services.GetRequiredService<AuthDb>();
		db.MigrateToLatest();
	}
}
