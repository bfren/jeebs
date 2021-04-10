// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermTaxonomy entity
	/// </summary>
	public abstract record WpTermTaxonomyEntity : IWithId<WpTermTaxonomyId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpTermTaxonomyId Id
		{
			get =>
				new(TermTaxonomyId);

			init =>
				TermTaxonomyId = value.Value;
		}

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		[Id]
		public long TermTaxonomyId { get; init; }

		/// <summary>
		/// TermId
		/// </summary>
		public long TermId { get; init; }

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
		public long ParentId { get; init; }

		/// <summary>
		/// Count
		/// </summary>
		public long Count { get; init; }
	}
}
