// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Usa;

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
