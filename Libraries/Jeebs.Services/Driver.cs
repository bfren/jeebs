using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver base class
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public abstract class Driver<TConfig> : IDriver<TConfig>
		where TConfig : ServiceConfig
	{
		/// <summary>
		/// Driver name
		/// </summary>
		protected readonly string Name;

		/// <summary>
		/// ILog
		/// </summary>
		protected readonly ILog Log;

		/// <summary>
		/// Jeebs configuration
		/// </summary>
		protected readonly JeebsConfig JeebsConfig;

		/// <summary>
		/// Service configuration
		/// </summary>
		protected readonly TConfig ServiceConfig;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">DriverArgs</param>
		protected Driver(string name, DriverArgs<TConfig> args)
		{
			(Name, Log, JeebsConfig) = (name, args.Log, args.JeebsConfig.Value);
			ServiceConfig = JeebsConfig.Services.GetServiceConfig(args.ServiceConfigs, name);
		}
	}
}
