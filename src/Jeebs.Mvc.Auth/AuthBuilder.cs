// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Config.Web.Auth;
using Jeebs.Config.Web.Auth.Jwt;
using Jeebs.Data.Common;
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
/// Fluently configure authentication and authorisation.
/// </summary>
public sealed class AuthBuilder
{
	private readonly IServiceCollection services;

	private readonly AuthenticationBuilder builder;

	private readonly AuthConfig config;

	private bool dataAdded;

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="services">IServiceCollection.</param>
	/// <param name="config">AuthConfig.</param>
	public AuthBuilder(IServiceCollection services, AuthConfig config)
	{
		(this.services, this.config) = (services, config);

		// If auth is not enabled throw an exception
		if (!config.Enabled)
		{
			throw new AuthNotEnabledException("You must configure auth in the Jeebs config file.");
		}

		// Set default authentication scheme
		builder = services.AddAuthentication(config.Scheme switch
		{
			AuthScheme.Cookies =>
				CookieAuthenticationDefaults.AuthenticationScheme,

			AuthScheme.Jwt =>
				JwtBearerDefaults.AuthenticationScheme,

			_ =>
				throw new UnsupportedAuthSchemeException(config.Scheme?.ToString() ?? "unknown")
		});

		// Add cookie
		if (config.Scheme == AuthScheme.Cookies)
		{
			_ = WithCookie();
		}

		// Add JWT
		else if (config.Scheme == AuthScheme.Jwt)
		{
			_ = WithJwt();
		}
	}

	/// <summary>
	/// Enable cookie authentication and authorisation.
	/// </summary>
	/// <returns>AuthBuilder.</returns>
	public AuthBuilder WithCookie()
	{
		_ = builder.AddCookie(opt =>
		{
			opt.LoginPath = new PathString(config.LoginPath ?? "/auth/signin");
			opt.AccessDeniedPath = new PathString(config.AccessDeniedPath ?? "/auth/denied");
		});

		return this;
	}

	/// <summary>
	/// Enable JSON Web Token authentication and authorisation.
	/// </summary>
	/// <exception cref="InvalidJwtConfigurationException"></exception>
	/// <returns>AuthBuilder.</returns>
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
			var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
				.AddRequirements(new JwtRequirement())
				.Build();

			opt.AddPolicy("Token", policy);
			opt.DefaultPolicy = policy;

			opt.InvokeHandlersAfterFailure = false;
		});

		// Add bearer token
		_ = builder.AddJwtBearer();

		// Return builder
		return this;
	}

	/// <inheritdoc cref="WithData{TDbClient, TAdapter, TTypeMapper}(bool)"/>
	public AuthBuilder WithData<TDbClient, TAdapter>(bool useAuthDbClientAsMain)
		where TDbClient : class, IAuthDbClient
		where TAdapter : class, IAdapter
	{
		// Only allow WithData to be called once
		if (dataAdded)
		{
			throw new AuthDataAlreadyAddedException();
		}
		dataAdded = true;

		// Add services
		_ = services.AddAuthData<TDbClient>(useAuthDbClientAsMain);
		_ = services.AddTransient<TAdapter>();
		_ = services.AddTransient<IAdapter>(provider => provider.GetRequiredService<TAdapter>());

		// Return builder
		return this;
	}

	/// <summary>
	/// Enable custom data authentication and authorisation.
	/// </summary>
	/// <typeparam name="TDbClient">IAuthDbClient type.</typeparam>
	/// <typeparam name="TAdapter">IAdapter type.</typeparam>
	/// <typeparam name="TTypeMapper">ITypeMapper type.</typeparam>
	/// <param name="useAuthDbClientAsMain">If true, <typeparamref name="TDbClient"/> will be registered as <see cref="IDbClient"/>.</param>
	/// <exception cref="AuthDataAlreadyAddedException"></exception>
	/// <returns>AuthBuilder.</returns>
	public AuthBuilder WithData<TDbClient, TAdapter, TTypeMapper>(bool useAuthDbClientAsMain)
		where TDbClient : class, IAuthDbClient
		where TAdapter : class, IAdapter
		where TTypeMapper : class, ITypeMapper
	{
		// Add main services
		_ = WithData<TDbClient, TAdapter>(useAuthDbClientAsMain);

		// Add type mapper
		_ = services.AddTransient<TTypeMapper>();
		_ = services.AddTransient<ITypeMapper>(provider => provider.GetRequiredService<TTypeMapper>());

		// Return builder
		return this;
	}
}
