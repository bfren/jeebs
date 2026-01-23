// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.IO;
using Jeebs.Services.Drawing;
using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia;

/// <summary>
/// Image Driver implemented using SkiaSharp.
/// </summary>
public sealed class ImageDriver : IImageDriver
{
	/// <inheritdoc/>
	public Result<IImageWrapper> FromFile(string path)
	{
		if (!File.Exists(path))
		{
			return R.Fail("Image file does not exist: '{Path}'.", new { path })
				.Ctx(nameof(ImageDriver), nameof(FromFile));
		}

		// Create and return image object
		using var image = SKImage.FromEncodedData(path);
		return new ImageWrapper(image.EncodedData);
	}

	/// <inheritdoc/>
	public IImageWrapper FromStream(Stream stream)
	{
		// Create and return image object
		using var image = SKImage.FromEncodedData(stream);
		return new ImageWrapper(image.EncodedData);
	}
}
