// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.WordPress.Tables;

/// <summary>
/// Term Table
/// </summary>
public sealed record class TermsTable : Table
{
	/// <summary>
	/// TermId
	/// </summary>
	[Id]
	public string Id =>
		"term_id";

	/// <summary>
	/// Title
	/// </summary>
	public string Title =>
		"name";

	/// <summary>
	/// Slug
	/// </summary>
	public string Slug =>
		"slug";

	/// <summary>
	/// Group
	/// </summary>
	public string Group =>
		"term_group";

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public TermsTable(string prefix) : base($"{prefix}terms") { }
}
