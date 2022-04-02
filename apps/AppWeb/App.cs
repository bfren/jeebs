// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace AppWeb;

public class App : Jeebs.Apps.Web.WebApp
{
	public App() : base(false) { }

	public override void Configure(WebApplication app)
	{
		base.Configure(app);

		app.Run(async ctx => await ctx.Response.WriteAsync("Hello, world!").ConfigureAwait(false));
	}
}
