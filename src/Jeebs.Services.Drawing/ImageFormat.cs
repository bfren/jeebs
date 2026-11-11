// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drawing;

/// <summary>
/// Supported image formats.
/// </summary>
/// <remarks>
/// The values are set to match the <see cref="System.Drawing.Imaging.ImageFormat"/> enumeration values,
/// so that they can be used with the <see cref="Drawing.Image.Save(string, Drawing.Imaging.ImageFormat)"/> method.
/// </remarks>
public enum ImageFormat
{
	/// <summary>
	/// Joint Photographic Experts Group format.
	/// </summary>
	Jpeg = 3,

	/// <summary>
	/// Portable Network Graphics format.
	/// </summary>
	Png = 4
}
