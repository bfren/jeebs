// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Services.Drawing.Geometry;
using JeebsF;
using Jm.Services.Drawing.ImageWrapper;
using static JeebsF.OptionF;

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
				return None<IImageWrapper>(new MaskHeightOrWidthRequiredMsg());
			}

			// Calculate the size of the new image
			var size = ImageF.CalculateNewSize(Width, Height, width, height);

			// Calculate the mask to apply to the original image
			var mask = ImageF.CalculateMask(Width, Height, size.Width, size.Height);

			// Use implementation to return masked and resized image
			try
			{
				var resized = apply(size, mask);
				return Return(resized);
			}
			catch (Exception e)
			{
				return None<IImageWrapper>(new ExceptionApplyingImageMaskMsg(e));
			}
		}
	}
}
