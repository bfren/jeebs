// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Security.Cryptography;

namespace F
{
	public static partial class Rnd
	{
		public static partial class NumberF
		{
			/// <summary>
			/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
			/// </summary>
			/// <remarks>
			/// Don't share code with <see cref="GetInt64(long, long, RandomNumberGenerator?)"/> for memory allocation reasons
			/// </remarks>
			/// <param name="min">Minimum acceptable value</param>
			/// <param name="max">Maximum acceptable value</param>
			public static int GetInt32(int min = 0, int max = int.MaxValue)
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
				return (int)(min + add);
			}
		}
	}
}
