// Copyright (c) bfren.uk

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Usa
{
	/// <summary>
	/// Featured image Url
	/// </summary>
	public sealed class FeaturedImageUrl : AttachmentCustomField
	{
		/// <summary>
		/// This field is required
		/// </summary>
		public FeaturedImageUrl() : base(Constants.FeaturedImageId) { }
	}
}
