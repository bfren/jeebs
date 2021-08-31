// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Services.Drawing;
using Jeebs.Services.Drivers.Drawing.Skia.Exceptions;
using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia
{
	public static class ImageFormat_Extensions
	{
		/// <summary>
		/// Return the <see cref="SKEncodedImageFormat"/> equivalent of an <see cref="ImageFormat"/>
		/// </summary>
		/// <param name="this">ImageFormat</param>
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
}
