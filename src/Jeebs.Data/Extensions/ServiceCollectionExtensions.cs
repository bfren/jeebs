// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Data;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/> objects.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Add Db and DbClient instances to the <see cref="IServiceCollection"/>.
	/// </summary>
	/// <typeparam name="TDb">Database type.</typeparam>
	/// <typeparam name="TDbClient">Database Client type.</typeparam>
	/// <param name="this">IServiceCollection.</param>
	/// <returns>IServiceCollection.</returns>
	public static IServiceCollection AddData<TDb, TDbClient>(this IServiceCollection @this)
		where TDb : class, IDb
		where TDbClient : class, IDbClient
	{
		_ = @this.AddSingleton<TDb>();
		_ = @this.AddTransient<IDb>(p => p.GetRequiredService<TDb>());

		_ = @this.AddTransient<TDbClient>();
		_ = @this.AddTransient<IDbClient, TDbClient>();

		return @this;
	}
}
