// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IO;
using Jeebs.Services.Drawing;
using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia;

/// <summary>
/// Image Wrapper - using SkiaSharp.
/// </summary>
public sealed class ImageWrapper : Services.Drawing.ImageWrapper
{
	/// <inheritdoc/>
	public override int Width =>
		decoded.Value.Width;

	/// <inheritdoc/>
	public override int Height =>
		decoded.Value.Height;

	private readonly SKData image;

	// decoded once, on first use, and shared by every method
	private readonly Lazy<SKImage> decoded;

	internal ImageWrapper(SKData image) =>
		(this.image, decoded) = (image, new(() => SKImage.FromEncodedData(image)));

	/// <inheritdoc/>
	public override void Save(string path, ImageFormat format)
	{
		var encodedFormat = format.GetEncodedImageFormat();
		using var data = decoded.Value.Encode(encodedFormat, 80);
		using var fs = File.Create(path);
		data.SaveTo(fs);
	}

	/// <inheritdoc/>
	public override Result<byte[]> ToJpegByteArray(int quality) =>
		ToByteArray(SKEncodedImageFormat.Jpeg, quality);

	/// <inheritdoc/>
	public override Result<byte[]> ToPngByteArray(int quality) =>
		ToByteArray(SKEncodedImageFormat.Png, quality);

	/// <summary>
	/// Output image as byte array.
	/// </summary>
	/// <param name="format">SKEncodedImageFormat.</param>
	/// <param name="quality">Image quality (0 - 100).</param>
	internal Result<byte[]> ToByteArray(SKEncodedImageFormat format, int quality) =>
		R.Try(
			() =>
			{
				using var data = decoded.Value.Encode(format, quality);
				using var ms = new MemoryStream();
				data.SaveTo(ms);
				return ms.ToArray();
			},
			e => R.Fail(e).Msg("Error converting image to byte array.")
				.Ctx(nameof(ImageWrapper), nameof(ToByteArray))
		);

	/// <inheritdoc/>
	public override Result<IImageWrapper> ApplyMask(int width, int height) =>
		ApplyMask(width, height, (size, mask) =>
		{
			// Create source and destination rectangles
			var source = new SKRectI(mask.X, mask.Y, mask.X + mask.Width, mask.Y + mask.Height);
			var destination = new SKRectI(0, 0, size.Width, size.Height);

			// Create surface on which to draw the masked and resized image
			using var surface = SKSurface.Create(info: new(size.Width, size.Height));

			// Draw the actual image, smoothing it as it is scaled (the default sampling is nearest-neighbour)
			surface.Canvas.DrawImage(decoded.Value, source, destination, new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear));

			// Return resized image
			using var resized = surface.Snapshot();

			return new ImageWrapper(resized.Encode());
		});

	/// <inheritdoc/>
	public override void Dispose()
	{
		if (decoded.IsValueCreated)
		{
			decoded.Value.Dispose();
		}

		image.Dispose();
	}
}
