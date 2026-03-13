// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Auth.Data;

public static partial class ServiceCollectionExtensions
{
	/// <summary>
	/// Add required data services for Auth.
	/// </summary>
	/// <typeparam name="TDbClient">IAuthDbClient type.</typeparam>
	/// <param name="services">IServiceCollection.</param>
	/// <param name="useAuthDbClientAsMain">If true, <typeparamref name="TDbClient"/> will be registered as <see cref="IDbClient"/>.</param>
	public static IServiceCollection AddAuthData<TDbClient>(this IServiceCollection services, bool useAuthDbClientAsMain)
		where TDbClient : class, IAuthDbClient
	{
		// Register Auth database classes
		_ = services.AddSingleton<AuthDb>();
		_ = services.AddSingleton<IAuthDb>(s => s.GetRequiredService<AuthDb>());
		_ = services.AddSingleton<TDbClient>();
		_ = services.AddSingleton<IAuthDbClient>(s => s.GetRequiredService<TDbClient>());

		// Share Auth database with main database
		if (useAuthDbClientAsMain)
		{
			_ = services.AddSingleton<IDbClient>(s => s.GetRequiredService<TDbClient>());
		}

		// Register Auth repositories
		_ = services.AddTransient<AuthRoleRepository>();
		_ = services.AddTransient<IAuthRoleRepository>(s => s.GetRequiredService<AuthRoleRepository>());
		_ = services.AddTransient<AuthUserRepository>();
		_ = services.AddTransient<IAuthUserRepository>(s => s.GetRequiredService<AuthUserRepository>());
		_ = services.AddTransient<AuthUserRoleRepository>();
		_ = services.AddTransient<IAuthUserRoleRepository>(s => s.GetRequiredService<AuthUserRoleRepository>());

		// Register Auth provider
		_ = services.AddTransient<AuthDataProvider>();
		_ = services.AddTransient<IAuthDataProvider>(x => x.GetRequiredService<AuthDataProvider>());

		// Return
		return services;
	}
}
