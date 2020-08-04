using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs.WordPress
{
	/// <summary>
	/// WordPress wrapper
	/// </summary>
	public interface IWp
	{
		/// <summary>
		/// WordPress database instance
		/// </summary>
		IWpDb Db { get; }

		/// <summary>
		/// Register custom post types
		/// </summary>
		void RegisterCustomPostTypes();

		/// <summary>
		/// Register custom taxonomies
		/// </summary>
		void RegisterCustomTaxonomies();
	}

	/// <summary>
	/// WordPress wrapper
	/// </summary>
	/// <typeparam name="TConfig">WpConfig type</typeparam>
	public interface IWp<TConfig> : IWp
		where TConfig : WpConfig
	{
		/// <summary>
		/// WordPress configuration
		/// </summary>
		TConfig Config { get; }
	}
}
