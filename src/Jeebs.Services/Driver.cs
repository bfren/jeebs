// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Jeebs.Logging;

namespace Jeebs.Services;

/// <summary>
/// Service driver base class
/// </summary>
/// <typeparam name="TConfig">Service configuration type</typeparam>
public abstract class Driver<TConfig> : IDriver<TConfig>
	where TConfig : IServiceConfig
{
	/// <summary>
	/// Driver name
	/// </summary>
	protected string Name { get; private init; }

	/// <summary>
	/// ILog
	/// </summary>
	protected ILog Log { get; private init; }

	/// <summary>
	/// Jeebs configuration
	/// </summary>
	protected JeebsConfig JeebsConfig { get; private init; }

	/// <summary>
	/// Service configuration
	/// </summary>
	protected TConfig ServiceConfig { get; private init; }

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
