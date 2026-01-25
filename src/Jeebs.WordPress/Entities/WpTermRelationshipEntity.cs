// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Attributes;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// TermRelationship entity.
/// </summary>
public abstract record class WpTermRelationshipEntity : IWithId
{
	/// <summary>
	/// Required to enable mapping - but the WP database does not have a unique key for the Term Relationship table.
	/// </summary>
	/// <exception cref="NotSupportedException"></exception>
	[Ignore]
	public IUnion Id
	{
		get => throw NotSupported;
		init => throw NotSupported;
	}

	private static NotSupportedException NotSupported =>
		new("Term Relationships do not have unique IDs in the WordPress database.");

	/// <summary>
	/// PostId.
	/// </summary>
	public Ids.WpPostId PostId { get; init; } = new();

	/// <summary>
	/// TermTaxonomyId.
	/// </summary>
	public Ids.WpTermTaxonomyId TermTaxonomyId { get; init; } = new();

	/// <summary>
	/// SortOrder.
	/// </summary>
	public ulong SortOrder { get; init; }
}
