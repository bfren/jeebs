// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver arguments
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public interface IDriverArgs<TConfig> where TConfig : IServiceConfig { }
}
