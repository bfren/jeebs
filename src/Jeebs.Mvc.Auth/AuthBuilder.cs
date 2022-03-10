// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Config.Web.Auth;
using Jeebs.Config.Web.Auth.Jwt;
using Jeebs.Mvc.Auth.Exceptions;
using Jeebs.Mvc.Auth.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Auth;

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
				throw new UnsupportedAuthSchemeException(config.Scheme?.ToString() ?? "unknown")
		});

		// Add cookie info
		if (config.Scheme == AuthScheme.Cookies)
		{
			_ = builder.AddCookie(opt =>
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
		_ = services.AddSingleton<AuthDb>();
		_ = services.AddSingleton<IAuthDb>(s => s.GetRequiredService<AuthDb>());
		_ = services.AddSingleton<IAuthDbClient, TDbClient>();

		_ = services.AddScoped<AuthDbQuery>();
		_ = services.AddScoped<IAuthDbQuery>(s => s.GetRequiredService<AuthDbQuery>());

		// Add AuthFunc
		_ = services.AddScoped<AuthUserRepository>();
		_ = services.AddScoped<IAuthUserRepository>(s => s.GetRequiredService<AuthUserRepository>());
		_ = services.AddScoped<IAuthUserRepository<AuthUserEntity>>(s => s.GetRequiredService<AuthUserRepository>());

		_ = services.AddScoped<AuthRoleRepository>();
		_ = services.AddScoped<IAuthRoleRepository>(s => s.GetRequiredService<AuthRoleRepository>());
		_ = services.AddScoped<IAuthRoleRepository<AuthRoleEntity>>(s => s.GetRequiredService<AuthRoleRepository>());

		_ = services.AddScoped<AuthUserRoleRepository>();
		_ = services.AddScoped<IAuthUserRoleRepository>(s => s.GetRequiredService<AuthUserRoleRepository>());
		_ = services.AddScoped<IAuthUserRoleRepository<AuthUserRoleEntity>>(s => s.GetRequiredService<AuthUserRoleRepository>());

		// Add AuthProvider
		_ = services.AddScoped<AuthDataProvider>();
		_ = services.AddScoped<IAuthDataProvider>(x => x.GetRequiredService<AuthDataProvider>());

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
	/// <exception cref="InvalidJwtConfigurationException"></exception>
	public AuthBuilder WithJwt()
	{
		// Ensure JWT configuration is valid
		if (!config.Jwt.IsValid)
		{
			throw new InvalidJwtConfigurationException();
		}

		// Add services
		_ = services.AddScoped<IAuthJwtProvider, AuthJwtProvider>();
		_ = services.AddSingleton<IAuthorizationHandler, JwtHandler>();

		// Add authorisation policy
		_ = services.AddAuthorization(opt =>
		  {
			  opt.AddPolicy("Token", policy =>
				_ = policy
					.AddRequirements(new JwtRequirement())
					.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

			  opt.InvokeHandlersAfterFailure = false;
		  });

		// Add bearer token
		_ = builder.AddJwtBearer();

		// Return builder
		return this;
	}
}
