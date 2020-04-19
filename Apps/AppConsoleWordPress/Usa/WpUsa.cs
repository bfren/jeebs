using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Jeebs.Config;
using Jeebs.WordPress;
using Jeebs.WordPress.Entities;
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
		/// <param name="log">ILog</param>
		public WpUsa(IOptions<DbConfig> dbConfig, IOptions<WpUsaConfig> wpConfig, ILog log) : base(dbConfig.Value, wpConfig.Value, log) { }

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
