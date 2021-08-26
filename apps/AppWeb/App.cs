// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace AppWeb
{
	public class App : Jeebs.Apps.WebApp
	{
		public App() : base(false) { }

		protected override void Configure(IHostEnvironment env, IApplicationBuilder app, IConfiguration config)
		{
			base.Configure(env, app, config);

			app.Run(async ctx => await ctx.Response.WriteAsync("Hello, world!").ConfigureAwait(false));
		}
	}
}
