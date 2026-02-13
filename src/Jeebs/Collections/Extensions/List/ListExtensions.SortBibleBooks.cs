// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Collections;

/// <summary>
/// <see cref="List{T}"/> extension methods.
/// </summary>
public static partial class ListExtensions
{
	/// <summary>
	/// Sort a list of items containing a Bible Book property, according to their place in Scripture.
	/// </summary>
	/// <typeparam name="T">List item type.</typeparam>
	/// <param name="this">The list of objects to sort by Bible Book.</param>
	/// <param name="getName">Bible Book selector.</param>
	/// <returns>List of items sorted by Bible Book.</returns>
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
