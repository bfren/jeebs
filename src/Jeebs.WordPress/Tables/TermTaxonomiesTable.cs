// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.WordPress.Tables;

/// <summary>
/// Term Taxonomy Table.
/// </summary>
public sealed record class TermTaxonomiesTable : Table
{
	/// <summary>
	/// TermTaxonomyId.
	/// </summary>
	[Id]
	public string Id =>
		"term_taxonomy_id";

	/// <summary>
	/// TermId.
	/// </summary>
	public string TermId =>
		"term_id";

	/// <summary>
	/// Taxonomy.
	/// </summary>
	public string Taxonomy =>
		"taxonomy";

	/// <summary>
	/// Description.
	/// </summary>
	public string Description =>
		"description";

	/// <summary>
	/// ParentId.
	/// </summary>
	public string ParentId =>
		"parent";

	/// <summary>
	/// Count.
	/// </summary>
	public string Count =>
		"count";

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="prefix">Table prefix.</param>
	public TermTaxonomiesTable(string prefix) : base($"{prefix}term_taxonomy") { }
}
