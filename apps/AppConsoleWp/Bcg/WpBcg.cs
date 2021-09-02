﻿// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Config;
using Jeebs.WordPress;
using Jeebs.WordPress.Data.Enums;
using Microsoft.Extensions.Options;

namespace AppConsoleWp.Bcg
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
		public WpBcg(IOptions<DbConfig> dbConfig, IOptions<WpBcgConfig> wpConfig, ILog<WpBcg> log) : base(dbConfig, wpConfig, log) { }

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
			public readonly static AudioRecordingCustomField Audio = new();
			public readonly static FeedImageCustomField FeedImage = new();
			public readonly static FirstPreachedCustomField FirstPreached = new();
			public readonly static PassageCustomField Passage = new();
			public readonly static PdfCustomField Pdf = new();
		}

		/// <summary>
		/// Custom Post Types
		/// </summary>
		public static class PostTypes
		{
			public readonly static PostType Sermon = new("sermon");
		}

		/// <summary>
		/// Custom Taxonomies
		/// </summary>
		public static class Taxonomies
		{
			public readonly static Taxonomy BibleBook = new("bible_book");
			public readonly static Taxonomy PlacePreached = new("place_preached");
			public readonly static Taxonomy Section = new("section");
			public readonly static Taxonomy Series = new("series");
			public readonly static Taxonomy Theme = new("theme");
		}
	}
}
