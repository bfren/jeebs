using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace F
{
	/// <summary>
	/// Maths shorthands
	/// </summary>
	public static class MathsF
	{
		/// <summary>
		/// Returns a random number between 0 and 1
		/// </summary>
		public static double Random()
		{
			// Get a fresh provider
			using var csp = new RNGCryptoServiceProvider();

			// 8 bytes = 64-bit
			byte[] b = new byte[8];
			csp.GetBytes(b);
			var dbl = (double)BitConverter.ToInt64(b, 0);

			// Convert to a random number between 0 and 1
			return Math.Abs(dbl) / long.MinValue * -1;
		}

		/// <summary>
		/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
		/// </summary>
		/// <param name="min">Minimum acceptable value</param>
		/// <param name="max">Maximum acceptable value</param>
		public static int RandomInt32(int min = 0, int max = int.MaxValue)
			=> (int)RandomInt64(min, max);

		/// <summary>
		/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
		/// </summary>
		/// <param name="min">Minimum acceptable value</param>
		/// <param name="max">Maximum acceptable value</param>
		public static long RandomInt64(long min = 0, long max = long.MaxValue)
		{
			// Check arguments
			if (min >= max)
			{
				throw new ArgumentOutOfRangeException(nameof(min), min, "Minimium value must be less than the maximum value.");
			}

			if (min < 0)
			{
				throw new ArgumentException("Minimum value must be at least 0.", nameof(min));
			}

			// Get the range between the specified minimum and maximum values
			var range = max - min;

			// Now add a random amount of the range to the minimum value - it will never exceed maximum value
			var add = Math.Round(range * Random());
			return (long)(min + add);
		}
	}
}
