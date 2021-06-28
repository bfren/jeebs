// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermRelationship entity
	/// </summary>
	public abstract record WpTermRelationshipEntity : WpTermRelationshipEntityWithId
	{
		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public long TermTaxonomyId { get; init; }

		/// <summary>
		/// SortOrder
		/// </summary>
		public long SortOrder { get; init; }
	}
}
