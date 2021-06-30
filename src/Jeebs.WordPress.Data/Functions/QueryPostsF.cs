// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace F.WordPressF.DataF
{
	/// <summary>
	/// Query Posts Functions
	/// </summary>
	public static partial class QueryPostsF
	{
		/// <summary>Messages</summary>
		public static partial class Msg { }

		/// <summary>
		/// Internal Post Meta type for getting meta values
		/// </summary>
		internal sealed record PostMeta : WpPostMetaEntity;

		/// <summary>
		/// Internal Term type for linking terms with posts
		/// </summary>
		internal sealed record Term : TermList.Term
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
}
