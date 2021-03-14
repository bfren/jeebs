// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public interface IDriver<TConfig> where TConfig : IServiceConfig { }
}
