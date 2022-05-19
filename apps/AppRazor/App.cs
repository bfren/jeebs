// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Mvc.Auth;
using Serilog;

namespace AppRazor;

public sealed class App : Jeebs.Apps.Web.RazorApp
{
	public override void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
	{
		base.ConfigureServices(ctx, services);

		services.AddAuthentication(ctx.Configuration)
			.WithData<MySqlDbClient>(true);
	}

	public override void ConfigureSerilog(HostBuilderContext ctx, LoggerConfiguration loggerConfig)
	{
		base.ConfigureSerilog(ctx, loggerConfig);
		_ = loggerConfig.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning);
	}
}
