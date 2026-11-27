// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Config.Db;
using Jeebs.Functions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Jeebs.Data.Common;

/// <inheritdoc/>
/// <param name="dbConfig">Database configuration.</param>
/// <param name="cache">The distributed cache instance.</param>
public sealed class DistributedCacheService(IOptions<DbConfig> dbConfig, IDistributedCache cache) : ICacheService
{
	/// <inheritdoc/>
	public Task<T> GetOrCreateAsync<T>(string query, object? param, Func<Task<T>> fetch) =>
		GetOrCreateAsync(query, param, fetch, TimeSpan.FromSeconds(dbConfig.Value.QueryCacheSeconds));

	/// <inheritdoc/>
	public async Task<T> GetOrCreateAsync<T>(string query, object? param, Func<Task<T>> fetch, TimeSpan relativeExpiration)
	{
		// If expiration is zero or negative, don't cache
		if (relativeExpiration <= TimeSpan.Zero)
		{
			return await fetch();
		}

		// Check cache before fetching - the key includes the result type, because the same query can be
		// read into different types (e.g. WordPress attachments) and must not share an entry
		var key = $"{typeof(T).FullName}:{query}:{JsonF.Serialise(param).Unwrap(_ => JsonF.Empty)}";
		var cachedString = await cache.GetStringAsync(key);
		if (!string.IsNullOrEmpty(cachedString))
		{
			var cachedValue = JsonF.Deserialise<T>(cachedString);
			if (cachedValue is Ok<T> ok)
			{
				return ok.Value;
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
