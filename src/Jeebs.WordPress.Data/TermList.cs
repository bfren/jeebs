// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
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
		public abstract record Term : IWithId<WpTermId>
		{
			/// <summary>
			/// TermId
			/// </summary>
			public WpTermId Id { get; init; } = new();

			/// <summary>
			/// TermId
			/// </summary>
			public long TermId { get; init; }

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

			/// <summary>
			/// Display title
			/// </summary>
			public override string ToString() =>
				Title;
		}
	}
}
