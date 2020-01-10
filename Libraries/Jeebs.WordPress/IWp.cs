using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs.WordPress
{
	/// <summary>
	/// WordPress wrapper
	/// </summary>
	/// <typeparam name="TConfig">WpConfig type</typeparam>
	public interface IWp<TConfig>
		where TConfig : WpConfig
	{
		/// <summary>
		/// WordPress configuration
		/// </summary>
		TConfig Config { get; }

		/// <summary>
		/// WordPress database instance
		/// </summary>
		IWpDb Db { get; }
	}
}
