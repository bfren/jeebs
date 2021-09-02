// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.Entities;

/// <summary>
/// TermTaxonomy entity
/// </summary>
public abstract record class WpTermTaxonomyEntity : WpTermTaxonomyEntityWithId
{
	/// <summary>
	/// TermId
	/// </summary>
	public WpTermId TermId { get; init; } = new();

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
	public WpTermId ParentId { get; init; } = new();

	/// <summary>
	/// Count
	/// </summary>
	public ulong Count { get; init; }
}
