using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Jeebs.Config
{
	/// <summary>
	/// IConfiguration extension methods
	/// </summary>
	public static class ConfigurationExtensions
	{
		private static readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

		/// <summary>
		/// Get JeebsConfig object from IConfiguration
		/// </summary>
		/// <param name="config">IConfiguration</param>
		public static JeebsConfig GetJeebsConfig(this IConfiguration config)
			=> GetSection<JeebsConfig>(config, JeebsConfig.Key);

		/// <summary>
		/// Return a configuration section as type T
		/// </summary>
		/// <typeparam name="T">Configuration settings type</typeparam>
		/// <param name="config">IConfigurationRoot object</param>
		/// <param name="sectionKey">Section key</param>
		/// <returns>Configuration section</returns>
		public static T GetSection<T>(this IConfiguration config, string sectionKey)
			where T : class, new()
			=> cache.GetOrCreate(typeof(T).FullName, _ => config.GetSection(JeebsConfig.GetKey(sectionKey)).Get<T>() ?? new T());
	}
}
