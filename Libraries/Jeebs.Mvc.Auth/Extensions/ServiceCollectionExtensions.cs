// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;
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
			// Start fluent configuration
			if (config.GetSection<AuthConfig>(AuthConfig.Key) is AuthConfig auth && auth.Enabled)
			{
				return new(@this, auth);
			}

			// Auth must be enable in configuration settings
			throw new Jx.Config.AuthNotEnabledException();
		}
	}
}
