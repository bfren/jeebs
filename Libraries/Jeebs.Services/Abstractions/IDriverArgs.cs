// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver arguments
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public interface IDriverArgs<TConfig> where TConfig : ServiceConfig { }
}
