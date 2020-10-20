using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AppWeb
{
	public class App : Jeebs.Apps.WebApp
	{
		public App() : base(false) { }

		protected override void Configure(IHostEnvironment env, IConfiguration config, IApplicationBuilder app)
		{
			base.Configure(env, config, app);

			app.Run(async ctx => await ctx.Response.WriteAsync("Hello, world!").ConfigureAwait(false));
		}
	}
}
