// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Security.Cryptography;

namespace F
{
	/// <summary>
	/// Maths shorthands
	/// </summary>
	public static class MathsF
	{
		private const string MinimumMustBeLessThanMaximum = "Minimium value must be less than the maximum value.";

		private const string MinimumMustBeAtLeastZero = "Minimum value must be at least 0.";

		/// <summary>
		/// Returns a random number between 0 and 1
		/// </summary>
		/// <remarks>
		/// Thanks to https://stackoverflow.com/users/11178549/theodor-zoulias for comments and suggested improvements
		/// - see https://stackoverflow.com/a/64264895/8199362
		/// </remarks>
		/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
		public static double Random(RandomNumberGenerator? generator = null)
		{
			// Get 8 random bytes to convert into a 64-bit integer
			var lng = BitConverter.ToInt64(ByteF.Random(8, generator), 0);
			var dbl = (double)(lng < 0 ? ~lng : lng);

			// Convert to a random number between 0 and 1
			return dbl / long.MaxValue;
		}

		/// <summary>
		/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
		/// </summary>
		/// <remarks>
		/// Don't share code with <see cref="RandomInt64(long, long, RandomNumberGenerator?)"/> for memory allocation reasons
		/// </remarks>
		/// <param name="min">Minimum acceptable value</param>
		/// <param name="max">Maximum acceptable value</param>
		/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
		public static int RandomInt32(int min = 0, int max = int.MaxValue, RandomNumberGenerator? generator = null)
		{
			// Check arguments
			if (min >= max)
			{
				throw new ArgumentOutOfRangeException(nameof(min), min, MinimumMustBeLessThanMaximum);
			}

			if (min < 0)
			{
				throw new ArgumentException(MinimumMustBeAtLeastZero, nameof(min));
			}

			// Get the range between the specified minimum and maximum values
			var range = max - min;

			// Now add a random amount of the range to the minimum value - it will never exceed maximum value
			var add = Math.Round(range * Random(generator));
			return (int)(min + add);
		}

		/// <summary>
		/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
		/// </summary>
		/// <remarks>
		/// Don't share code with <see cref="RandomInt32(int, int, RandomNumberGenerator?)"/> for memory allocation reasons
		/// </remarks>
		/// <param name="min">Minimum acceptable value</param>
		/// <param name="max">Maximum acceptable value</param>
		/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RNGCryptoServiceProvider"/></param>
		public static long RandomInt64(long min = 0, long max = long.MaxValue, RandomNumberGenerator? generator = null)
		{
			// Check arguments
			if (min >= max)
			{
				throw new ArgumentOutOfRangeException(nameof(min), min, MinimumMustBeLessThanMaximum);
			}

			if (min < 0)
			{
				throw new ArgumentException(MinimumMustBeAtLeastZero, nameof(min));
			}

			// Get the range between the specified minimum and maximum values
			var range = max - min;

			// Now add a random amount of the range to the minimum value - it will never exceed maximum value
			var add = Math.Round(range * Random(generator));
			return (long)(min + add);
		}
	}
}
