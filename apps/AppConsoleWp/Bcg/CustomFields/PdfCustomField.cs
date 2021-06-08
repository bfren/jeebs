// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Bcg
{
	/// <summary>
	/// Sermon script PDF
	/// </summary>
	public sealed class PdfCustomField : FileCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public PdfCustomField() : base("pdf") { }
	}
}
