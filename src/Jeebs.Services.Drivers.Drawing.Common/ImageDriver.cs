// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.IO;
using Jeebs.Services.Drawing;
using static F.OptionF;

namespace Jeebs.Services.Drivers.Drawing.Common
{
	/// <summary>
	/// Image Driver implemented using System.Drawing.Common
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
			var image = System.Drawing.Image.FromFile(path);
			return new ImageWrapper(image);
		}

		/// <inheritdoc/>
		public IImageWrapper FromStream(Stream stream)
		{
			// Create and return image object
			var image = System.Drawing.Image.FromStream(stream);
			return new ImageWrapper(image);
		}

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>The image file was not found</summary>
			public sealed record ImageFileNotFoundMsg(string Path) : INotFoundMsg { }
		}
	}
}
