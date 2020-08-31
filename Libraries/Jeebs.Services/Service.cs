using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service base class
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public abstract class Service<TConfig> : IDriver<TConfig>
		where TConfig : ServiceConfig
	{
		/// <summary>
		/// Driver name
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// ILog object
		/// </summary>
		protected readonly ILog Log;

		/// <summary>
		/// Service configuration
		/// </summary>
		public TConfig Config { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="log">ILog</param>
		/// <param name="config">JeebsConfig</param>
		/// <param name="getCollection">Function to return service configuration collection for this service type</param>
		protected Service(string name, ILog log, JeebsConfig config, Func<ServicesConfig, Dictionary<string, TConfig>> getCollection)
		{
			Name = name;
			Log = log;
			Config = config.Services.GetServiceConfig(getCollection, name);
		}
	}
}
