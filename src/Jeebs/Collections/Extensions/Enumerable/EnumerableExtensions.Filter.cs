// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Collections;

public static partial class EnumerableExtensions
{
	/// <summary>
	/// Filter out null items (and empty / whitespace strings) from a list.
	/// </summary>
	/// <typeparam name="T">Input value type.</typeparam>
	/// <typeparam name="TReturn">Output value type.</typeparam>
	/// <param name="this">List.</param>
	/// <param name="map">Mapping function.</param>
	/// <returns>Original list with null and empty items removed.</returns>
	public static IEnumerable<TReturn> Filter<T, TReturn>(this IEnumerable<T> @this, Func<T, TReturn?> map)
		where TReturn : class
	{
		foreach (var x in @this)
		{
			if (map(x) is TReturn y)
			{
				if (y is string z)
				{
					if (!string.IsNullOrWhiteSpace(z))
					{
						yield return y;
					}
				}
				else
				{
					yield return y;
				}
			}
		}
	}

	/// <summary>
	/// Filter out null items from a list.
	/// </summary>
	/// <typeparam name="T">Input value type.</typeparam>
	/// <typeparam name="TReturn">Output value type.</typeparam>
	/// <param name="this">List.</param>
	/// <param name="map">Mapping function.</param>
	/// <returns>Original list with null items removed.</returns>
	public static IEnumerable<TReturn> Filter<T, TReturn>(this IEnumerable<T> @this, Func<T, TReturn?> map)
		where TReturn : struct
	{
		foreach (var x in @this)
		{
			if (map(x) is TReturn y)
			{
				yield return y;
			}
		}
	}
}
