// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.IO;
using Jeebs.Services.Drawing;
using Jm.Services.Drawing.ImageWrapper;
using static JeebsF.OptionF;

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
				return None<IImageWrapper>(new ImageFileNotFoundMsg(path));
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
	}
}
