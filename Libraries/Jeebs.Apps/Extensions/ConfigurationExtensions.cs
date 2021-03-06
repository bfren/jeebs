// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
