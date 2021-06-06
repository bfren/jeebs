// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Config;
using Jeebs.Mvc.Auth.Exceptions;
using Jeebs.Mvc.Auth.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Auth
{
	/// <summary>
	/// Fluently configure authentication and authorisation
	/// </summary>
	public class AuthBuilder
	{
		private readonly IServiceCollection services;

		private readonly AuthenticationBuilder builder;

		private readonly AuthConfig config;

		private bool providerAdded;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <param name="config">AuthConfig</param>
		public AuthBuilder(IServiceCollection services, AuthConfig config)
		{
			(this.services, this.config) = (services, config);

			// Set default authentication scheme
			builder = services.AddAuthentication(config.Scheme switch
			{
				AuthScheme.Cookies =>
					CookieAuthenticationDefaults.AuthenticationScheme,

				_ =>
					throw new Jx.Config.UnsupportedAuthenticationSchemeException(config.Scheme?.ToString() ?? "unknown")
			});

			// Add cookie info
			if (config.Scheme == AuthScheme.Cookies)
			{
				builder.AddCookie(opt =>
				{
					opt.LoginPath = new PathString(config.LoginPath ?? "/auth/signin");
					opt.AccessDeniedPath = new PathString(config.AccessDeniedPath ?? "/auth/denied");
				});
			}
		}

		/// <summary>
		/// Enable custom data authentication and authorisation
		/// </summary>
		/// <typeparam name="TDbClient">IAuthDbClient type</typeparam>
		public AuthBuilder WithData<TDbClient>()
			where TDbClient : class, IAuthDbClient
		{
			CheckProvider();

			// Add AuthDb
			services.AddSingleton<AuthDb>();
			services.AddSingleton<IAuthDb>(s => s.GetRequiredService<AuthDb>());
			services.AddSingleton<IAuthDbClient, TDbClient>();

			services.AddScoped<AuthDbQuery>();
			services.AddScoped<IAuthDbQuery>(s => s.GetRequiredService<AuthDbQuery>());

			// Add AuthFunc
			services.AddScoped<AuthUserRepository>();
			services.AddScoped<IAuthUserRepository>(s => s.GetRequiredService<AuthUserRepository>());
			services.AddScoped<IAuthUserRepository<AuthUserEntity>>(s => s.GetRequiredService<AuthUserRepository>());

			services.AddScoped<AuthRoleRepository>();
			services.AddScoped<IAuthRoleRepository>(s => s.GetRequiredService<AuthRoleRepository>());
			services.AddScoped<IAuthRoleRepository<AuthRoleEntity>>(s => s.GetRequiredService<AuthRoleRepository>());

			services.AddScoped<AuthUserRoleRepository>();
			services.AddScoped<IAuthUserRoleRepository>(s => s.GetRequiredService<AuthUserRoleRepository>());
			services.AddScoped<IAuthUserRoleRepository<AuthUserRoleEntity>>(s => s.GetRequiredService<AuthUserRoleRepository>());

			// Add AuthProvider
			services.AddScoped<AuthDataProvider>();
			services.AddScoped<IAuthDataProvider>(x => x.GetRequiredService<AuthDataProvider>());

			return this;
		}

		private void CheckProvider()
		{
			if (providerAdded)
			{
				throw new AuthProviderAlreadyAddedException();
			}

			providerAdded = true;
		}

		/// <summary>
		/// Enable JSON Web Token authentication and authorisation
		/// </summary>
		public AuthBuilder WithJwt()
		{
			// Ensure JWT configuration is valid
			if (!config.Jwt.IsValid)
			{
				throw new Jx.Config.InvalidJwtConfigurationException();
			}

			// Add services
			services.AddScoped<IAuthJwtProvider, AuthJwtProvider>();
			services.AddSingleton<IAuthorizationHandler, JwtHandler>();

			// Add authorisation policy
			services.AddAuthorization(opt =>
			{
				opt.AddPolicy("Token", policy =>
				{
					policy
						.AddRequirements(new JwtRequirement())
						.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
				});

				opt.InvokeHandlersAfterFailure = false;
			});

			// Add bearer token
			builder.AddJwtBearer();

			// Return builder
			return this;
		}
	}
}
