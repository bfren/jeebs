// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		/// <param name="format">Image format</param>
		void Save(string path, ImageFormat format = ImageFormat.Jpeg);

		/// <summary>
		/// Return image as a JPEG byte array
		/// </summary>
		/// <param name="quality">[Optional] Image quality (0 - 100)</param>
		byte[] ToJpegByteArray(int quality = 80);

		/// <summary>
		/// Return image as a PNG byte array
		/// </summary>
		/// <param name="quality">[Optional] Image quality (0 - 100)</param>
		byte[] ToPngByteArray(int quality = 80);

		/// <summary>
		/// Resize and crop an image to fill a mask of specified width and height
		/// </summary>
		/// <param name="width">Mask width</param>
		/// <param name="height">Mask height</param>
		Option<IImageWrapper> ApplyMask(int width, int height);
	}
}
