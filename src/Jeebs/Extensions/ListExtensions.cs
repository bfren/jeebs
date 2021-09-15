// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using static F.OptionF;

namespace Jeebs;

/// <summary>
/// IList Extensions
/// </summary>
public static class ListExtensions
{
	/// <summary>
	/// Return the items either side of <paramref name="item"/> - or <see cref="None{T}"/>
	/// </summary>
	/// <remarks>
	/// NB: if there are multiple items matching <paramref name="item"/>, the first will be used
	/// (this is how <see cref="List{T}.IndexOf(T)"/> works)
	/// </remarks>
	/// <typeparam name="T">Item type</typeparam>
	/// <param name="this">List of items</param>
	/// <param name="item">Item to match</param>
	public static (Option<T> prev, Option<T> next) GetEitherSide<T>(this List<T> @this, T item)
	{
		static (Option<T>, Option<T>) invalid(IMsg reason) =>
			(None<T>(reason), None<T>(reason));

		// There are no items
		if (@this.Count == 0)
		{
			return invalid(new Msg.ListIsEmptyMsg());
		}

		// There is only one item
		if (@this.Count == 1)
		{
			return invalid(new Msg.ListContainsSingleItemMsg());
		}

		// The item is not in the list
		if (!@this.Contains(item))
		{
			return invalid(new Msg.ListDoesNotContainItemMsg<T>(item));
		}

		// Get the index of the item
		var index = @this.IndexOf(item);

		// If it is the first item, Previous should be None
		if (index == 0)
		{
			return (None<T, Msg.ItemIsFirstItemMsg>(), @this[1]);
		}
		// If it is the last item, Next should be None
		else if (index == @this.Count - 1)
		{
			return (@this[index - 1], None<T, Msg.ItemIsLastItemMsg>());
		}
		// Return the items either side of the item
		else
		{
			return (@this[index - 1], @this[index + 1]);
		}
	}

	/// <summary>
	/// Get a slice of values from a list
	/// </summary>
	/// <typeparam name="T">List item type</typeparam>
	/// <param name="this">List</param>
	/// <param name="range">Range of slice to return (exclusive of end element)</param>
	public static List<T> GetSlice<T>(this List<T> @this, Range range)
	{
		var (start, length) = range.GetOffsetAndLength(@this.Count);
		return @this.GetRange(start, length);
	}

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

	/// <summary>Messages</summary>
	public static class Msg
	{
		/// <summary>List is empty</summary>
		public sealed record class ListIsEmptyMsg : IMsg { }

		/// <summary>List contains only one item</summary>
		public sealed record class ListContainsSingleItemMsg : IMsg { }

		/// <summary>List does not contain the specified item</summary>
		public sealed record class ListDoesNotContainItemMsg<T>(T Value) : WithValueMsg<T>;

		/// <summary>The specified item is the first in the list (so there is no previous item)</summary>
		public sealed record class ItemIsFirstItemMsg : IMsg { }

		/// <summary>The specified item is the last in the list (so there is no next item)</summary>
		public sealed record class ItemIsLastItemMsg : IMsg { }
	}
}
