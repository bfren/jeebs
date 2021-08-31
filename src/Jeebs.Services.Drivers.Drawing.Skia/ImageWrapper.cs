// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Services.Drawing;
using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia
{
	/// <summary>
	/// Image Wrapper - using SkiaSharp
	/// </summary>
	public sealed class ImageWrapper : Services.Drawing.ImageWrapper, IDisposable
	{
		/// <inheritdoc/>
		public override int Width =>
			image.Width;

		/// <inheritdoc/>
		public override int Height =>
			image.Height;

		private readonly SKImage image;

		internal ImageWrapper(SKImage image) =>
			this.image = image;

		/// <inheritdoc/>
		public override void Save(string path, ImageFormat format = ImageFormat.Jpeg)
		{
			var encodedFormat = format.GetEncodedImageFormat();
			using var data = image.Encode(encodedFormat, 80);
			using var fs = File.OpenWrite(path);
			data.SaveTo(fs);
		}

		/// <inheritdoc/>
		public override byte[] ToJpegByteArray()
		{
			using var data = image.Encode(SKEncodedImageFormat.Jpeg, 80);
			using var ms = new MemoryStream();
			data.SaveTo(ms);
			return ms.ToArray();
		}

		/// <inheritdoc/>
		public override Option<IImageWrapper> ApplyMask(int width, int height) =>
			ApplyMask(width, height, (size, mask) =>
			{
				// Create source and destination rectangles
				var source = new SKRectI(mask.X, mask.Y, mask.X + mask.Width, mask.Y + mask.Height);
				var destination = new SKRectI(0, 0, size.Width, size.Height);

				// Create surface on which to draw the masked and resized image
				using var surface = SKSurface.Create(info: new(size.Width, size.Height));
				using var paint = new SKPaint();

				// For small images use faster image processing
				if (width <= 200 && height <= 200)
				{
					paint.IsAntialias = false;
					paint.FilterQuality = SKFilterQuality.Low;
				}
				else
				{
					paint.IsAntialias = true;
					paint.FilterQuality = SKFilterQuality.High;
				}

				// Draw the actual image
				surface.Canvas.DrawImage(image, source, destination, paint);

				// Return resized image
				using var resized = surface.Snapshot();
				return new ImageWrapper(resized);
			});

		/// <inheritdoc/>
		public void Dispose() =>
			image.Dispose();
	}
}
