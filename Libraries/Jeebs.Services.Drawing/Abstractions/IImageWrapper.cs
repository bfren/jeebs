// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Services.Drawing
{
	/// <summary>
	/// Image wrapper
	/// </summary>
	public interface IImageWrapper
	{
		/// <summary>
		/// Image width
		/// </summary>
		int Width { get; }

		/// <summary>
		/// Image height
		/// </summary>
		int Height { get; }

		/// <summary>
		/// Save image to the hard drive
		/// </summary>
		/// <param name="path">Absolute path</param>
		void Save(string path);

		/// <summary>
		/// Return image as a JPEG byte array
		/// </summary>
		byte[] ToJpegByteArray();

		/// <summary>
		/// Resize and crop an image to fill a mask of specified width and height
		/// </summary>
		/// <param name="width">Mask width</param>
		/// <param name="height">Mask height</param>
		Option<IImageWrapper> ApplyMask(int width, int height);
	}
}
