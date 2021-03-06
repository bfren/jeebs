using Jeebs.WordPress;

namespace AppConsoleWordPress.Bcg
{
	/// <summary>
	/// The place a sermon was first preached
	/// </summary>
	public sealed class FeedImageCustomField : AttachmentCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public FeedImageCustomField() : base("image") { }
	}
}
