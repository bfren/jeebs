// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Jeebs.Config.Web.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Auth;

/// <summary>
/// Extension methods for IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Add authentication
	/// </summary>
	/// <param name="this">IServiceCollection</param>
	/// <param name="config">IConfiguration</param>
	/// <exception cref="AuthNotEnabledException"></exception>
	public static AuthBuilder AddAuthentication(this IServiceCollection @this, IConfiguration config)
	{
		// Start fluent configuration
		if (config.GetSection<AuthConfig>(AuthConfig.Key) is AuthConfig auth && auth.Enabled)
		{
			return new(@this, auth);
		}

		// Auth must be enable in configuration settings
		throw new AuthNotEnabledException();
	}
}
