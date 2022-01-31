// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.TypeHandlers;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress.Data;

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
	public WpConfig WpConfig { get; private init; }

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
		WpConfig = wpConfig.Value;
		log.Verbose("WordPress Config: {@WpConfig}", WpConfig);

		// Create query object
		Query = new WpDbQuery(this, log.ForContext<IWpDbQuery>());

		// Create schema
		Schema = new WpDbSchema(WpConfig.TablePrefix);

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

	/// <inheritdoc/>
	protected override void AddTypeHandlers(DbMapper mapper)
	{
		mapper.ResetTypeHandlers();
		mapper.AddTypeHandler(new CommentTypeTypeHandler());
		mapper.AddTypeHandler(new MimeTypeTypeHandler());
		mapper.AddTypeHandler(new PostStatusTypeHandler());
		mapper.AddTypeHandler(new PostTypeTypeHandler());
		mapper.AddTypeHandler(new TaxonomyTypeHandler());
		mapper.AddStrongIdTypeHandlers();
	}
}
