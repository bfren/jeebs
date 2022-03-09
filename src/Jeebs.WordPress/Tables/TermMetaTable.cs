// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.WordPress.Tables;

/// <summary>
/// Term Meta Table
/// </summary>
public sealed record class TermMetaTable : Table
{
	/// <summary>
	/// TermMetaId
	/// </summary>
	public string Id =>
		"meta_id";

	/// <summary>
	/// TermId
	/// </summary>
	public string TermId =>
		"term_id";

	/// <summary>
	/// Key
	/// </summary>
	public string Key =>
		"meta_key";

	/// <summary>
	/// Value
	/// </summary>
	public string Value =>
		"meta_value";

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public TermMetaTable(string prefix) : base($"{prefix}termmeta") { }
}
