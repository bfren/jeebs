// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Config.WordPress;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.TypeHandlers;
using Jeebs.Logging;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.TypeHandlers;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress;

/// <inheritdoc cref="IWpDb"/>
/// <remarks>
/// It should be registered with a DI container as a singleton to avoid the (expensive) table creation
/// each time the instance is required
/// </remarks>
/// <typeparam name="TC">WpCommentEntity</typeparam>
/// <typeparam name="TCm">WpCommentMetaEntity</typeparam>
/// <typeparam name="TL">WpLinkEntity</typeparam>
/// <typeparam name="TO">WpOptionEntity</typeparam>
/// <typeparam name="TP">WpPostEntity</typeparam>
/// <typeparam name="TPm">WpPostMetaEntity</typeparam>
/// <typeparam name="TT">WpTermEntity</typeparam>
/// <typeparam name="TTm">WpTermMetaEntity</typeparam>
/// <typeparam name="TTr">WpTermRelationshipEntity</typeparam>
/// <typeparam name="TTt">WpTermTaxonomyEntity</typeparam>
/// <typeparam name="TU">WpUserEntity</typeparam>
/// <typeparam name="TUm">WpUserMetaEntity</typeparam>
public sealed class WpDb<TC, TCm, TL, TO, TP, TPm, TT, TTm, TTr, TTt, TU, TUm> : Db, IWpDb
	where TC : WpCommentEntity
	where TCm : WpCommentMetaEntity
	where TL : WpLinkEntity
	where TO : WpOptionEntity
	where TP : WpPostEntity
	where TPm : WpPostMetaEntity
	where TT : WpTermEntity
	where TTm : WpTermMetaEntity
	where TTr : WpTermRelationshipEntity
	where TTt : WpTermTaxonomyEntity
	where TU : WpUserEntity
	where TUm : WpUserMetaEntity
{
	private static readonly object X = new();

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
		log.Vrb("WordPress Config: {@WpConfig}", WpConfig);

		// Create query object
		Query = new WpDbQuery(this, log.ForContext<IWpDbQuery>());

		// Create schema
		Schema = new WpDbSchema(WpConfig.TablePrefix);

		// Map entities to tables
		_ = Map<TC>.To(Schema.Comments);
		_ = Map<TCm>.To(Schema.CommentsMeta);
		_ = Map<TL>.To(Schema.Links);
		_ = Map<TO>.To(Schema.Options);
		_ = Map<TP>.To(Schema.Posts);
		_ = Map<TPm>.To(Schema.PostsMeta);
		_ = Map<TT>.To(Schema.Terms);
		_ = Map<TTm>.To(Schema.TermsMeta);
		_ = Map<TTr>.To(Schema.TermRelationships);
		_ = Map<TTt>.To(Schema.TermTaxonomies);
		_ = Map<TU>.To(Schema.Users);
		_ = Map<TUm>.To(Schema.UsersMeta);

		lock (X)
		{
			// Add type handlers
			if (!client.Types.HasTypeHandler<WpPostId>())
			{
				client.Types.ResetTypeHandlers();
				client.Types.AddTypeHandler(new CommentTypeTypeHandler());
				client.Types.AddTypeHandler(new MimeTypeTypeHandler());
				client.Types.AddTypeHandler(new PostStatusTypeHandler());
				client.Types.AddTypeHandler(new PostTypeTypeHandler());
				client.Types.AddTypeHandler(new TaxonomyTypeHandler());
				client.Types.AddStrongIdTypeHandlers();
			}
		}
	}
}
