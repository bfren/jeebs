using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public interface IDriver<TConfig>
		where TConfig : ServiceConfig
	{
		/// <summary>
		/// Driver name
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Driver configuration
		/// </summary>
		public TConfig Config { get; }
	}
}
