// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Data.Common;

public static class ServiceCollectionExtensions
{
	/// <inheritdoc cref="Data.ServiceCollectionExtensions.AddData{TDb, TDbClient}(IServiceCollection)"/>
	public static IServiceCollection AddData<TDb, TDbClient>(this IServiceCollection @this)
		where TDb : class, IDb
		where TDbClient : class, IDbClient
	{
		// Add Data implementations
		_ = Data.ServiceCollectionExtensions.AddData<TDb, TDbClient>(@this);

		// Add Data.Common implementations
		_ = @this.AddTransient<IDb>(p => p.GetRequiredService<TDb>());
		_ = @this.AddTransient<IDbClient, TDbClient>();

		return @this;
	}

	/// <summary>
	/// Add Db, DbClient and Adapter instances to the <see cref="IServiceCollection"/>.
	/// </summary>
	/// <typeparam name="TDb">Database type.</typeparam>
	/// <typeparam name="TDbClient">Database Client type.</typeparam>
	/// <typeparam name="TAdapter">Adapter type.</typeparam>
	/// <param name="this">IServiceCollection.</param>
	/// <returns>IServiceCollection.</returns>
	public static IServiceCollection AddData<TDb, TDbClient, TAdapter>(this IServiceCollection @this)
		where TDb : class, IDb
		where TDbClient : class, IDbClient
		where TAdapter : class, IAdapter
	{
		// Add Data implementations
		_ = @this.AddData<TDb, TDbClient>();

		// Add Adapter
		_ = @this.AddTransient<TAdapter>();
		_ = @this.AddTransient<IAdapter, TAdapter>();

		return @this;
	}
}
