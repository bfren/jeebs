using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
		/// <summary>
		/// Get JeebsConfig object from IConfiguration
		/// </summary>
		/// <param name="config">IConfiguration</param>
		public static JeebsConfig GetJeebsConfig(this IConfiguration config) =>
			config.GetSection<JeebsConfig>(JeebsConfig.Key);
	}
}
