// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

/// <summary>
/// Cache service to improve query performance.
/// </summary>
public interface ICacheService
{
	/// <inheritdoc cref="GetOrCreateAsync{T}(string, object?, Func{Task{T}}, TimeSpan)"/>
	Task<T> GetOrCreateAsync<T>(string query, object? param, Func<Task<T>> fetch);

	/// <summary>
	/// Returns the cached value for the given query if it exists, otherwise it will
	/// execute the fetch function to retrieve the value, cache it, and return it.
	/// </summary>
	/// <typeparam name="T">Query return value type.</typeparam>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="fetch">Function to retrieve query result.</param>
	/// <param name="relativeExpiration">The relative expiration time of the cache entry.</param>
	/// <returns>Query result.</returns>
	Task<T> GetOrCreateAsync<T>(string query, object? param, Func<Task<T>> fetch, TimeSpan relativeExpiration);
}
