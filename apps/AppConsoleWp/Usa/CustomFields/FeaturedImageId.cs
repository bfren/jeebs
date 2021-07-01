// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Usa
{
	/// <summary>
	/// Featured image ID
	/// </summary>
	public sealed class FeaturedImageId : TextCustomField
	{
		/// <summary>
		/// This field is required
		/// </summary>
		public FeaturedImageId() : base(Constants.FeaturedImageId) { }
	}
}
