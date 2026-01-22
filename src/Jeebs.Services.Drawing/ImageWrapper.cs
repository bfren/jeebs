// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Services.Drawing.Functions;
using Jeebs.Services.Drawing.Geometry;

namespace Jeebs.Services.Drawing;

/// <inheritdoc cref="IImageWrapper"/>
public abstract class ImageWrapper : IImageWrapper
{
	/// <inheritdoc/>
	public abstract int Width { get; }

	/// <inheritdoc/>
	public abstract int Height { get; }

	/// <inheritdoc/>
	public virtual void Save(string path) =>
		Save(path, ImageFormat.Jpeg);

	/// <inheritdoc/>
	public abstract void Save(string path, ImageFormat format);

	/// <inheritdoc/>
	public virtual Result<byte[]> ToJpegByteArray() =>
		ToJpegByteArray(80);

	/// <inheritdoc/>
	public abstract Result<byte[]> ToJpegByteArray(int quality);

	/// <inheritdoc/>
	public virtual Result<byte[]> ToPngByteArray() =>
		ToPngByteArray(80);

	/// <inheritdoc/>
	public abstract Result<byte[]> ToPngByteArray(int quality);

	/// <inheritdoc/>
	public abstract Result<IImageWrapper> ApplyMask(int width, int height);

	/// <summary>
	/// Resize and crop an image to fill a mask of specified width and height.
	/// </summary>
	/// <param name="width">Mask width.</param>
	/// <param name="height">Mask height.</param>
	/// <param name="apply">Function to perform the graphics manipulation.</param>
	protected Result<IImageWrapper> ApplyMask(int width, int height, Func<Size, Rectangle, IImageWrapper> apply)
	{
		// At least one of width and height should be greater than zero
		if (width == 0 && height == 0)
		{
			return R.Fail(nameof(ImageWrapper), nameof(ApplyMask),
				"Mask width or height is required,"
			);
		}

		// Calculate the size of the new image
		var size = ImageF.CalculateNewSize(Width, Height, width, height);

		// Calculate the mask to apply to the original image
		var mask = ImageF.CalculateMask(Width, Height, size.Width, size.Height);

		// Use implementation to return masked and resized image
		return R.Try(
			() => apply(size, mask),
			e => R.Fail(nameof(ImageWrapper), nameof(ApplyMask),
				e, "Error applying image mask."
			)
		);
	}
}
