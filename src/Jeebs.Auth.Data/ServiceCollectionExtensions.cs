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

		_ = services.AddScoped<AuthDbQuery>();
		_ = services.AddScoped<IAuthDbQuery, AuthDbQuery>();

		// Share auth database with main database
		if (useAuthDbClientAsMain)
		{
			_ = services.AddSingleton<IDbClient>(s => s.GetRequiredService<TDbClient>());
		}

		// Register Auth repositories
		_ = services.AddScoped<AuthUserRepository>();
		_ = services.AddScoped<IAuthUserRepository>(s => s.GetRequiredService<AuthUserRepository>());
		_ = services.AddScoped<IAuthUserRepository<AuthUserEntity>>(s => s.GetRequiredService<AuthUserRepository>());

		_ = services.AddScoped<AuthRoleRepository>();
		_ = services.AddScoped<IAuthRoleRepository>(s => s.GetRequiredService<AuthRoleRepository>());
		_ = services.AddScoped<IAuthRoleRepository<AuthRoleEntity>>(s => s.GetRequiredService<AuthRoleRepository>());

		_ = services.AddScoped<AuthUserRoleRepository>();
		_ = services.AddScoped<IAuthUserRoleRepository>(s => s.GetRequiredService<AuthUserRoleRepository>());
		_ = services.AddScoped<IAuthUserRoleRepository<AuthUserRoleEntity>>(s => s.GetRequiredService<AuthUserRoleRepository>());

		// Register Auth provider
		_ = services.AddScoped<AuthDataProvider>();
		_ = services.AddScoped<IAuthDataProvider>(x => x.GetRequiredService<AuthDataProvider>());

		// Return
		return services;
	}
}
