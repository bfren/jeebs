// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.IO;
using Jeebs.Services.Drawing;
using Jeebs.Services.Drivers.Drawing.Stb.Functions;
using StbImageSharp;

namespace Jeebs.Services.Drivers.Drawing.Stb;

/// <summary>
/// Image Driver implemented in managed code using StbImageSharp - no native libraries.
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

		return R.Try(
			() => File.ReadAllBytes(path),
			e => R.Fail(e).Msg("Unable to read image file.")
				.Ctx(nameof(ImageDriver), nameof(FromFile))
		)
		.Bind(
			x => Load(x, nameof(FromFile))
		);
	}

	/// <inheritdoc/>
	public Result<IImageWrapper> FromStream(Stream stream)
	{
		using var ms = new MemoryStream();
		stream.CopyTo(ms);
		return Load(ms.ToArray(), nameof(FromStream));
	}

	/// <summary>
	/// Decode an image and turn it the way its EXIF orientation says it should be shown.
	/// </summary>
	/// <param name="data">Encoded image.</param>
	/// <param name="method">Calling method name, for the failure context.</param>
	internal static Result<IImageWrapper> Load(byte[] data, string method) =>
		R.Try(
			() => ImageResult.FromMemory(data, ColorComponents.RedGreenBlueAlpha),
			e => R.Fail(e).Msg("Not a valid image.")
				.Ctx(nameof(ImageDriver), method)
		)
		.Map(
			x =>
			{
				var (pixels, width, height) = PixelF.Orient(x.Data, x.Width, x.Height, PixelF.GetOrientation(data));
				return (IImageWrapper)new ImageWrapper(pixels, width, height);
			}
		);
}
