// Copyright (c) bfren.uk

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Usa
{
	/// <summary>
	/// URL of attached file
	/// </summary>
	public sealed class AttachedFileUrl : TextCustomField
	{
		/// <summary>
		/// This field is not required
		/// </summary>
		public AttachedFileUrl() : base(Constants.Attachment) { }
	}
}
