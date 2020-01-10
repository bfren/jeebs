using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress
{
	/// <summary>
	/// WordPress wrapper
	/// </summary>
	/// <typeparam name="TConfig">WpConfig type</typeparam>
	public abstract class Wp<TConfig, Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> : IWp<TConfig>
		where TConfig : WpConfig
		where Tc : WpCommentEntity
		where Tcm : WpCommentMetaEntity
		where Tl : WpLinkEntity
		where To : WpOptionEntity
		where Tp : WpPostEntity
		where Tpm : WpPostMetaEntity
		where Tt : WpTermEntity
		where Ttm : WpTermMetaEntity
		where Ttr : WpTermRelationshipEntity
		where Ttt : WpTermTaxonomyEntity
		where Tu : WpUserEntity
		where Tum : WpUserMetaEntity
	{
		/// <summary>
		/// Whether or not the class has been initialised
		/// </summary>
		private static bool initialised;

		/// <summary>
		/// WordPress configuration
		/// </summary>
		public TConfig Config { get; }

		/// <summary>
		/// WordPress database instance
		/// </summary>
		public IWpDb Db { get; }

		/// <summary>
		/// Create object and register custom fields / post types / taxonomies
		/// </summary>
		/// <param name="dbConfig">DbConfig</param>
		/// <param name="wpConfig">WpConfig</param>
		/// <param name="log">ILog</param>
		protected Wp(in DbConfig dbConfig, in TConfig wpConfig, in ILog log)
		{
			// Store config
			Config = wpConfig;

			// Create new database object using this instance's entity types
			Db = new WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum>(dbConfig, wpConfig, log);

			// Don't need to lock here - if two threads try to register custom types etc,
			// it will simply return false
			if (!initialised)
			{
				initialised = true;
				RegisterCustomFields();
				RegisterCustomPostTypes();
				RegisterCustomTaxonomies();
			}
		}

		/// <summary>
		/// Register custom fields
		/// </summary>
		public abstract void RegisterCustomFields();

		/// <summary>
		/// Register custom post types
		/// </summary>
		public abstract void RegisterCustomPostTypes();

		/// <summary>
		/// Register custom taxonomies
		/// </summary>
		public abstract void RegisterCustomTaxonomies();
	}
}
