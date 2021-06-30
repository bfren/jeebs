// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermTaxonomy entity
	/// </summary>
	public abstract record WpTermTaxonomyEntity : WpTermTaxonomyEntityWithId
	{
		/// <summary>
		/// TermId
		/// </summary>
		public ulong TermId { get; init; }

		/// <summary>
		/// Taxonomy
		/// </summary>
		public Taxonomy Taxonomy { get; init; } = Taxonomy.Blank;

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; init; } = string.Empty;

		/// <summary>
		/// ParentId
		/// </summary>
		public ulong ParentId { get; init; }

		/// <summary>
		/// Count
		/// </summary>
		public ulong Count { get; init; }
	}
}
