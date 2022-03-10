// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// TermTaxonomy entity
/// </summary>
public abstract record class WpTermTaxonomyEntityWithId : Id.IWithId<WpTermTaxonomyId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpTermTaxonomyId Id { get; init; } = new();
}
