using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;
using Microsoft.Extensions.Configuration;

namespace Jeebs.Apps
{
	/// <summary>
	/// IConfiguration extension methods
	/// </summary>
	public static class ConfigurationExtensions
	{
		/// <summary>
		/// Get JeebsConfig object from IConfiguration
		/// </summary>
		/// <param name="config">IConfiguration</param>
		public static JeebsConfig GetJeebsConfig(this IConfiguration config) =>
			config.GetSection(JeebsConfig.Key)?.Get<JeebsConfig>() ?? new JeebsConfig();

		/// <summary>
		/// Return a configuration section as type T
		/// </summary>
		/// <typeparam name="T">Configuration settings type</typeparam>
		/// <param name="config">IConfigurationRoot object</param>
		/// <param name="sectionKey">Section key</param>
		/// <returns>Configuration section</returns>
		public static T GetSection<T>(this IConfiguration config, string sectionKey)
			where T : class, new() =>
			config.GetSection(JeebsConfig.GetKey(sectionKey))?.Get<T>() ?? new T();
	}
}
