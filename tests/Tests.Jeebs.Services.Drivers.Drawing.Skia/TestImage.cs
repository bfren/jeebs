// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia;

internal static class TestImage
{
	/// <summary>
	/// PNG of the given size, in alternating one-pixel black and white columns.
	/// </summary>
	public static byte[] StripedPng(int width, int height)
	{
		using var bitmap = new SKBitmap(width, height);
		for (var x = 0; x < width; x++)
		{
			var colour = x % 2 == 0 ? SKColors.Black : SKColors.White;
			for (var y = 0; y < height; y++)
			{
				bitmap.SetPixel(x, y, colour);
			}
		}

		using var data = bitmap.Encode(SKEncodedImageFormat.Png, 100);
		return data.ToArray();
	}
}
