// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Bcg;

/// <summary>
/// Bible Passage
/// </summary>
public sealed class PassageCustomField : TextCustomField
{
	/// <summary>
	/// This is a required field
	/// </summary>
	public PassageCustomField() : base("passage") { }
}
