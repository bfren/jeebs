// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// IList Extensions
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Sort a list of Bible books, according to their place in Scripture
		/// </summary>
		/// <typeparam name="T">Object Type</typeparam>
		/// <param name="this">The list of Bible Books</param>
		/// <param name="getName">Function to return name of Bible Book</param>
		/// <returns>Sorted list of Bible Books</returns>
		public static void SortBibleBooks<T>(this List<T> @this, Func<T, string> getName)
		{
			// If empty list, do nothing
			if (@this.Count == 0)
			{
				return;
			}

			// Get list of Bible Books
			var bibleBooks = Constants.BibleBooks.All;

			// Create comparison
			var comp = new Comparison<T>(
				(x, y) => bibleBooks.IndexOf(getName(x)) - bibleBooks.IndexOf(getName(y))
			);

			// Sort list
			@this.Sort(comp);
		}
	}
}
