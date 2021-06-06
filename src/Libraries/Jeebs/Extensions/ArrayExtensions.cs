// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Linq;

namespace Jeebs
{
	/// <summary>
	/// Array Extensions
	/// </summary>
	public static class ArrayExtensions
	{
		/// <summary>
		/// Extend an array with additional items
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="this">Original array</param>
		/// <param name="additionalItems">Additional items</param>
		/// <returns>Extended array</returns>
		public static T[] ExtendWith<T>(this T[] @this, params T[] additionalItems)
		{
			if (additionalItems is null || additionalItems.Length == 0)
			{
				return @this;
			}

			return @this.Concat(additionalItems).ToArray();
		}
	}
}
