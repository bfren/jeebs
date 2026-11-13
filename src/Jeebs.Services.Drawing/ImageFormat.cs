// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drawing;

/// <summary>
/// Supported image formats.
/// </summary>
public enum ImageFormat
{
	/// <summary>
	/// Default value - not a valid format.
	/// </summary>
	Unsupported = 0,

	/// <summary>
	/// Joint Photographic Experts Group format.
	/// </summary>
	Jpeg = 1,

	/// <summary>
	/// Portable Network Graphics format.
	/// </summary>
	Png = 2
}
