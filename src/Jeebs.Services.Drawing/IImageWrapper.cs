// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drawing;

/// <summary>
/// Image wrapper.
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
	/// Save image to the hard drive as a JPEG
	/// </summary>
	/// <param name="path">Absolute path</param>
	void Save(string path);

	/// <summary>
	/// Save image to the hard drive
	/// </summary>
	/// <param name="path">Absolute path</param>
	/// <param name="format">Image format</param>
	void Save(string path, ImageFormat format);

	/// <summary>
	/// Return image as a JPEG byte array with quality 80
	/// </summary>
	byte[] ToJpegByteArray();

	/// <summary>
	/// Return image as a JPEG byte array
	/// </summary>
	/// <param name="quality">Image quality (0 - 100)</param>
	byte[] ToJpegByteArray(int quality);

	/// <summary>
	/// Return image as a PNG byte array with quality 80
	/// </summary>
	byte[] ToPngByteArray();

	/// <summary>
	/// Return image as a PNG byte array
	/// </summary>
	/// <param name="quality">[Optional] Image quality (0 - 100)</param>
	byte[] ToPngByteArray(int quality);

	/// <summary>
	/// Resize and crop an image to fill a mask of specified width and height
	/// </summary>
	/// <param name="width">Mask width</param>
	/// <param name="height">Mask height</param>
	Maybe<IImageWrapper> ApplyMask(int width, int height);
}
