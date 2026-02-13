// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Jeebs.Config;

/// <summary>
/// <see cref="IConfiguration"/> extension methods.
/// </summary>
public static partial class ConfigurationExtensions
{
	/// <summary>
	/// Caches configuration section values.
	/// </summary>
	private static MemoryCache Cache { get; } = new(new MemoryCacheOptions());
}
