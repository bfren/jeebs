using Jeebs.WordPress;

namespace AppConsoleWordPress.Bcg
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
