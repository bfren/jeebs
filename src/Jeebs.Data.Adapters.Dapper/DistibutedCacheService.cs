// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Functions;
using Microsoft.Extensions.Caching.Distributed;

namespace Jeebs.Data.Adapters.Dapper;

/// <inheritdoc/>
/// <param name="cache">The distributed cache instance.</param>
/// <param name="relativeExpiration">The relative expiration time.</param>
public sealed class DistibutedCacheService(IDistributedCache cache, TimeSpan relativeExpiration) : ICacheService
{
	/// <inheritdoc/>
	public async Task<T> GetOrCreateAsync<T>(string query, object? param, Func<Task<T>> fetch)
	{
		// Check cache before fetching
		var key = $"{query}:{JsonF.Serialise(param).Unwrap(_ => JsonF.Empty)}";
		var cachedString = await cache.GetStringAsync(key);
		if (!string.IsNullOrEmpty(cachedString))
		{
			var cachedValue = JsonF.Deserialise<T>(cachedString);
			if (cachedValue.IsOk)
			{
				return cachedValue.Unwrap();
			}
		}

		// Not cached so fetch and store
		var value = await fetch();
		var json = JsonF.Serialise(value).Unwrap(_ => JsonF.Empty);
		await cache.SetStringAsync(key, json, new DistributedCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = relativeExpiration
		});

		return value;
	}
}
