// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermRelationship entity
	/// </summary>
	public abstract record WpTermRelationshipEntity : IWithId
	{
		/// <summary>
		/// Required to enable mapping - but the WP database does not have a unique key for the Term Relationship table
		/// </summary>
		[Ignore]
		public StrongId Id =>
			throw new System.NotSupportedException("Term Relationships do not have unique IDs in the WordPress database.");

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public ulong PostId { get; init; }

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public ulong TermTaxonomyId { get; init; }

		/// <summary>
		/// SortOrder
		/// </summary>
		public ulong SortOrder { get; init; }
	}
}
