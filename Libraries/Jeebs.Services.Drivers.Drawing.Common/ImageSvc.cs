using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Services.Drawing;
using Jm.Services.Drawing.ImageWrapper;

namespace Jeebs.Services.Drivers.Drawing.Common
{
	/// <summary>
	/// Image Service - using System.Drawing.Common
	/// </summary>
	public sealed class ImageSvc : IImageSvc
	{
		/// <inheritdoc/>
		public Option<IImageWrapper> FromFile(string path)
		{
			if (!File.Exists(path))
			{
				return Option.None<IImageWrapper>().AddReason(new ImageFileNotFoundMsg(path));
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
