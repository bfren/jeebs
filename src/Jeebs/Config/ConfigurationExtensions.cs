// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Jeebs.Config;

/// <summary>
/// Extension methods for <see cref="IConfiguration"/> objects.
/// </summary>
public static class ConfigurationExtensions
{
	/// <summary>
	/// Caches configuration section values.
	/// </summary>
	private static MemoryCache Cache { get; } = new(new MemoryCacheOptions());

	/// <summary>
	/// Return a configuration section from the cache as type T.
	/// </summary>
	/// <typeparam name="T">Configuration section type.</typeparam>
	/// <param name="config">IConfigurationRoot object.</param>
	/// <param name="sectionKey">Section key.</param>
	/// <returns>Configuration Section as bound object.</returns>
	public static T GetSection<T>(this IConfiguration config, string sectionKey)
		where T : class, new() =>
		GetSection<T>(config, sectionKey, true);

	/// <summary>
	/// Return a configuration section as type T.
	/// </summary>
	/// <typeparam name="T">Configuration settings type.</typeparam>
	/// <param name="config">IConfigurationRoot object.</param>
	/// <param name="sectionKey">Section key.</param>
	/// <param name="useCache">If true the config section will be retrieved from / added to the cache.</param>
	/// <returns>Configuration Section as bound object.</returns>
	public static T GetSection<T>(this IConfiguration config, string sectionKey, bool useCache)
		where T : class, new()
	{
		static T getSection(IConfiguration config, string sectionKey) =>
			config.GetSection(JeebsConfig.GetKey(sectionKey)).Get<T>() ?? new T();

		return useCache switch
		{
			true when Cache.GetOrCreate(typeof(T).FullName ?? typeof(T).Name, _ => getSection(config, sectionKey)) is T section =>
				section,

			_ =>
				getSection(config, sectionKey)
		};
	}
}
