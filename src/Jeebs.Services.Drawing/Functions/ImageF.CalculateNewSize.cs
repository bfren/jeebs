// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing.Geometry;

namespace Jeebs.Services.Drawing.Functions;

/// <summary>
/// Image functions.
/// </summary>
public static partial class ImageF
{
	/// <summary>
	/// Calculate new size of an image based on the image ratio.
	/// </summary>
	/// <param name="imgWidth">Image width.</param>
	/// <param name="imgHeight">Image height.</param>
	/// <param name="newWidth">New image width.</param>
	/// <param name="newHeight">New image height.</param>
	public static Size CalculateNewSize(int imgWidth, int imgHeight, int newWidth, int newHeight) =>
		CalculateNewSize(
			new Size(imgWidth, imgHeight),
			new Size(newWidth, newHeight)
		);

	/// <summary>
	/// Calculate new size of an image based on the image ratio.
	/// </summary>
	/// <param name="currentSize">Current size.</param>
	/// <param name="newSize">New size.</param>
	public static Size CalculateNewSize(Size currentSize, Size newSize)
	{
		if (newSize.Width == 0 && newSize.Height > 0)
		{
			return newSize with { Width = (int)(newSize.Height * currentSize.Ratio) };
		}
		else if (newSize.Width > 0 && newSize.Height == 0)
		{
			return newSize with { Height = (int)(newSize.Width / currentSize.Ratio) };
		}

		return newSize;
	}
}
