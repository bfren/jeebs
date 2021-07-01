// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Usa
{
	/// <summary>
	/// Info about an attached file
	/// </summary>
	public sealed class AttachedFileInfo : TextCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public AttachedFileInfo() : base(Constants.AttachmentMetadata) { }
	}
}
