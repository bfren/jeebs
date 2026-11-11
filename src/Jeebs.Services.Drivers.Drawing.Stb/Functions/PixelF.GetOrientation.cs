// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Buffers.Binary;

namespace Jeebs.Services.Drivers.Drawing.Stb.Functions;

internal static partial class PixelF
{
	/// <summary>
	/// Read the EXIF orientation (1 - 8) of a JPEG file, or 1 (as stored) if there isn't one.
	/// </summary>
	/// <remarks>
	/// Cameras and phones save photos as the sensor saw them and record how to turn them here.
	/// StbImageSharp ignores it, so <see cref="Orient"/> applies it - as Skia does.
	/// </remarks>
	/// <param name="data">Encoded image.</param>
	internal static int GetOrientation(byte[] data)
	{
		// JPEG files start with FF D8 - other formats are returned as stored
		if (data.Length < 4 || data[0] != 0xFF || data[1] != 0xD8)
		{
			return 1;
		}

		// Walk the header segments: FF, marker, two-byte length (including itself), contents
		var i = 2;
		while (i + 4 <= data.Length && data[i] == 0xFF)
		{
			var marker = data[i + 1];
			var length = (data[i + 2] << 8) | data[i + 3];

			// Start of the image data, end of the image, or a broken length: no more headers
			if (marker is 0xDA or 0xD9 || length < 2)
			{
				break;
			}

			// EXIF lives in an APP1 segment
			if (marker == 0xE1)
			{
				var contents = data.AsSpan(i + 4, Math.Min(length - 2, data.Length - i - 4));
				if (ReadExifOrientation(contents) is int orientation)
				{
					return orientation;
				}
			}

			i += 2 + length;
		}

		return 1;
	}

	/// <summary>
	/// Read the orientation tag (0x0112) from the first image directory of an APP1 EXIF segment.
	/// </summary>
	/// <param name="app1">APP1 segment contents.</param>
	private static int? ReadExifOrientation(ReadOnlySpan<byte> app1)
	{
		if (app1.Length < 14 || !app1[..6].SequenceEqual("Exif\0\0"u8))
		{
			return null;
		}

		// TIFF header: II (little-endian) or MM (big-endian), 42, offset of the first directory
		var tiff = app1[6..];
		var little = tiff[0] == 'I' && tiff[1] == 'I';
		if (!little && !(tiff[0] == 'M' && tiff[1] == 'M'))
		{
			return null;
		}

		var directory = ReadUInt32(tiff, 4, little);
		if (directory < 8 || directory > tiff.Length - 2)
		{
			return null;
		}

		// Each entry is 12 bytes: tag, type, count, value - a SHORT value sits in the first two value bytes
		var start = (int)directory;
		var count = ReadUInt16(tiff, start, little);
		for (var n = 0; n < count; n++)
		{
			var entry = start + 2 + (n * 12);
			if (entry + 12 > tiff.Length)
			{
				return null;
			}

			if (ReadUInt16(tiff, entry, little) == 0x0112)
			{
				var value = ReadUInt16(tiff, entry + 8, little);
				return value is >= 1 and <= 8 ? value : null;
			}
		}

		return null;
	}

	private static ushort ReadUInt16(ReadOnlySpan<byte> span, int offset, bool little) =>
		little ? BinaryPrimitives.ReadUInt16LittleEndian(span[offset..]) : BinaryPrimitives.ReadUInt16BigEndian(span[offset..]);

	private static uint ReadUInt32(ReadOnlySpan<byte> span, int offset, bool little) =>
		little ? BinaryPrimitives.ReadUInt32LittleEndian(span[offset..]) : BinaryPrimitives.ReadUInt32BigEndian(span[offset..]);
}
