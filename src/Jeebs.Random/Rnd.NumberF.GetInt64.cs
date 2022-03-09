// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Random;

public static partial class Rnd
{
	public static partial class NumberF
	{
		/// <summary>
		/// Returns a random integer between <see langword="0"/> and <see cref="long.MaxValue"/> inclusive
		/// </summary>
		/// <remarks>
		/// Don't share code with <see cref="GetInt32(int, int)"/> for memory allocation reasons
		/// </remarks>
		public static long GetInt64() =>
			GetInt64(0, long.MaxValue);

		/// <summary>
		/// Returns a random integer between <see langword="0"/> and <paramref name="max"/> inclusive
		/// </summary>
		/// <remarks>
		/// Don't share code with <see cref="GetInt32(int, int)"/> for memory allocation reasons
		/// </remarks>
		/// <param name="max">Maximum acceptable value</param>
		public static long GetInt64(long max) =>
			GetInt64(0, max);

		/// <summary>
		/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
		/// </summary>
		/// <remarks>
		/// Don't share code with <see cref="GetInt32(int, int)"/> for memory allocation reasons
		/// </remarks>
		/// <param name="min">Minimum acceptable value</param>
		/// <param name="max">Maximum acceptable value</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public static long GetInt64(long min, long max)
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
			var add = Math.Round(range * Get());
			return (long)(min + add);
		}
	}
}
