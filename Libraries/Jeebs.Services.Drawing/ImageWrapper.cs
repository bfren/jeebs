using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Services.Drawing.Structs;
using Jm.Services.Drawing.ImageWrapper;

namespace Jeebs.Services.Drawing
{
	/// <inheritdoc cref="IImageWrapper"/>
	public abstract class ImageWrapper : IImageWrapper
	{
		/// <inheritdoc/>
		public abstract int Width { get; }

		/// <inheritdoc/>
		public abstract int Height { get; }

		/// <inheritdoc/>
		public abstract void Save(string path);

		/// <inheritdoc/>
		public abstract byte[] ToJpegByteArray();

		/// <inheritdoc/>
		public abstract Option<IImageWrapper> ApplyMask(int width, int height);

		/// <summary>
		/// Resize and crop an image to fill a mask of specified width and height
		/// </summary>
		/// <param name="width">Mask width</param>
		/// <param name="height">Mask height</param>
		/// <param name="apply">Function to perform the graphics manipulation</param>
		protected Option<IImageWrapper> ApplyMask(int width, int height, Func<Size, Rectangle, IImageWrapper> apply)
		{
			// At least one of width and height should be greater than zero
			if (width == 0 && height == 0)
			{
				return Option.None<IImageWrapper>().AddReason<MaskHeightOrWidthRequiredMsg>();
			}

			// Calculate the size of the new image
			var size = F.ImageF.CalculateNewSize(Width, Height, width, height);

			// Calculate the mask to apply to the original image
			var mask = F.ImageF.CalculateMask(Width, Height, size.Width, size.Height);

			// Use implementation to return masked and resized image
			try
			{
				var resized = apply(size, mask);
				return Option.Wrap(resized);
			}
			catch (Exception e)
			{
				return Option.None<IImageWrapper>().AddReason(new ExceptionApplyingImageMaskMsg(e));
			}
		}
	}
}
