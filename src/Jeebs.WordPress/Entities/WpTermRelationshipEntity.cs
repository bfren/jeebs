// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.Id;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// TermRelationship entity
/// </summary>
public abstract record class WpTermRelationshipEntity : Id.IWithId
{
	/// <summary>
	/// Required to enable mapping - but the WP database does not have a unique key for the Term Relationship table
	/// </summary>
	/// <exception cref="System.NotSupportedException"></exception>
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
