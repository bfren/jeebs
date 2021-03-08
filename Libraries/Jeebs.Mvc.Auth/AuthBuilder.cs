// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth;
using Jeebs.Config;
using Jeebs.Mvc.Auth.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <param name="builder">AuthenticationBuilder</param>
		/// <param name="config">AuthConfig</param>
		public AuthBuilder(IServiceCollection services, AuthenticationBuilder builder, AuthConfig config) =>
			(this.services, this.builder, this.config) = (services, builder, config);

		/// <summary>
		/// Enable data authentication and authorisation
		/// </summary>
		/// <typeparam name="T">IDataAuthProvider type</typeparam>
		public AuthBuilder WithData<T>()
			where T : class, IDataAuthProvider
		{
			services.AddScoped<IDataAuthProvider, T>();

			return this;
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
			services.AddScoped<IJwtAuthProvider, JwtAuthProvider>();
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
