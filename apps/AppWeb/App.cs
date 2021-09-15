// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AppWeb;

public class App : Jeebs.Apps.WebApp
{
	public App() : base(false) { }

	protected override void Configure(IHostEnvironment env, IApplicationBuilder app, IConfiguration config)
	{
		base.Configure(env, app, config);

		app.Run(async ctx => await ctx.Response.WriteAsync("Hello, world!").ConfigureAwait(false));
	}
}
