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
	public abstract class Wp<TConfig, Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum>
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
		/// WordPress configuration
		/// </summary>
		public TConfig Config { get; }

		/// <summary>
		/// WordPress database instance
		/// </summary>
		public WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> Db { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="dbConfig">DbConfig</param>
		/// <param name="wpConfig">WpConfig</param>
		/// <param name="log">ILog</param>
		protected Wp(in DbConfig dbConfig, in TConfig wpConfig, in ILog log)
		{
			Config = wpConfig;
			Db = new WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum>(dbConfig, wpConfig, log);
		}
	}
}
