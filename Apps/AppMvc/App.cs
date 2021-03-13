// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using AppMvc.Models;
using Jeebs.Mvc.Auth;
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

			services.AddAuth(config)
				.WithData<Fake.DataAuthProviderWithRole, UserModel, RoleModel>()
				.WithJwt();
		}
	}
}
