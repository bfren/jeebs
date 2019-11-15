using System;
using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Providers.Dapper;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Tables;
using static Jeebs.Data.Adapters;

namespace Jeebs.WordPress
{
	/// <summary>
	/// WordPress Database Instance
	/// </summary>
	/// <typeparam name="Tc">WpCommentEntity</typeparam>
	/// <typeparam name="Tcm">WpCommentMetaEntity</typeparam>
	/// <typeparam name="Tl">WpLinkEntity</typeparam>
	/// <typeparam name="To">WpOptionEntity</typeparam>
	/// <typeparam name="Tp">WpPostEntity</typeparam>
	/// <typeparam name="Tpm">WpPostMetaEntity</typeparam>
	/// <typeparam name="Tt">WpTermEntity</typeparam>
	/// <typeparam name="Ttm">WpTermMetaEntity</typeparam>
	/// <typeparam name="Ttr">WpTermRelationshipEntity</typeparam>
	/// <typeparam name="Ttt">WpTermTaxonomyEntity</typeparam>
	/// <typeparam name="Tu">WpUserEntity</typeparam>
	/// <typeparam name="Tum">WpUserMetaEntity</typeparam>
	public abstract class WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> : DapperDb<MySqlDbClient>
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
		/// Term Table
		/// </summary>
		public TermTable<Tt> Term { get; set; }

		/// <summary>
		/// Set database connection string, and create tables for schema
		/// </summary>
		/// <param name="dbConfig">DbConfig</param>
		/// <param name="wpConfig">WpConfig</param>
		/// <exception cref="Jx.ConfigurationException">If WordPress database configuration cannot be found</exception>
		protected WpDb(in DbConfig dbConfig, in WpConfig wpConfig)
		{
			if (dbConfig.GetConnection(wpConfig.Db) is DbConnectionConfig cfg)
			{
				ConnectionString = cfg.ConnectionString;

				var adapter = new MySqlAdapter();
				var tablePrefix = wpConfig.TablePrefix ?? cfg.TablePrefix;

				Term = new TermTable<Tt>(adapter, tablePrefix);
			}
			else
			{
				throw new Jx.ConfigException("Unable to determine WordPress database configuration.");
			}
		}
	}
}
