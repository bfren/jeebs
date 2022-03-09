// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.WordPress.Tables;

/// <summary>
/// Term Relationship Table
/// </summary>
public sealed record class TermRelationshipTable : Table
{
	/// <summary>
	/// PostId
	/// </summary>
	public string PostId =>
		"object_id";

	/// <summary>
	/// TermTaxonomyId
	/// </summary>
	public string TermTaxonomyId =>
		"term_taxonomy_id";

	/// <summary>
	/// SortOrder
	/// </summary>
	public string SortOrder =>
		"term_order";

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public TermRelationshipTable(string prefix) : base($"{prefix}term_relationships") { }
}
