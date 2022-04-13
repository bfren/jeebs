// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.WordPress.Tables;

/// <summary>
/// Term Relationship Table
/// </summary>
public sealed record class TermRelationshipsTable : Table
{
	/// <summary>
	/// PostId
	/// </summary>
	[Id]
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
	public TermRelationshipsTable(string prefix) : base($"{prefix}term_relationships") { }
}
