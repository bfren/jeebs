using System;
using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Tables;
using Jeebs.WordPress.TypeHandlers;

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
	public sealed class WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> : Db<MySqlDbClient>
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
		public WpDb(in DbConfig dbConfig, in WpConfig wpConfig, in ILog log) : base(log)
		{
			// Get connection string from WordPress database configuration
			if (dbConfig.GetConnection(wpConfig.Db) is DbConnectionConfig cfg)
			{
				// Set connection string
				ConnectionString = cfg.ConnectionString;

				// Get table prefix for this instance
				var tablePrefix = wpConfig.TablePrefix ?? cfg.TablePrefix;

				// Create table definitions
				Term = new TermTable<Tt>(client.Adapter, tablePrefix);
			}
			else
			{
				throw new Jx.ConfigException("Unable to determine WordPress database configuration.");
			}
		}

		/// <summary>
		/// Add Dapper type handlers
		/// </summary>
		static WpDb()
		{
			Dapper.SqlMapper.AddTypeHandler(new BooleanTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new CommentTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new MimeTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new PostStatusTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new PostTypeTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new TaxonomyTypeHandler());
		}
	}
}
