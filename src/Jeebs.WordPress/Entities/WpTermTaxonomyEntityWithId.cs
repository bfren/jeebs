// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.WordPress.Entities.StrongIds;
using StrongId;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// TermTaxonomy entity.
/// </summary>
public abstract record class WpTermTaxonomyEntityWithId : IWithId<WpTermTaxonomyId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpTermTaxonomyId Id { get; init; } = new();
}
