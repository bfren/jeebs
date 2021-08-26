// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermRelationship entity
	/// </summary>
	public abstract record class WpTermRelationshipEntity : IWithId
	{
		/// <summary>
		/// Required to enable mapping - but the WP database does not have a unique key for the Term Relationship table
		/// </summary>
		[Ignore]
		public IStrongId Id =>
			throw new System.NotSupportedException("Term Relationships do not have unique IDs in the WordPress database.");

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public WpPostId PostId { get; init; } = new();

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public WpTermTaxonomyId TermTaxonomyId { get; init; } = new();

		/// <summary>
		/// SortOrder
		/// </summary>
		public ulong SortOrder { get; init; }
	}
}
