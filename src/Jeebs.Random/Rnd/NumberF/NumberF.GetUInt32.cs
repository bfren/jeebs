// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace F;

public static partial class Rnd
{
	public static partial class NumberF
	{
		/// <summary>
		/// Returns a random integer between <see langword="0"/> and <see cref="uint.MaxValue"/> inclusive
		/// </summary>
		/// <remarks>
		/// Don't share code with <see cref="GetUInt64(ulong, ulong)"/> for memory allocation reasons
		/// </remarks>
		public static uint GetUInt32() =>
			GetUInt32(0, uint.MaxValue);

		/// <summary>
		/// Returns a random integer between <see langword="0"/> and <see cref="uint.MaxValue"/> inclusive
		/// </summary>
		/// <remarks>
		/// Don't share code with <see cref="GetUInt64(ulong, ulong)"/> for memory allocation reasons
		/// </remarks>
		/// <param name="max">Maximum acceptable value</param>
		public static uint GetUInt32(uint max) =>
			GetUInt32(0, max);

		/// <summary>
		/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
		/// </summary>
		/// <remarks>
		/// Don't share code with <see cref="GetUInt64(ulong, ulong)"/> for memory allocation reasons
		/// </remarks>
		/// <param name="min">Minimum acceptable value</param>
		/// <param name="max">Maximum acceptable value</param>
		public static uint GetUInt32(uint min, uint max)
		{
			// Check arguments
			if (min >= max)
			{
				throw new ArgumentOutOfRangeException(nameof(min), min, MinimumMustBeLessThanMaximum);
			}

			// Get the range between the specified minimum and maximum values
			var range = max - min;

			// Now add a random amount of the range to the minimum value - it will never exceed maximum value
			var add = Math.Round(range * Get());
			return (uint)(min + add);
		}
	}
}
