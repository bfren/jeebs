// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.IO;
using Jeebs.Services.Drawing;
using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia;

/// <summary>
/// Image Driver implemented using SkiaSharp.
/// </summary>
public sealed partial class ImageDriver : IImageDriver
{
	/// <inheritdoc/>
	public Result<IImageWrapper> FromFile(string path)
	{
		if (!File.Exists(path))
		{
			return ImageFileNotFound(path);
		}

		// Create and return image object
		using var image = SKImage.FromEncodedData(path);
		return WrapImageIfNotNull(image, nameof(FromFile));
	}

	/// <inheritdoc/>
	public Result<IImageWrapper> FromStream(Stream stream)
	{
		// Create and return image object
		using var image = SKImage.FromEncodedData(stream);
		return WrapImageIfNotNull(image, nameof(FromStream));
	}

	/// <summary>
	/// Wrap a decoded image - Skia returns null when the data is not an image it can read.
	/// </summary>
	/// <param name="image">Decoded image, or null.</param>
	/// <param name="method">Calling method name, for the failure context.</param>
	private static Result<IImageWrapper> WrapImageIfNotNull(SKImage? image, string method) =>
		image switch
		{
			SKImage x =>
				new ImageWrapper(x.EncodedData),

			_ =>
				NotValidImage()
					.WithContext(nameof(ImageDriver), method)
		};

	[Failure("Image file does not exist: '{Path}'.")]
	private static partial Failure ImageFileNotFound(string path);

	[Failure("Not a valid image.")]
	private static partial Failure NotValidImage();
}
