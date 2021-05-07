// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using F.WordPressF.DataF;
using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.TypeHandlers;
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
		/// <inheritdoc/>
		public IWpDbQuery Query { get; private init; }

		/// <inheritdoc/>
		public IWpDbSchema Schema { get; private init; }

		/// <inheritdoc cref="WpDb{Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum}.WpDb(IDbClient, IOptions{DbConfig}, IOptions{WpConfig}, ILog)"/>
		public WpDb(IOptions<DbConfig> dbConfig, IOptions<WpConfig> wpConfig, ILog log)
			: this(new MySqlDbClient(), dbConfig, wpConfig, log) { }

		/// <summary>
		/// Create tables and map entity types
		/// </summary>
		/// <param name="client">Database client</param>
		/// <param name="dbConfig">Database configuration</param>
		/// <param name="wpConfig">WordPress configuration</param>
		/// <param name="log">ILog</param>
		internal WpDb(IDbClient client, IOptions<DbConfig> dbConfig, IOptions<WpConfig> wpConfig, ILog log)
			: base(client, dbConfig, log, wpConfig.Value.Db)
		{
			// Log WordPress config
			log.Verbose("WordPress Config: {@WpConfig}", wpConfig.Value);

			// Create query object
			Query = new WpDbQuery(this, log.ForContext<IWpDbQuery>());

			// Create schema
			Schema = new WpDbSchema(wpConfig.Value.TablePrefix);

			// Map entities to tables
			Map<Tc>.To(Schema.Comment);
			Map<Tcm>.To(Schema.CommentMeta);
			Map<Tl>.To(Schema.Link);
			Map<To>.To(Schema.Option);
			Map<Tp>.To(Schema.Post);
			Map<Tpm>.To(Schema.PostMeta);
			Map<Tt>.To(Schema.Term);
			Map<Ttm>.To(Schema.TermMeta);
			Map<Ttr>.To(Schema.TermRelationship);
			Map<Ttt>.To(Schema.TermTaxonomy);
			Map<Tu>.To(Schema.User);
			Map<Tum>.To(Schema.UserMeta);
		}

		#region Query Methods

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> QueryPostsAsync<TModel>(Query.GetPostsOptions opt)
			where TModel : IWithId =>
			QueryPostsF.ExecuteAsync<TModel>(this, opt);

		/// <inheritdoc/>
		public Task<Option<IPagedList<TModel>>> QueryPostsAsync<TModel>(long page, Query.GetPostsOptions opt)
			where TModel : IWithId =>
			QueryPostsF.ExecuteAsync<TModel>(this, page, opt);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> QueryTermsAsync<TModel>(Query.GetTermsOptions opt)
			where TModel : IWithId =>
			QueryTermsF.ExecuteAsync<TModel>(this, opt);

		#endregion

		#region Static

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

		#endregion
	}
}
