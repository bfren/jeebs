using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// WordPress wrapper
	/// </summary>
	/// <typeparam name="TConfig">WpConfig type</typeparam>
	public abstract class Wp<TConfig>
		where TConfig : WpConfig
	{
		/// <summary>
		/// WordPress configuration
		/// </summary>
		public TConfig Config { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="config">WpConfig</param>
		protected Wp(in TConfig config)
		{
			Config = config;
		}
	}
}
