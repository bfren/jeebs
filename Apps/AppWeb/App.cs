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
	public class App : Jeebs.Apps.WebApps.WebApp
	{
		protected override void Configure(in IHostEnvironment env, in IConfiguration config, ref IApplicationBuilder app)
		{
			base.Configure(env, config, ref app);

			app.Run(async ctx =>
			{
				await ctx.Response.WriteAsync("Hello, world!");
			});
		}
	}
}
