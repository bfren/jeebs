// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IO;
using Jeebs.Services.Drawing;
using Jeebs.Services.Drivers.Drawing.Stb.Functions;
using StbImageWriteSharp;

namespace Jeebs.Services.Drivers.Drawing.Stb;

/// <summary>
/// Image Wrapper - using StbImageSharp.
/// </summary>
public sealed class ImageWrapper : Services.Drawing.ImageWrapper
{
	/// <inheritdoc/>
	public override int Width { get; }

	/// <inheritdoc/>
	public override int Height { get; }

	/// <summary>
	/// Decoded pixels: four bytes (red, green, blue, alpha) per pixel, row by row from the top.
	/// </summary>
	internal byte[] Pixels { get; }

	internal ImageWrapper(byte[] pixels, int width, int height) =>
		(Pixels, Width, Height) = (pixels, width, height);

	/// <inheritdoc/>
	public override void Save(string path, ImageFormat format)
	{
		// Encode first, so an encoding error doesn't leave an empty file behind
		using var ms = new MemoryStream();
		Write(ms, format, 80);
		File.WriteAllBytes(path, ms.ToArray());
	}

	/// <inheritdoc/>
	public override Result<byte[]> ToJpegByteArray(int quality) =>
		ToByteArray(ImageFormat.Jpeg, quality);

	/// <inheritdoc/>
	public override Result<byte[]> ToPngByteArray(int quality) =>
		ToByteArray(ImageFormat.Png, quality);

	/// <summary>
	/// Output image as byte array.
	/// </summary>
	/// <param name="format">ImageFormat.</param>
	/// <param name="quality">Image quality (0 - 100) - JPEG only.</param>
	internal Result<byte[]> ToByteArray(ImageFormat format, int quality) =>
		R.Try(
			() =>
			{
				using var ms = new MemoryStream();
				Write(ms, format, quality);
				return ms.ToArray();
			},
			e => R.Fail(e).Msg("Error converting image to byte array.")
				.Ctx(nameof(ImageWrapper), nameof(ToByteArray))
		);

	private void Write(Stream stream, ImageFormat format, int quality)
	{
		var writer = new ImageWriter();
		switch (format)
		{
			case ImageFormat.Jpeg:
				writer.WriteJpg(Pixels, Width, Height, ColorComponents.RedGreenBlueAlpha, stream, quality);
				break;

			case ImageFormat.Png:
				writer.WritePng(Pixels, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(format), format, "Unsupported image format.");
		}
	}

	/// <inheritdoc/>
	public override Result<IImageWrapper> ApplyMask(int width, int height) =>
		ApplyMask(width, height, (size, mask) =>
			new ImageWrapper(PixelF.Resize(Pixels, Width, Height, mask, size.Width, size.Height), size.Width, size.Height)
		);

	/// <summary>
	/// The pixels are managed memory, so there is nothing to free.
	/// </summary>
	public override void Dispose() { }
}
