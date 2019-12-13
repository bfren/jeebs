using System;
using System.Collections.Generic;
using System.Text;

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
		/// <param name="list">The list of Bible Books</param>
		/// <param name="getName">Function to return name of Bible Book</param>
		/// <returns>Sorted list of Bible Books</returns>
		public static List<T> SortBibleBooks<T>(this IList<T> list, Func<T, string> getName)
		{
			// Get list of Bible Books
			var bibleBooks = Constants.BibleBooks.All;

			// Create comparison
			var comp = new Comparison<T>((x, y) => bibleBooks.IndexOf(getName(x)) - bibleBooks.IndexOf(getName(y)));

			// Sort list
			var sorted = new List<T>(list);
			sorted.Sort(comp);

			// Return list
			return sorted;
		}
	}
}
