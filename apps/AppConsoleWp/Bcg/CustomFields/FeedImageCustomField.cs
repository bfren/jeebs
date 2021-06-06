// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Bcg
{
	/// <summary>
	/// The place a sermon was first preached
	/// </summary>
	public sealed record FeedImageCustomField : AttachmentCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public FeedImageCustomField() : base("image") { }
	}
}
