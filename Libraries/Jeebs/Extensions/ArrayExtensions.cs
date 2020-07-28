using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
			if (additionalItems is null)
			{
				return @this;
			}

			return @this.Concat(additionalItems).ToArray();
		}

		/// <summary>
		/// Shuffle elements in an array using a Fisher-Yates Shuffle
		/// See https://forums.asp.net/post/4869658.aspx
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="this">Array to shuffle</param>
		/// <returns>Shuffled array</returns>
		public static T[] Shuffle<T>(this T[] @this)
		{
			var shuffled = @this.ToArray();
			var random = new Random();

			for (int i = shuffled.Length; i > 1; i--)
			{
				int j = random.Next(i);
				T tmp = shuffled[j];
				shuffled[j] = shuffled[i - 1];
				shuffled[i - 1] = tmp;
			}

			return shuffled;
		}
	}
}
