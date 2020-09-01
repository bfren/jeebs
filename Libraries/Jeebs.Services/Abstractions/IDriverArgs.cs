using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver arguments
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public interface IDriverArgs<TConfig> where TConfig : ServiceConfig { }
}
