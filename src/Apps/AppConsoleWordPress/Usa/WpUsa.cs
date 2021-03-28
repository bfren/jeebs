// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Config;
using Jeebs.WordPress.Data;
using Jeebs.WordPress;
using Microsoft.Extensions.Options;

namespace AppConsoleWordPress.Usa
{
	/// <summary>
	/// USA WordPress instance
	/// </summary>
	public sealed class WpUsa : Wp<
		WpUsaConfig,
		Entities.Comment,
		Entities.CommentMeta,
		Entities.Link,
		Entities.Option,
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
		/// <param name="logs">DbLogs</param>
		public WpUsa(IOptions<DbConfig> dbConfig, IOptions<WpUsaConfig> wpConfig, DbLogs logs) : base(dbConfig.Value, wpConfig.Value, logs) { }

		/// <summary>
		/// Register custom post types
		/// </summary>
		public override void RegisterCustomPostTypes() { }

		/// <summary>
		/// Register custom taxonomies
		/// </summary>
		public override void RegisterCustomTaxonomies() { }
	}
}
