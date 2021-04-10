// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress
{
	/// <inheritdoc cref="IWp{TConfig}"/>
	/// <typeparam name="TConfig">WpConfig type</typeparam>
	/// <typeparam name="Tc">WpCommentEntity type</typeparam>
	/// <typeparam name="Tcm">WpCommentMetaEntity type</typeparam>
	/// <typeparam name="Tl">WpLinkEntity type</typeparam>
	/// <typeparam name="To">WpOptionEntity type</typeparam>
	/// <typeparam name="Tp">WpPostEntity type</typeparam>
	/// <typeparam name="Tpm">WpPostMetaEntity type</typeparam>
	/// <typeparam name="Tt">WpTermEntity type</typeparam>
	/// <typeparam name="Ttm">WpTermMetaEntity type</typeparam>
	/// <typeparam name="Ttr">WpTermRelationshipEntity type</typeparam>
	/// <typeparam name="Ttt">WpTermTaxonomyEntity type</typeparam>
	/// <typeparam name="Tu">WpUserEntity type</typeparam>
	/// <typeparam name="Tum">WpUserMetaEntity type</typeparam>
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

		/// <inheritdoc/>
		public TConfig Config { get; private init; }

		/// <inheritdoc/>
		public IWpDb Db { get; private init; }

		/// <inheritdoc/>
		public IWpDbQuery Query { get; private init; }

		/// <summary>
		/// Create object and register custom fields / post types / taxonomies
		/// </summary>
		/// <param name="query">IWpDbQuery</param>
		/// <param name="dbConfig">DbConfig</param>
		/// <param name="wpConfig">WpConfig</param>
		/// <param name="log">ILog</param>
		protected Wp(IWpDbQuery query, IOptions<DbConfig> dbConfig, TConfig wpConfig, ILog<IWpDb> log)
		{
			// Store config and query objects
			Config = wpConfig;
			Query = query;

			// Create new database object using this instance's entity types
			Db = new WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum>(dbConfig, log, wpConfig);

			// Don't need to lock here - if two threads try to register custom types etc,
			// it will simply return false
			if (!initialised)
			{
				initialised = true;
				RegisterCustomPostTypes();
				RegisterCustomTaxonomies();
			}
		}

		/// <inheritdoc/>
		public abstract void RegisterCustomPostTypes();

		/// <inheritdoc/>
		public abstract void RegisterCustomTaxonomies();
	}
}
