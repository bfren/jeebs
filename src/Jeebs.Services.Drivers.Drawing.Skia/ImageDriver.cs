// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing;
using SkiaSharp;
using static F.OptionF;

namespace Jeebs.Services.Drivers.Drawing.Skia
{
	/// <summary>
	/// Image Driver implemented using SkiaSharp
	/// </summary>
	public sealed class ImageDriver : IImageDriver
	{
		/// <inheritdoc/>
		public Option<IImageWrapper> FromFile(string path)
		{
			if (!File.Exists(path))
			{
				return None<IImageWrapper>(new Msg.ImageFileNotFoundMsg(path));
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

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>The image file was not found</summary>
			public sealed record class ImageFileNotFoundMsg(string Path) : INotFoundMsg { }
		}
	}
}
