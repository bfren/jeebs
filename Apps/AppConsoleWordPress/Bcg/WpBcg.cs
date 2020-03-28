using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Jeebs.Config;
using Jeebs.WordPress;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Enums;
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

		/// <summary>
		/// Register custom fields
		/// </summary>
		public override void RegisterCustomFields() { }

		/// <summary>
		/// Register custom post types
		/// </summary>
		public override void RegisterCustomPostTypes()
		{
			PostType.AddCustomPostType(PostTypes.Sermon);
		}

		/// <summary>
		/// Register custom taxonomies
		/// </summary>
		public override void RegisterCustomTaxonomies()
		{
			Taxonomy.AddCustomTaxonomy(Taxonomies.BibleBook);
			Taxonomy.AddCustomTaxonomy(Taxonomies.PlacePreached);
			Taxonomy.AddCustomTaxonomy(Taxonomies.Section);
			Taxonomy.AddCustomTaxonomy(Taxonomies.Series);
			Taxonomy.AddCustomTaxonomy(Taxonomies.Theme);
		}

		/// <summary>
		/// Custom Fields
		/// </summary>
		public static class CustomFields
		{
			public readonly static AudioRecordingCustomField Audio = new AudioRecordingCustomField();
			public readonly static FirstPreachedCustomField FirstPreached = new FirstPreachedCustomField();
			public readonly static PassageCustomField Passage = new PassageCustomField();
			public readonly static PdfCustomField Pdf = new PdfCustomField();
		}

		/// <summary>
		/// Custom Post Types
		/// </summary>
		public static class PostTypes
		{
			public readonly static PostType Sermon = new PostType("sermon");
		}

		/// <summary>
		/// Custom Taxonomies
		/// </summary>
		public static class Taxonomies
		{
			public readonly static Taxonomy BibleBook = new Taxonomy("bible_book");
			public readonly static Taxonomy PlacePreached = new Taxonomy("place_preached");
			public readonly static Taxonomy Section = new Taxonomy("section");
			public readonly static Taxonomy Series = new Taxonomy("series");
			public readonly static Taxonomy Theme = new Taxonomy("theme");
		}
	}
}
