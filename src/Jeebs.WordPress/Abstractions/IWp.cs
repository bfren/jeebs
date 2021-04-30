// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;
using Jeebs.WordPress.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// WordPress wrapper
	/// </summary>
	public interface IWp
	{
		/// <summary>
		/// WordPress Database instance
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
