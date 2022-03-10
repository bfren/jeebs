// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Jeebs.Config;

/// <summary>
/// IConfiguration extension methods
/// </summary>
public static class ConfigurationExtensions
{
	/// <summary>
	/// Caches configuration section values
	/// </summary>
	private static MemoryCache Cache { get; } = new(new MemoryCacheOptions());

	/// <summary>
	/// Return a configuration section from the cache as type T
	/// </summary>
	/// <typeparam name="T">Configuration settings type</typeparam>
	/// <param name="config">IConfigurationRoot object</param>
	/// <param name="sectionKey">Section key</param>
	public static T GetSection<T>(this IConfiguration config, string sectionKey)
		where T : class, new() =>
		GetSection<T>(config, sectionKey, true);

	/// <summary>
	/// Return a configuration section as type T
	/// </summary>
	/// <typeparam name="T">Configuration settings type</typeparam>
	/// <param name="config">IConfigurationRoot object</param>
	/// <param name="sectionKey">Section key</param>
	/// <param name="useCache">If true the config section will be retrieved from / added to the cache</param>
	public static T GetSection<T>(this IConfiguration config, string sectionKey, bool useCache)
		where T : class, new()
	{
		return useCache switch
		{
			true =>
				Cache.GetOrCreate(typeof(T).FullName, _ => getSection(config, sectionKey)),

			false =>
				getSection(config, sectionKey)
		};

		static T getSection(IConfiguration config, string sectionKey) =>
			config.GetSection(JeebsConfig.GetKey(sectionKey)).Get<T>();
	}
}
