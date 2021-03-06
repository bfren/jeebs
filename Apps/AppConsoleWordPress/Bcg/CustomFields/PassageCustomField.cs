using Jeebs.WordPress;

namespace AppConsoleWordPress.Bcg
{
	/// <summary>
	/// Bible Passage
	/// </summary>
	public sealed class PassageCustomField : TextCustomField
	{
		/// <summary>
		/// This is a required field
		/// </summary>
		public PassageCustomField() : base("passage", true) { }
	}
}
