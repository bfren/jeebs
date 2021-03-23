// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Auth;
using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Mvc.Auth;
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
		}

		protected override void Ready(IServiceProvider services)
		{
			base.Ready(services);

			var db = services.GetRequiredService<AuthDb>();
			db.MigrateToLatest();
		}
	}
}
