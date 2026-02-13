// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Collections;

public static partial class ListExtensions
{
	/// <summary>
	/// Return the items either side of <paramref name="item"/> - or <see cref="None"/>.
	/// </summary>
	/// <remarks>
	/// NB: if there are multiple items matching <paramref name="item"/>, the first will be used
	/// (this is how <see cref="List{T}.IndexOf(T)"/> works)
	/// </remarks>
	/// <typeparam name="T">Item type.</typeparam>
	/// <param name="this">List of items.</param>
	/// <param name="item">Item to match.</param>
	/// <returns>The items in the list either side of <paramref name="item"/>.</returns>
	public static (Maybe<T> prev, Maybe<T> next) GetEitherSide<T>(this List<T> @this, T item)
	{
		// There are no items
		// There is only one item
		// The item is not in the list
		if (@this.Count == 0 || @this.Count == 1 || !@this.Contains(item))
		{
			return (M.None, M.None);
		}

		return @this.IndexOf(item) switch
		{
			// If it is the first item, Previous should be None
			0 =>
				(M.None, @this[1]),

			// If it is the last item, Next should be None
			{ } x when x == (@this.Count - 1) =>
				(@this[^2], M.None),

			// Return the items either side of the item
			{ } x =>
				 (@this[x - 1], @this[x + 1])
		};
	}
}
