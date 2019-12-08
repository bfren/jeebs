using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Jeebs.Config;
using Jeebs.WordPress;
using Jeebs.WordPress.Entities;
using Microsoft.Extensions.Options;

namespace AppConsoleWordPress.Bcg
{
	/// <summary>
	/// BCG WordPress instance
	/// </summary>
	public sealed class WpBcg : Wp<
		WpBcgConfig,
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
		/// <param name="wpConfig">WpBcgConfig</param>
		/// <param name="log">ILog</param>
		public WpBcg(IOptions<DbConfig> dbConfig, IOptions<WpBcgConfig> wpConfig, ILog log) : base(dbConfig.Value, wpConfig.Value, log) { }
	}
}
