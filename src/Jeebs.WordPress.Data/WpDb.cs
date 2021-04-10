// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.TypeHandlers;
using Jeebs.WordPress.Tables;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress.Data
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
	public sealed class WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> : Db, IWpDb
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
		#region Tables

		/// <inheritdoc/>
		public CommentTable Comment { get; private init; }

		/// <inheritdoc/>
		public CommentMetaTable CommentMeta { get; private init; }

		/// <inheritdoc/>
		public LinkTable Link { get; private init; }

		/// <inheritdoc/>
		public OptionTable Option { get; private init; }

		/// <inheritdoc/>
		public PostTable Post { get; private init; }

		/// <inheritdoc/>
		public PostMetaTable PostMeta { get; private init; }

		/// <inheritdoc/>
		public TermTable Term { get; private init; }

		/// <inheritdoc/>
		public TermMetaTable TermMeta { get; private init; }

		/// <inheritdoc/>
		public TermRelationshipTable TermRelationship { get; private init; }

		/// <inheritdoc/>
		public TermTaxonomyTable TermTaxonomy { get; private init; }

		/// <inheritdoc/>
		public UserTable User { get; private init; }

		/// <inheritdoc/>
		public UserMetaTable UserMeta { get; private init; }

		#endregion

		/// <inheritdoc cref="WpDb{Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum}.WpDb(IDbClient, IOptions{DbConfig}, ILog{IWpDb}, WpConfig)"/>
		internal WpDb(IOptions<DbConfig> dbConfig, ILog<IWpDb> log, WpConfig wpConfig) : this(new MySqlDbClient(), dbConfig, log, wpConfig)
		{ }

		/// <summary>
		/// Create tables and map entity types
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="dbConfig">DbConfig</param>
		/// <param name="log">ILog</param>
		/// <param name="wpConfig">WpConfig</param>
		internal WpDb(IDbClient client, IOptions<DbConfig> dbConfig, ILog<IWpDb> log, WpConfig wpConfig)
			: base(client, dbConfig, log, wpConfig.Db)
		{
			// Get WordPress config
			log.Verbose("WordPress Config: {@WpConfig}", wpConfig);

			// Create table definitions
			Comment = new CommentTable(wpConfig.TablePrefix);
			CommentMeta = new CommentMetaTable(wpConfig.TablePrefix);
			Link = new LinkTable(wpConfig.TablePrefix);
			Option = new OptionTable(wpConfig.TablePrefix);
			Post = new PostTable(wpConfig.TablePrefix);
			PostMeta = new PostMetaTable(wpConfig.TablePrefix);
			Term = new TermTable(wpConfig.TablePrefix);
			TermMeta = new TermMetaTable(wpConfig.TablePrefix);
			TermRelationship = new TermRelationshipTable(wpConfig.TablePrefix);
			TermTaxonomy = new TermTaxonomyTable(wpConfig.TablePrefix);
			User = new UserTable(wpConfig.TablePrefix);
			UserMeta = new UserMetaTable(wpConfig.TablePrefix);

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
			Dapper.SqlMapper.AddTypeHandler(new CommentTypeTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new MimeTypeTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new PostStatusTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new PostTypeTypeHandler());
			Dapper.SqlMapper.AddTypeHandler(new TaxonomyTypeHandler());
		}
	}
}
