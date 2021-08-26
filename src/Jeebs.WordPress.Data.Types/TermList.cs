// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Term List - allows taxonomies to be added to posts
	/// </summary>
	public sealed class TermList : List<TermList.Term>
	{
		/// <summary>
		/// Taxonomy of this Term List
		/// </summary>
		public Taxonomy Taxonomy { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="taxonomy">Taxonomy of these terms</param>
		public TermList(Taxonomy taxonomy) =>
			Taxonomy = taxonomy;

		/// <summary>
		/// Term Model
		/// </summary>
		public abstract record class Term : WpTermEntityWithId
		{
			/// <summary>
			/// Title
			/// </summary>
			public string Title { get; init; } = string.Empty;

			/// <summary>
			/// Slug
			/// </summary>
			public string Slug { get; init; } = string.Empty;

			/// <summary>
			/// Count
			/// </summary>
			public long Count { get; init; }
		}
	}
}
