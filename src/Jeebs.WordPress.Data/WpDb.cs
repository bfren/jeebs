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
		/// <summary>
		/// WpDbQuery
		/// </summary>
		internal WpDbQuery Query { get; private init; }

		/// <inheritdoc/>
		public IWpDbSchema Schema { get; private init; }

		/// <inheritdoc cref="WpDb{Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum}.WpDb(IDbClient, IOptions{DbConfig}, ILog{IWpDb}, WpConfig)"/>
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
			// Create query object
			Query = new(this, log.ForContext<WpDbQuery>());

			// Get WordPress config
			log.Verbose("WordPress Config: {@WpConfig}", wpConfig.Value);

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

		/// <inheritdoc cref="QueryPostsAsync{TModel}(long, Query.GetPostsOptions{Tp})"/>
		public Task<Option<IEnumerable<TModel>>> QueryPostsAsync<TModel>(Query.GetPostsOptions<Tp> opt)
			where TModel : IWithId =>
			QueryPostsF.ExecuteAsync<Tp, Tpm, Tt, TModel>(Query, opt);

		/// <summary>
		/// Query Post objects
		/// </summary>
		/// <param name="page">Page number</param>
		/// <param name="opt">Query options</param>
		public Task<Option<IPagedList<TModel>>> QueryPostsAsync<TModel>(long page, Query.GetPostsOptions<Tp> opt)
			where TModel : IWithId =>
			QueryPostsF.ExecuteAsync<Tp, Tpm, Tt, TModel>(Query, page, opt);

		/// <summary>
		/// Query Terms
		/// </summary>
		/// <param name="opt">Query options</param>
		public Task<Option<IEnumerable<TModel>>> QueryTermsAsync<TModel>(Query.GetTermsOptions<Tt> opt)
			where TModel : IWithId =>
			QueryTermsF.ExecuteAsync<Tt, TModel>(Query, opt);

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
