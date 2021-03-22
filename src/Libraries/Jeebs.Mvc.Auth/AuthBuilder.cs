// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth;
using Jeebs.Auth.Data;
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
		/// Enable data authentication and authorisation
		/// </summary>
		/// <typeparam name="TProvider">IDataAuthProvider type</typeparam>
		/// <typeparam name="TUser">IAuthUser type</typeparam>
		public AuthBuilder WithData<TProvider, TUser>()
			where TProvider : class, IAuthDataProvider<TUser>
			where TUser : IAuthUser
		{
			CheckProvider();

			services.AddScoped<TProvider>();
			services.AddScoped<IAuthDataProvider<TUser>>(s => s.GetRequiredService<TProvider>());

			return this;
		}

		/// <summary>
		/// Enable data authentication and authorisation
		/// </summary>
		/// <typeparam name="TProvider">IDataAuthProvider type</typeparam>
		/// <typeparam name="TUser">IUserModel type</typeparam>
		/// <typeparam name="TRole">IRoleModel type</typeparam>
		public AuthBuilder WithData<TProvider, TUser, TRole>()
			where TProvider : class, IDataAuthProvider<TUser, TRole>
			where TUser : IAuthUser<TRole>
			where TRole : IAuthRole
		{
			CheckProvider();

			services.AddScoped<TProvider>();
			services.AddScoped<IDataAuthProvider<TUser, TRole>>(s => s.GetRequiredService<TProvider>());

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
