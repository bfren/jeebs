// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Auth.Data;

/// <summary>
/// Auth data functions
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Add required data services for authentication
	/// </summary>
	/// <typeparam name="TDbClient">IAuthDbClient type</typeparam>
	/// <param name="services"></param>
	/// <param name="useAuthDbClientAsMain">If true, <typeparamref name="TDbClient"/> will be registered as <see cref="IDbClient"/></param>
	public static IServiceCollection AddAuthData<TDbClient>(this IServiceCollection services, bool useAuthDbClientAsMain)
		where TDbClient : class, IAuthDbClient
	{
		// Register Auth database classes
		_ = services.AddSingleton<AuthDb>();
		_ = services.AddSingleton<IAuthDb>(s => s.GetRequiredService<AuthDb>());
		_ = services.AddSingleton<TDbClient>();
		_ = services.AddSingleton<IAuthDbClient>(s => s.GetRequiredService<TDbClient>());

		_ = services.AddTransient<AuthDbQuery>();
		_ = services.AddTransient<IAuthDbQuery, AuthDbQuery>();

		// Share auth database with main database
		if (useAuthDbClientAsMain)
		{
			_ = services.AddSingleton<IDbClient>(s => s.GetRequiredService<TDbClient>());
		}

		// Register Auth repositories
		_ = services.AddTransient<AuthUserRepository>();
		_ = services.AddTransient<IAuthUserRepository>(s => s.GetRequiredService<AuthUserRepository>());
		_ = services.AddTransient<IAuthUserRepository<AuthUserEntity>>(s => s.GetRequiredService<AuthUserRepository>());

		_ = services.AddTransient<AuthRoleRepository>();
		_ = services.AddTransient<IAuthRoleRepository>(s => s.GetRequiredService<AuthRoleRepository>());
		_ = services.AddTransient<IAuthRoleRepository<AuthRoleEntity>>(s => s.GetRequiredService<AuthRoleRepository>());

		_ = services.AddTransient<AuthUserRoleRepository>();
		_ = services.AddTransient<IAuthUserRoleRepository>(s => s.GetRequiredService<AuthUserRoleRepository>());
		_ = services.AddTransient<IAuthUserRoleRepository<AuthUserRoleEntity>>(s => s.GetRequiredService<AuthUserRoleRepository>());

		// Register Auth provider
		_ = services.AddTransient<AuthDataProvider>();
		_ = services.AddTransient<IAuthDataProvider>(x => x.GetRequiredService<AuthDataProvider>());

		// Return
		return services;
	}
}
