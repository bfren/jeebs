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
public sealed class ImageWrapper : Services.Drawing.ImageWrapper, IDisposable
{
	/// <inheritdoc/>
	public override int Width =>
		SKImage.FromEncodedData(image).Width;

	/// <inheritdoc/>
	public override int Height =>
		SKImage.FromEncodedData(image).Height;

	private readonly SKData image;

	internal ImageWrapper(SKData image) =>
		this.image = image;

	/// <inheritdoc/>
	public override void Save(string path, ImageFormat format)
	{
		var encodedFormat = format.GetEncodedImageFormat();
		using var data = SKImage.FromEncodedData(image).Encode(encodedFormat, 80);
		using var fs = File.OpenWrite(path);
		data.SaveTo(fs);
	}

	/// <inheritdoc/>
	public override byte[] ToJpegByteArray(int quality) =>
		ToByteArray(SKEncodedImageFormat.Jpeg, quality);

	/// <inheritdoc/>
	public override byte[] ToPngByteArray(int quality) =>
		ToByteArray(SKEncodedImageFormat.Png, quality);

	/// <summary>
	/// Output image as byte array.
	/// </summary>
	/// <param name="format">SKEncodedImageFormat.</param>
	/// <param name="quality">Image quality (0 - 100).</param>
	internal byte[] ToByteArray(SKEncodedImageFormat format, int quality)
	{
		using var data = SKImage.FromEncodedData(image).Encode(format, quality);
		using var ms = new MemoryStream();
		data.SaveTo(ms);
		return ms.ToArray();
	}

	/// <inheritdoc/>
	public override Maybe<IImageWrapper> ApplyMask(int width, int height) =>
		ApplyMask(width, height, (size, mask) =>
		{
			// Create source and destination rectangles
			var source = new SKRectI(mask.X, mask.Y, mask.X + mask.Width, mask.Y + mask.Height);
			var destination = new SKRectI(0, 0, size.Width, size.Height);

			// Create surface on which to draw the masked and resized image
			using var surface = SKSurface.Create(info: new(size.Width, size.Height));
			using var paint = new SKPaint() { IsAntialias = width > 200 || height > 200 };

			// Draw the actual image
			surface.Canvas.DrawImage(SKImage.FromEncodedData(image), source, destination, paint);

			// Return resized image
			using var resized = surface.Snapshot();

			return new ImageWrapper(resized.Encode());
		});

	/// <inheritdoc/>
	public void Dispose() =>
		image.Dispose();
}
