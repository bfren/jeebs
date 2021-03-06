﻿using Jeebs.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppMvc
{
	public sealed class App : Jeebs.Apps.MvcApp
	{
		public App() : base(false) { }

		protected override void ConfigureServices(IHostEnvironment env, IConfiguration config, IServiceCollection services)
		{
			base.ConfigureServices(env, config, services);

			services.AddScoped<IDataAuthProvider, Fake.DataAuthProvider>();
			services.AddScoped<IJwtAuthProvider, JwtAuthProvider>();
		}
	}
}
