// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Services.Drawing.Geometry;

namespace Jeebs.Services.Drivers.Drawing.Stb.Functions;

internal static partial class PixelF
{
	/// <summary>
	/// Crop an area of an image and resize it, smoothing as it goes.
	/// </summary>
	/// <remarks>
	/// Uses a tent filter in two passes (across, then down). When enlarging this is the same as
	/// bilinear; when shrinking the filter widens so every source pixel counts towards the result.
	/// Colour is weighted by alpha, so transparent pixels don't darken the edges around them.
	/// </remarks>
	/// <param name="pixels">Source pixels.</param>
	/// <param name="width">Source width.</param>
	/// <param name="height">Source height.</param>
	/// <param name="crop">Area of the source to use.</param>
	/// <param name="newWidth">Output width.</param>
	/// <param name="newHeight">Output height.</param>
	internal static byte[] Resize(byte[] pixels, int width, int height, Rectangle crop, int newWidth, int newHeight)
	{
		var columns = GetWeights(crop.X, crop.Width, newWidth, width);
		var rows = GetWeights(crop.Y, crop.Height, newHeight, height);

		// Only the source rows the second pass reads need the first pass
		var top = rows[0].Start;
		var bottom = rows[^1].Start + rows[^1].Weights.Length;

		// First pass: across each source row - colour is stored multiplied by alpha
		var across = new float[(bottom - top) * newWidth * 4];
		for (var y = top; y < bottom; y++)
		{
			for (var x = 0; x < newWidth; x++)
			{
				var (start, weights) = columns[x];
				float r = 0, g = 0, b = 0, a = 0;
				for (var k = 0; k < weights.Length; k++)
				{
					var i = ((y * width) + start + k) * 4;
					var wa = weights[k] * pixels[i + 3];
					r += wa * pixels[i];
					g += wa * pixels[i + 1];
					b += wa * pixels[i + 2];
					a += wa;
				}

				var t = (((y - top) * newWidth) + x) * 4;
				(across[t], across[t + 1], across[t + 2], across[t + 3]) = (r, g, b, a);
			}
		}

		// Second pass: down each column, then divide the colour by alpha again
		var output = new byte[newWidth * newHeight * 4];
		for (var y = 0; y < newHeight; y++)
		{
			var (start, weights) = rows[y];
			for (var x = 0; x < newWidth; x++)
			{
				float r = 0, g = 0, b = 0, a = 0;
				for (var k = 0; k < weights.Length; k++)
				{
					var t = (((start - top + k) * newWidth) + x) * 4;
					r += weights[k] * across[t];
					g += weights[k] * across[t + 1];
					b += weights[k] * across[t + 2];
					a += weights[k] * across[t + 3];
				}

				var o = ((y * newWidth) + x) * 4;
				if (a > 0)
				{
					(output[o], output[o + 1], output[o + 2]) = (ToByte(r / a), ToByte(g / a), ToByte(b / a));
				}

				output[o + 3] = ToByte(a);
			}
		}

		return output;
	}

	/// <summary>
	/// For each output pixel along one side, the first source pixel it reads and how much each one counts.
	/// </summary>
	/// <param name="offset">Where the cropped area starts.</param>
	/// <param name="length">Length of the cropped area.</param>
	/// <param name="newLength">Output length.</param>
	/// <param name="limit">Length of the whole source side - reads stay inside it.</param>
	internal static (int Start, float[] Weights)[] GetWeights(int offset, int length, int newLength, int limit)
	{
		var scale = (double)newLength / length;

		// Half-width of the tent, in source pixels: one when enlarging, wider when shrinking
		var support = Math.Max(1d, 1d / scale);

		var result = new (int, float[])[newLength];
		for (var i = 0; i < newLength; i++)
		{
			// Centre of this output pixel, in source coordinates
			var centre = offset + ((i + 0.5) / scale);
			var start = Math.Max(0, (int)Math.Floor(centre - support));
			var end = Math.Min(limit, (int)Math.Ceiling(centre + support));

			var weights = new float[end - start];
			var sum = 0f;
			for (var j = 0; j < weights.Length; j++)
			{
				weights[j] = (float)Math.Max(0, 1 - (Math.Abs(start + j + 0.5 - centre) / support));
				sum += weights[j];
			}

			for (var j = 0; j < weights.Length; j++)
			{
				weights[j] /= sum;
			}

			result[i] = (start, weights);
		}

		return result;
	}

	private static byte ToByte(float value) =>
		(byte)Math.Clamp((int)MathF.Round(value), 0, 255);
}
