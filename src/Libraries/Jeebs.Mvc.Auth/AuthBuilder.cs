// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			services.AddScoped<IAuthDbClient, TDbClient>();

			// Add AuthFunc
			services.AddScoped<AuthUserFunc>();
			services.AddScoped<IAuthUserFunc<AuthUserEntity>>(s => s.GetRequiredService<AuthUserFunc>());
			services.AddScoped<AuthRoleFunc>();
			services.AddScoped<IAuthRoleFunc<AuthRoleEntity>>(s => s.GetRequiredService<AuthRoleFunc>());
			services.AddScoped<AuthUserRoleFunc>();
			services.AddScoped<IAuthUserRoleFunc<AuthUserRoleEntity>>(s => s.GetRequiredService<AuthUserRoleFunc>());

			// Add AuthProvider
			services.AddScoped<AuthDataProvider>();
			services.AddScoped<IAuthDataProvider<AuthUserEntity, AuthRoleEntity, AuthUserRoleEntity>>(
				x => x.GetRequiredService<AuthDataProvider>()
			);

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
