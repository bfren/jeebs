// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Data.Common;

public static partial class ServiceCollectionExtensions
{
	/// <summary>
	/// Add distributed memory cache implementation to the service collection,
	/// and register a distribute cache implementation of <see cref="ICacheService"/>.
	/// </summary>
	/// <param name="this">IServiceCollection.</param>
	/// <returns>IServiceCollection.</returns>
	public static IServiceCollection AddDataQueryCache(this IServiceCollection @this)
	{
		_ = @this.AddDistributedMemoryCache();
		_ = @this.AddTransient<ICacheService, DistributedCacheService>();

		return @this;
	}
}
