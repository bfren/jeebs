// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Bcg
{
	/// <summary>
	/// Sermon script PDF
	/// </summary>
	public sealed record PdfCustomField : FileCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public PdfCustomField() : base("pdf") { }
	}
}
