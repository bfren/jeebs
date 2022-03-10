// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.IO;
using Jeebs.Messages;
using Jeebs.Services.Drawing;
using MaybeF;
using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia;

/// <summary>
/// Image Driver implemented using SkiaSharp
/// </summary>
public sealed class ImageDriver : IImageDriver
{
	/// <inheritdoc/>
	public Maybe<IImageWrapper> FromFile(string path)
	{
		if (!File.Exists(path))
		{
			return F.None<IImageWrapper>(new M.ImageFileNotFoundMsg(path));
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
	public static class M
	{
		/// <summary>The image file was not found</summary>
		/// <param name="Value">File Path</param>
		public sealed record class ImageFileNotFoundMsg(string Value) : NotFoundMsg<string>
		{
			/// <inheritdoc/>
			public override string Name =>
				"File Path";
		}
	}
}
