using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.WordPress;

namespace AppConsoleWordPress.Bcg
{
	/// <summary>
	/// Sermon script PDF
	/// </summary>
	public sealed class PdfCustomField : AttachmentCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public PdfCustomField() : base("pdf") { }
	}
}
