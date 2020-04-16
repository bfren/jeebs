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
	/// It should be registered with a DI container as a singleton to avoid the (expensive) table creation
	/// each time the instance is required
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
	public sealed class WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> : Db<MySqlDbClient>, IWpDb
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
		/// MySqlAdapter
		/// </summary>
		public MySqlAdapter Adapter { get => client.Adapter; }

		/// <summary>
		/// Start a new query
		/// </summary>
		public new QueryWrapper QueryWrapper { get => new QueryWrapper(this); }

		#region Tables

		/// <summary>
		/// Comment Table
		/// </summary>
		public CommentTable Comment { get; }

		/// <summary>
		/// Comment Meta Table
		/// </summary>
		public CommentMetaTable CommentMeta { get; }

		/// <summary>
		/// Link Table
		/// </summary>
		public LinkTable Link { get; }

		/// <summary>
		/// Option Table
		/// </summary>
		public OptionTable Option { get; }

		/// <summary>
		/// Post Table
		/// </summary>
		public PostTable Post { get; }

		/// <summary>
		/// Post Meta Table
		/// </summary>
		public PostMetaTable PostMeta { get; }

		/// <summary>
		/// Term Table
		/// </summary>
		public TermTable Term { get; }

		/// <summary>
		/// Term Meta Table
		/// </summary>
		public TermMetaTable TermMeta { get; }

		/// <summary>
		/// Term Relationship Table
		/// </summary>
		public TermRelationshipTable TermRelationship { get; }

		/// <summary>
		/// Term Taxonomy Table
		/// </summary>
		public TermTaxonomyTable TermTaxonomy { get; }

		/// <summary>
		/// User Table
		/// </summary>
		public UserTable User { get; }

		/// <summary>
		/// User Meta Table
		/// </summary>
		public UserMetaTable UserMeta { get; }

		#endregion

		/// <summary>
		/// Set database connection string, and create tables for schema
		/// </summary>
		/// <param name="dbConfig">DbConfig</param>
		/// <param name="wpConfig">WpConfig</param>
		/// <param name="log">ILog</param>
		/// <exception cref="Jx.ConfigException">If WordPress database configuration cannot be found</exception>
		public WpDb(DbConfig dbConfig, WpConfig wpConfig, ILog log) : base(log)
		{
			// Get connection string from WordPress database configuration
			if (dbConfig.GetConnection(wpConfig.Db) is DbConnectionConfig cfg)
			{
				// Set connection string
				ConnectionString = cfg.ConnectionString;

				// Get table prefix for this instance
				var tablePrefix = wpConfig.TablePrefix ?? cfg.TablePrefix;

				// Create table definitions
				Comment = new CommentTable(tablePrefix);
				CommentMeta = new CommentMetaTable(tablePrefix);
				Link = new LinkTable(tablePrefix);
				Option = new OptionTable(tablePrefix);
				Post = new PostTable(tablePrefix);
				PostMeta = new PostMetaTable(tablePrefix);
				Term = new TermTable(tablePrefix);
				TermMeta = new TermMetaTable(tablePrefix);
				TermRelationship = new TermRelationshipTable(tablePrefix);
				TermTaxonomy = new TermTaxonomyTable(tablePrefix);
				User = new UserTable(tablePrefix);
				UserMeta = new UserMetaTable(tablePrefix);

				// Map entities to tables
				Map<Tc>.To(Comment, Adapter);
				Map<Tcm>.To(CommentMeta, Adapter);
				Map<Tl>.To(Link, Adapter);
				Map<To>.To(Option, Adapter);
				Map<Tp>.To(Post, Adapter);
				Map<Tpm>.To(PostMeta, Adapter);
				Map<Tt>.To(Term, Adapter);
				Map<Ttm>.To(TermMeta, Adapter);
				Map<Ttr>.To(TermRelationship, Adapter);
				Map<Ttt>.To(TermTaxonomy, Adapter);
				Map<Tu>.To(User, Adapter);
				Map<Tum>.To(UserMeta, Adapter);
			}
			else
			{
				throw new Jx.ConfigException("Unable to determine WordPress database configuration.");
			}
		}

		/// <summary>
		/// Add Dapper type handlers
		/// This is in the static constructor so it only happens once per application load
		/// </summary>
		static WpDb()
		{
			Dapper.SqlMapper.ResetTypeHandlers();
			Dapper.SqlMapper.AddTypeHandler(new BooleanTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new CommentTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new MimeTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new PostStatusTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new PostTypeTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new TaxonomyTypeHandler());
		}
	}
}
