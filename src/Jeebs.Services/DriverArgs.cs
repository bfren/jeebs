// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using Jeebs.Config;
using Microsoft.Extensions.Options;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver arguments base type
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public abstract class DriverArgs<TConfig> : IDriverArgs<TConfig>
		where TConfig : IServiceConfig
	{
		/// <summary>
		/// ILog
		/// </summary>
		public ILog Log { get; }

		/// <summary>
		/// JeebsConfig
		/// </summary>
		public IOptions<JeebsConfig> JeebsConfig { get; }

		/// <summary>
		/// Function to return all service configurations for this type
		/// </summary>
		public Func<ServicesConfig, Dictionary<string, TConfig>> ServiceConfigs { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="jeebsConfig">JeebsConfig</param>
		/// <param name="serviceConfigs">Function to return all service configurations for this type</param>
		protected DriverArgs(
			ILog log,
			IOptions<JeebsConfig> jeebsConfig,
			Func<ServicesConfig, Dictionary<string, TConfig>> serviceConfigs
		) =>
			(Log, JeebsConfig, ServiceConfigs) = (log, jeebsConfig, serviceConfigs);
	}
}
