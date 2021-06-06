// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public interface IDriver<TConfig> where TConfig : IServiceConfig { }
}
