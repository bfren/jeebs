// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Config;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress;

/// <summary>
/// Enforces once-only registration of custom WordPress values
/// </summary>
public abstract class Wp
{
	/// <summary>
	/// Whether or not the class has been initialised
	/// </summary>
	private static readonly MemoryCache initialised = new(new MemoryCacheOptions());

	/// <summary>
	/// Initialise WordPress instance - but only once per <typeparamref name="TConfig"/>
	/// </summary>
	/// <typeparam name="TConfig">WpConfig type</typeparam>
	/// <param name="log">ILog</param>
	protected void Init<TConfig>(ILog<Wp> log)
	{
		// If a WordPress instance of the specified configuration type hasn't been created yet,
		// call Init() to register custom values
		_ = initialised.GetOrCreate(typeof(TConfig).FullName, entry =>
		{
			// Set expiration to maximum so it never expires while the application is running
			entry.SetAbsoluteExpiration(DateTimeOffset.MaxValue);

			// Do init
			log.Dbg("Initialising {WpDb}.", entry.Key);

			RegisterCustomPostTypes();
			RegisterCustomTaxonomies();

			// It doesn't matter what we return here, because whatever value we store, the
			// factory won't be called a second time
			return true;
		});
	}

	/// <inheritdoc/>
	public abstract void RegisterCustomPostTypes();

	/// <inheritdoc/>
	public abstract void RegisterCustomTaxonomies();
}

/// <inheritdoc cref="IWp{TConfig}"/>
/// <typeparam name="TConfig">WpConfig type</typeparam>
/// <typeparam name="Tc">WpCommentEntity type</typeparam>
/// <typeparam name="Tcm">WpCommentMetaEntity type</typeparam>
/// <typeparam name="Tl">WpLinkEntity type</typeparam>
/// <typeparam name="To">WpOptionEntity type</typeparam>
/// <typeparam name="Tp">WpPostEntity type</typeparam>
/// <typeparam name="Tpm">WpPostMetaEntity type</typeparam>
/// <typeparam name="Tt">WpTermEntity type</typeparam>
/// <typeparam name="Ttm">WpTermMetaEntity type</typeparam>
/// <typeparam name="Ttr">WpTermRelationshipEntity type</typeparam>
/// <typeparam name="Ttt">WpTermTaxonomyEntity type</typeparam>
/// <typeparam name="Tu">WpUserEntity type</typeparam>
/// <typeparam name="Tum">WpUserMetaEntity type</typeparam>
public abstract class Wp<TConfig, Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> : Wp, IWp<TConfig>
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
	/// <inheritdoc/>
	public TConfig Config { get; private init; }

	/// <inheritdoc cref="IWp.Db"/>
	public WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> Db { get; private init; }

	/// <inheritdoc/>
	IWpDb IWp.Db =>
		Db;

	/// <summary>
	/// Create object and register custom fields / post types / taxonomies
	/// </summary>
	/// <param name="dbConfig">DbConfig</param>
	/// <param name="wpConfig">WpConfig</param>
	/// <param name="logForDb">ILog</param>
	protected Wp(IOptions<DbConfig> dbConfig, IOptions<TConfig> wpConfig, ILog logForDb)
	{
		// Store config
		Config = wpConfig.Value;

		// Create new database object using this instance's entity types
		Db = new WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum>(dbConfig, wpConfig, logForDb);

		// Initialise
		Init<WpConfig>(logForDb.ForContext<Wp>());
	}
}
