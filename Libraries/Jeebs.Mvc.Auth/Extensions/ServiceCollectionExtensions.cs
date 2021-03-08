// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Auth
{
	/// <summary>
	/// Extension methods for IServiceCollection
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Add authentication and authorisation
		/// </summary>
		/// <param name="this">IServiceCollection</param>
		/// <param name="config">IConfiguration</param>
		public static AuthBuilder AddAuth(this IServiceCollection @this, IConfiguration config)
		{
			if (config.GetSection<AuthConfig>(AuthConfig.Key) is AuthConfig auth && auth.Enabled)
			{
				// Set default authentication scheme
				var builder = @this.AddAuthentication(auth.Scheme switch
				{
					AuthScheme.Cookies =>
						CookieAuthenticationDefaults.AuthenticationScheme,

					_ =>
						throw new Jx.Config.UnsupportedAuthenticationSchemeException(auth.Scheme?.ToString() ?? "unknown")
				});

				// Add cookie info
				if (auth.Scheme == AuthScheme.Cookies)
				{
					builder.AddCookie(opt =>
					{
						opt.LoginPath = new PathString(auth.LoginPath ?? "/auth/signin");
						opt.AccessDeniedPath = new PathString(auth.AccessDeniedPath ?? "/auth/denied");
					});
				}

				// Start fluent configuration
				return new(@this, builder, auth);
			}

			// Auth must be enable in configuration settings
			throw new Jx.Config.AuthNotEnabledException();
		}
	}
}
