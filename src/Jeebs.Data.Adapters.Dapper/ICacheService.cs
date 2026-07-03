// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;

namespace Jeebs.Data.Adapters.Dapper;

/// <summary>
/// Cache service to improve query performance.
/// </summary>
public interface ICacheService
{
	/// <summary>
	/// Returns the cached value for the given query if it exists, otherwise it will
	/// execute the fetch function to retrieve the value, cache it, and return it.
	/// </summary>
	/// <typeparam name="T">Query return value type.</typeparam>
	/// <param name="query">Database query string.</param>
	/// <param name="param">Query parameters.</param>
	/// <param name="fetch">Function to retrieve query result.</param>
	/// <returns>Query result.</returns>
	Task<T> GetOrCreateAsync<T>(string query, object? param, Func<Task<T>> fetch);
}
