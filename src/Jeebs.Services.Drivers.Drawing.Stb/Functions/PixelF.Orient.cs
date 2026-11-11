// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Runtime.InteropServices;

namespace Jeebs.Services.Drivers.Drawing.Stb.Functions;

internal static partial class PixelF
{
	/// <summary>
	/// Turn and / or flip pixels so they show the way the EXIF orientation says.
	/// </summary>
	/// <param name="pixels">Pixels as stored.</param>
	/// <param name="width">Stored width.</param>
	/// <param name="height">Stored height.</param>
	/// <param name="orientation">EXIF orientation: 1 as stored, 2 - 8 flipped and / or turned.</param>
	/// <returns>Pixels and their width and height (swapped for 5 - 8, which turn by 90 degrees).</returns>
	internal static (byte[] Pixels, int Width, int Height) Orient(byte[] pixels, int width, int height, int orientation)
	{
		if (orientation is < 2 or > 8)
		{
			return (pixels, width, height);
		}

		var (w, h) = (width, height);
		var (outWidth, outHeight) = orientation >= 5 ? (h, w) : (w, h);
		var output = new byte[pixels.Length];

		// Copy whole pixels (four bytes) at a time
		var source = MemoryMarshal.Cast<byte, uint>(pixels);
		var target = MemoryMarshal.Cast<byte, uint>(output.AsSpan());

		for (var y = 0; y < outHeight; y++)
		{
			for (var x = 0; x < outWidth; x++)
			{
				// Where this output pixel comes from in the stored image
				var (sx, sy) = orientation switch
				{
					2 => (w - 1 - x, y),         // flipped left to right
					3 => (w - 1 - x, h - 1 - y), // turned 180
					4 => (x, h - 1 - y),         // flipped top to bottom
					5 => (y, x),                 // flipped along the top-left diagonal
					6 => (y, h - 1 - x),         // turned 90 clockwise
					7 => (w - 1 - y, h - 1 - x), // flipped along the top-right diagonal
					_ => (w - 1 - y, x)          // 8: turned 90 anticlockwise
				};

				target[(y * outWidth) + x] = source[(sy * w) + sx];
			}
		}

		return (output, outWidth, outHeight);
	}
}
