// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress;
using Jeebs.WordPress.CustomFields;

namespace AppConsoleWp.Usa;

/// <summary>
/// Featured image Url.
/// </summary>
public sealed class FeaturedImageUrl : AttachmentCustomField
{
	/// <summary>
	/// This field is required.
	/// </summary>
	public FeaturedImageUrl() : base(Constants.FeaturedImageId) { }
}
