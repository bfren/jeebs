// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Mapping;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Tables;
using Jeebs.WordPress.TypeHandlers;

namespace Jeebs.WordPress
{
	/// <inheritdoc cref="IWpDb"/>
	/// <remarks>
	/// It should be registered with a DI container as a singleton to avoid the (expensive) table creation
	/// each time the instance is required
	/// </remarks>
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
		/// <inheritdoc/>
		public MySqlAdapter Adapter =>
			Client.Adapter;

		#region Tables

		/// <inheritdoc/>
		public CommentTable Comment { get; }

		/// <inheritdoc/>
		public CommentMetaTable CommentMeta { get; }

		/// <inheritdoc/>
		public LinkTable Link { get; }

		/// <inheritdoc/>
		public OptionTable Option { get; }

		/// <inheritdoc/>
		public PostTable Post { get; }

		/// <inheritdoc/>
		public PostMetaTable PostMeta { get; }

		/// <inheritdoc/>
		public TermTable Term { get; }

		/// <inheritdoc/>
		public TermMetaTable TermMeta { get; }

		/// <inheritdoc/>
		public TermRelationshipTable TermRelationship { get; }

		/// <inheritdoc/>
		public TermTaxonomyTable TermTaxonomy { get; }

		/// <inheritdoc/>
		public UserTable User { get; }

		/// <inheritdoc/>
		public UserMetaTable UserMeta { get; }

		#endregion

		/// <summary>
		/// Set database connection string, and create tables for schema
		/// </summary>
		/// <param name="dbConfig">DbConfig</param>
		/// <param name="wpConfig">WpConfig</param>
		/// <param name="log">ILog</param>
		public WpDb(DbConfig dbConfig, WpConfig wpConfig, ILog log) : base(log)
		{
			// Get connection from WordPress database configuration
			var cfg = dbConfig.GetConnection(wpConfig.Db);
			ConnectionString = cfg.ConnectionString;
			var tablePrefix = wpConfig.TablePrefix ?? cfg.TablePrefix;

			log.Trace("WordPress Config: {@WpConfig}, {@DbConfig}", wpConfig, cfg);

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
			Map<Tc>.To(Comment);
			Map<Tcm>.To(CommentMeta);
			Map<Tl>.To(Link);
			Map<To>.To(Option);
			Map<Tp>.To(Post);
			Map<Tpm>.To(PostMeta);
			Map<Tt>.To(Term);
			Map<Ttm>.To(TermMeta);
			Map<Ttr>.To(TermRelationship);
			Map<Ttt>.To(TermTaxonomy);
			Map<Tu>.To(User);
			Map<Tum>.To(UserMeta);
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

		/// <inheritdoc/>
		public QueryWrapper GetQueryWrapper() =>
			new(this);
	}
}
