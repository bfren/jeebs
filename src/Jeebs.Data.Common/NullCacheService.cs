// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

/// <summary>
/// Null cache service - simply runs the fetch function without caching the result.
/// </summary>
public sealed class NullCacheService : ICacheService
{
	/// <inheritdoc/>
	public Task<T> GetOrCreateAsync<T>(string query, object? param, Func<Task<T>> fetch) =>
		fetch();
}
