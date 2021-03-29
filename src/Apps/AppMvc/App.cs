// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using AppMvc.EfCore;
using Jeebs.Auth;
using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Mvc.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppMvc
{
	public sealed class App : Jeebs.Apps.MvcAppWithData
	{
		public App() : base(false) { }

		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			base.ConfigureServices(env, config, services);

			services.AddAuth(config)
				.WithData<MySqlDbClient>()
				.WithJwt();

			services.AddDbContext<EfCoreContext>(
				options => options.UseMySQL("server=192.168.1.104;port=18793;user id=ben;password=Broiler-Viability-Emergency8;database=test;convert zero datetime=True;sslmode=none")
			);
		}

		protected override void Ready(IServiceProvider services)
		{
			base.Ready(services);

			var db = services.GetRequiredService<AuthDb>();
			db.MigrateToLatest();
		}
	}
}
