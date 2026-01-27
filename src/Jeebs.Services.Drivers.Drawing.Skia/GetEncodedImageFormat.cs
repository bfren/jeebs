// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing;
using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia;

/// <summary>
/// <see cref="ImageFormat"/> extensions.
/// </summary>
public static class ImageFormatExtensions
{
	/// <summary>
	/// Return the <see cref="SKEncodedImageFormat"/> equivalent of an <see cref="ImageFormat"/>.
	/// </summary>
	/// <param name="this">ImageFormat.</param>
	/// <exception cref="UnsupportedImageFormatException"></exception>
	public static SKEncodedImageFormat GetEncodedImageFormat(this ImageFormat @this) =>
		@this switch
		{
			ImageFormat.Bmp =>
				SKEncodedImageFormat.Bmp,

			ImageFormat.Gif =>
				SKEncodedImageFormat.Gif,

			ImageFormat.Ico =>

				SKEncodedImageFormat.Ico,

			ImageFormat.Jpeg =>

				SKEncodedImageFormat.Jpeg,

			ImageFormat.Png =>
				SKEncodedImageFormat.Png,

			_ =>
				throw new UnsupportedImageFormatException($"{@this} is not a supported image format.")
		};
}
