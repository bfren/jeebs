// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities.StrongIds;

/// <summary>
/// WordPress Term Taxonomy ID
/// </summary>
/// <param name="Value">ID Value</param>
public sealed record class WpTermTaxonomyId(long Value) : Id.IStrongId
{
	/// <summary>
	/// Define parameterless constructor for MVC model binding
	/// </summary>
	public WpTermTaxonomyId() : this(0) { }
}