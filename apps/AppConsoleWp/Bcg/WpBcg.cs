// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.WordPress;
using Jeebs.WordPress.Enums;
using Microsoft.Extensions.Options;

namespace AppConsoleWp.Bcg;

/// <summary>
/// BCG WordPress instance.
/// </summary>
public sealed class WpBcg : Wp<
	WpBcgConfig,
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
	/// Create instance.
	/// </summary>
	/// <param name="dbConfig">DbConfig.</param>
	/// <param name="wpConfig">WpBcgConfig.</param>
	/// <param name="log">ILog.</param>
	public WpBcg(IOptions<DbConfig> dbConfig, IOptions<WpBcgConfig> wpConfig, Jeebs.Logging.ILog<WpBcg> log) : base(dbConfig, wpConfig, log) { }

	/// <summary>
	/// Register custom post types.
	/// </summary>
	public override void RegisterCustomPostTypes() =>
		PostType.AddCustomPostType(PostTypes.Sermon);

	/// <summary>
	/// Register custom taxonomies.
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
	/// Custom Fields.
	/// </summary>
	public static class CustomFields
	{
		public static readonly AudioRecordingCustomField Audio = new();
		public static readonly FeedImageCustomField FeedImage = new();
		public static readonly FirstPreachedCustomField FirstPreached = new();
		public static readonly PassageCustomField Passage = new();
		public static readonly PdfCustomField Pdf = new();
	}

	/// <summary>
	/// Custom Post Types.
	/// </summary>
	public static class PostTypes
	{
		public static readonly PostType Sermon = new("sermon");
	}

	/// <summary>
	/// Custom Taxonomies.
	/// </summary>
	public static class Taxonomies
	{
		public static readonly Taxonomy BibleBook = new("bible_book");
		public static readonly Taxonomy PlacePreached = new("place_preached");
		public static readonly Taxonomy Section = new("section");
		public static readonly Taxonomy Series = new("series");
		public static readonly Taxonomy Theme = new("theme");
	}
}
