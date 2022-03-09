// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.Functions;

/// <summary>
/// Query Posts Functions
/// </summary>
public static partial class QueryPostsF
{
	/// <summary>Messages</summary>
	public static partial class M { }

	/// <summary>
	/// Internal Post Meta type for getting meta values
	/// </summary>
	internal sealed record class PostMeta : WpPostMetaEntity;

	/// <summary>
	/// Internal Term type for linking terms with posts
	/// </summary>
	internal sealed record class Term : TermList.Term
	{
		/// <summary>
		/// Enables query for multiple posts and multiple taxonomies
		/// </summary>
		public long PostId { get; init; }

		/// <summary>
		/// Enables query for multiple posts and multiple taxonomies
		/// </summary>
		public Taxonomy Taxonomy { get; init; } = Taxonomy.Blank;
	}
}
