// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.WordPress.Data.Tables;

/// <summary>
/// Option Table
/// </summary>
public sealed record class OptionTable : Table
{
	/// <summary>
	/// OptionId
	/// </summary>
	public string Id =>
		"option_id";

	/// <summary>
	/// Key
	/// </summary>
	public string Key =>
		"option_name";

	/// <summary>
	/// Value
	/// </summary>
	public string Value =>
		"option_value";

	/// <summary>
	/// IsAutoloaded
	/// </summary>
	public string IsAutoloaded =>
		"autoload";

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public OptionTable(string prefix) : base($"{prefix}options") { }
}
