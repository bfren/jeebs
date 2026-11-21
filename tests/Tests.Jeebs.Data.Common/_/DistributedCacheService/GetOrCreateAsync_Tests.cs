// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Jeebs.Data.Common.DistributedCacheService_Tests;

public class GetOrCreateAsync_Tests
{
	private static (DistributedCacheService, Func<Task<int>>, Func<int>) Setup()
	{
		var cache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));
		var service = new DistributedCacheService(new DbConfig(), cache);
		var calls = 0;
		return (service, () => Task.FromResult(++calls), () => calls);
	}

	[Theory]
	[InlineData(1)]
	[InlineData(60)]
	[InlineData(3600)]
	public async Task Positive_Expiration__Caches_Result(int seconds)
	{
		// Arrange
		var (service, fetch, calls) = Setup();
		var query = Rnd.Str;
		var expiration = TimeSpan.FromSeconds(seconds);

		// Act
		var r0 = await service.GetOrCreateAsync(query, null, fetch, expiration);
		var r1 = await service.GetOrCreateAsync(query, null, fetch, expiration);

		// Assert
		Assert.Equal(1, calls());
		Assert.Equal(r0, r1);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-5)]
	public async Task Zero_Or_Negative_Expiration__Does_Not_Cache(int seconds)
	{
		// Arrange
		var (service, fetch, calls) = Setup();
		var query = Rnd.Str;
		var expiration = TimeSpan.FromSeconds(seconds);

		// Act
		_ = await service.GetOrCreateAsync(query, null, fetch, expiration);
		_ = await service.GetOrCreateAsync(query, null, fetch, expiration);

		// Assert
		Assert.Equal(2, calls());
	}
}
