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
			return R.Fail("Image file does not exist: '{Path}'.", path)
				.Ctx(nameof(ImageDriver), nameof(FromFile));
		}

		// Create and return image object
		using var image = SKImage.FromEncodedData(path);
		return Wrap(image, nameof(FromFile));
	}

	/// <inheritdoc/>
	public Result<IImageWrapper> FromStream(Stream stream)
	{
		// Create and return image object
		using var image = SKImage.FromEncodedData(stream);
		return Wrap(image, nameof(FromStream));
	}

	/// <summary>
	/// Wrap a decoded image - Skia returns null when the data is not an image it can read.
	/// </summary>
	/// <param name="image">Decoded image, or null.</param>
	/// <param name="method">Calling method name, for the failure context.</param>
	private static Result<IImageWrapper> Wrap(SKImage? image, string method) =>
		image switch
		{
			SKImage x =>
				new ImageWrapper(x.EncodedData),

			_ =>
				R.Fail("Not a valid image.")
					.Ctx(nameof(ImageDriver), method)
		};
}
