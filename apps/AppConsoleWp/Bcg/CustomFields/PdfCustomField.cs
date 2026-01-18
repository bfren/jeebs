// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.CustomFields;

namespace AppConsoleWp.Bcg;

/// <summary>
/// Sermon script PDF.
/// </summary>
public sealed class PdfCustomField : FileCustomField
{
	/// <summary>
	/// This is not a required field
	/// </summary>
	public PdfCustomField() : base("pdf") { }
}
