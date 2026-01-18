// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Config.Db;
using Jeebs.Config.WordPress;
using Jeebs.Logging;
using Jeebs.WordPress.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress;

/// <summary>
/// Enforces once-only registration of custom WordPress values.
/// </summary>
public abstract class Wp
{
	/// <summary>
	/// Whether or not the class has been initialised
	/// </summary>
	private static MemoryCache Initialised { get; } = new(new MemoryCacheOptions());

	/// <summary>
	/// Initialise WordPress instance - but only once per <typeparamref name="TConfig"/>
	/// </summary>
	/// <typeparam name="TConfig">WpConfig type</typeparam>
	/// <param name="log">ILog</param>
	protected void Init<TConfig>(ILog<Wp> log) =>
		// If a WordPress instance of the specified configuration type hasn't been created yet,
		// call Init() to register custom values
		Initialised.GetOrCreate(typeof(TConfig).FullName ?? typeof(TConfig).Name, entry =>
		{
			// Set expiration to maximum so it never expires while the application is running
			_ = entry.SetAbsoluteExpiration(DateTimeOffset.MaxValue);

			// Do init
			log.Dbg("Initialising {WpDb}.", entry.Key);

			RegisterCustomPostTypes();
			RegisterCustomTaxonomies();

			// It doesn't matter what we return here, because whatever value we store, the
			// factory won't be called a second time
			return true;
		});

	/// <inheritdoc/>
	public abstract void RegisterCustomPostTypes();

	/// <inheritdoc/>
	public abstract void RegisterCustomTaxonomies();
}

/// <inheritdoc cref="IWp{TConfig}"/>
/// <typeparam name="TConfig">WpConfig type</typeparam>
/// <typeparam name="TC">WpCommentEntity type</typeparam>
/// <typeparam name="TCm">WpCommentMetaEntity type</typeparam>
/// <typeparam name="TL">WpLinkEntity type</typeparam>
/// <typeparam name="TO">WpOptionEntity type</typeparam>
/// <typeparam name="TP">WpPostEntity type</typeparam>
/// <typeparam name="TPm">WpPostMetaEntity type</typeparam>
/// <typeparam name="TT">WpTermEntity type</typeparam>
/// <typeparam name="TTm">WpTermMetaEntity type</typeparam>
/// <typeparam name="TTr">WpTermRelationshipEntity type</typeparam>
/// <typeparam name="TTt">WpTermTaxonomyEntity type</typeparam>
/// <typeparam name="TU">WpUserEntity type</typeparam>
/// <typeparam name="TUm">WpUserMetaEntity type</typeparam>
public abstract class Wp<TConfig, TC, TCm, TL, TO, TP, TPm, TT, TTm, TTr, TTt, TU, TUm> : Wp, IWp<TConfig>
	where TConfig : WpConfig
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
	/// <inheritdoc/>
	public TConfig Config { get; private init; }

	/// <inheritdoc cref="IWp.Db"/>
	public WpDb<TC, TCm, TL, TO, TP, TPm, TT, TTm, TTr, TTt, TU, TUm> Db { get; private init; }

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
		Db = new WpDb<TC, TCm, TL, TO, TP, TPm, TT, TTm, TTr, TTt, TU, TUm>(dbConfig, wpConfig, logForDb);

		// Initialise
		Init<WpConfig>(logForDb.ForContext<Wp>());
	}
}
