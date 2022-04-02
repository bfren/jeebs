// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Logging;
using Jeebs.WordPress;
using Microsoft.Extensions.Options;

namespace AppConsoleWp.Usa;

/// <summary>
/// USA WordPress instance
/// </summary>
public sealed class WpUsa : Wp<
	WpUsaConfig,
	Entities.Comment,
	Entities.CommentMeta,
	Entities.Link,
	Entities.Opt,
	Entities.Post,
	Entities.PostMeta,
	Entities.Term,
	Entities.TermMeta,
	Entities.TermRelationship,
	Entities.TermTaxonomy,
	Entities.User,
	Entities.UserMeta
>
{
	/// <summary>
	/// Create instance
	/// </summary>
	/// <param name="dbConfig">DbConfig</param>
	/// <param name="wpConfig">WpUsaConfig</param>
	/// <param name="log">ILog</param>
	public WpUsa(IOptions<DbConfig> dbConfig, IOptions<WpUsaConfig> wpConfig, ILog<WpUsa> log) : base(dbConfig, wpConfig, log) { }

	/// <summary>
	/// Register custom post types
	/// </summary>
	public override void RegisterCustomPostTypes()
	{
		// No custom post types
	}

	/// <summary>
	/// Register custom taxonomies
	/// </summary>
	public override void RegisterCustomTaxonomies()
	{
		// No custom taxonomies
	}
}
