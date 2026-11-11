// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using StbImageWriteSharp;

namespace Jeebs.Services.Drivers.Drawing.Stb;

internal static class TestImage
{
	public static readonly byte[] Red = [255, 0, 0, 255];
	public static readonly byte[] Green = [0, 255, 0, 255];
	public static readonly byte[] Blue = [0, 0, 255, 255];
	public static readonly byte[] White = [255, 255, 255, 255];
	public static readonly byte[] Black = [0, 0, 0, 255];

	/// <summary>
	/// One-pixel black and white columns.
	/// </summary>
	public static byte[] Stripes(int x, int _) =>
		x % 2 == 0 ? Black : White;

	/// <summary>
	/// Top-left red, top-right green, bottom-left blue, bottom-right white.
	/// </summary>
	public static Func<int, int, byte[]> Quadrants(int width, int height) =>
		(x, y) => (x < width / 2, y < height / 2) switch
		{
			(true, true) => Red,
			(false, true) => Green,
			(true, false) => Blue,
			_ => White
		};

	public static byte[] Pixels(int width, int height, Func<int, int, byte[]> colour)
	{
		var pixels = new byte[width * height * 4];
		for (var y = 0; y < height; y++)
		{
			for (var x = 0; x < width; x++)
			{
				colour(x, y).CopyTo(pixels, ((y * width) + x) * 4);
			}
		}

		return pixels;
	}

	public static byte[] Png(int width, int height, Func<int, int, byte[]> colour)
	{
		using var ms = new MemoryStream();
		new ImageWriter().WritePng(Pixels(width, height, colour), width, height, ColorComponents.RedGreenBlueAlpha, ms);
		return ms.ToArray();
	}

	public static byte[] Jpeg(int width, int height, Func<int, int, byte[]> colour)
	{
		using var ms = new MemoryStream();
		new ImageWriter().WriteJpg(Pixels(width, height, colour), width, height, ColorComponents.RedGreenBlueAlpha, ms, 95);
		return ms.ToArray();
	}

	/// <summary>
	/// Insert an EXIF APP1 segment holding only an orientation tag straight after the JPEG start marker.
	/// </summary>
	public static byte[] WithOrientation(byte[] jpeg, int orientation, bool bigEndian = false)
	{
		byte[] tiff = bigEndian
			? [0x4D, 0x4D, 0x00, 0x2A, 0, 0, 0, 8, 0x00, 0x01, 0x01, 0x12, 0x00, 0x03, 0, 0, 0, 1, 0x00, (byte)orientation, 0, 0, 0, 0, 0, 0]
			: [0x49, 0x49, 0x2A, 0x00, 8, 0, 0, 0, 0x01, 0x00, 0x12, 0x01, 0x03, 0x00, 1, 0, 0, 0, (byte)orientation, 0x00, 0, 0, 0, 0, 0, 0];
		byte[] exif = [.. "Exif\0\0"u8, .. tiff];
		var length = exif.Length + 2;
		return [.. jpeg[..2], 0xFF, 0xE1, (byte)(length >> 8), (byte)length, .. exif, .. jpeg[2..]];
	}

	/// <summary>
	/// Name of the colour at a pixel, allowing for JPEG noise: R, G, B, W or K (black).
	/// </summary>
	public static string ColourAt(byte[] pixels, int width, int x, int y)
	{
		var i = ((y * width) + x) * 4;
		return (pixels[i] > 128, pixels[i + 1] > 128, pixels[i + 2] > 128) switch
		{
			(true, false, false) => "R",
			(false, true, false) => "G",
			(false, false, true) => "B",
			(true, true, true) => "W",
			(false, false, false) => "K",
			_ => "?"
		};
	}
}
