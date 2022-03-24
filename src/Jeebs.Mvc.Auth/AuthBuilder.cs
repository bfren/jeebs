// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Config.Web.Auth;
using Jeebs.Config.Web.Auth.Jwt;
using Jeebs.Data;
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
public sealed class AuthBuilder
{
	private readonly IServiceCollection services;

	private readonly AuthenticationBuilder builder;

	private readonly AuthConfig config;

	private bool dataAdded;

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
	/// <param name="useAuthDbClientAsMain">If true, <typeparamref name="TDbClient"/> will be registered as <see cref="IDbClient"/></param>
	/// <exception cref="AuthDataAlreadyAddedException"></exception>
	public AuthBuilder WithData<TDbClient>(bool useAuthDbClientAsMain)
		where TDbClient : class, IAuthDbClient
	{
		// Only allow WithData to be called once
		if (dataAdded)
		{
			throw new AuthDataAlreadyAddedException();
		}
		dataAdded = true;

		// Add services
		_ = services.AddAuthData<TDbClient>(useAuthDbClientAsMain);

		// Return builder
		return this;
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
