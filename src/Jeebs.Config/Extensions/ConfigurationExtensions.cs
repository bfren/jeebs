// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Jeebs.Config
{
	/// <summary>
	/// IConfiguration extension methods
	/// </summary>
	public static class ConfigurationExtensions
	{
		/// <summary>
		/// Caches configuration section values
		/// </summary>
		private static readonly MemoryCache cache = new(new MemoryCacheOptions());

		/// <summary>
		/// Return a configuration section as type T
		/// </summary>
		/// <typeparam name="T">Configuration settings type</typeparam>
		/// <param name="config">IConfigurationRoot object</param>
		/// <param name="sectionKey">Section key</param>
		/// <param name="useCache">[Optional] If true the config section will be retrieved from / added to the cache</param>
		public static T GetSection<T>(this IConfiguration config, string sectionKey, bool useCache = true)
			where T : class, new()
		{
			return useCache switch
			{
				true =>
					cache.GetOrCreate(typeof(T).FullName, _ => getSection(config, sectionKey)),

				false =>
					getSection(config, sectionKey)
			};

			static T getSection(IConfiguration config, string sectionKey) =>
				config.GetSection(JeebsConfig.GetKey(sectionKey)).Get<T>() ?? new T();
		}
	}
}
