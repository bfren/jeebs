// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Jeebs.Services.Drawing;
using JeebsF;

namespace Jeebs.Services.Drivers.Drawing.Common
{
	/// <summary>
	/// Image Wrapper - using System.Drawing.Common
	/// </summary>
	public sealed class ImageWrapper : Services.Drawing.ImageWrapper
	{
		/// <inheritdoc/>
		public override int Width =>
			image.Width;

		/// <inheritdoc/>
		public override int Height =>
			image.Height;

		private readonly Image image;

		internal ImageWrapper(Image image) =>
			this.image = image;

		/// <inheritdoc/>
		public override void Save(string path) =>
			image.Save(path);

		/// <inheritdoc/>
		public override byte[] ToJpegByteArray()
		{
			using var ms = new MemoryStream();
			image.Save(ms, ImageFormat.Jpeg);
			return ms.ToArray();
		}

		/// <inheritdoc/>
		public override Option<IImageWrapper> ApplyMask(int width, int height) =>
			ApplyMask(width, height, (size, mask) =>
			{
				// Create source and destination rectangles
				var source = new Rectangle(mask.X, mask.Y, mask.Width, mask.Height);
				var destination = new Rectangle(0, 0, size.Width, size.Height); // use calulcated sizes rather than originals

				// Create bitmap the correct size
				var resized = new Bitmap(size.Width, size.Height);

				// Resize image using the rectangles
				using (var g = Graphics.FromImage(resized))
				{
					if (width <= 200 && height <= 200)
					{
						g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
						g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
						g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
					}
					else
					{
						g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
						g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
						g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
					}

					g.DrawImage(image, destination, source, GraphicsUnit.Pixel);
				}

				// Return resized image
				return new ImageWrapper(resized);
			});
	}
}
