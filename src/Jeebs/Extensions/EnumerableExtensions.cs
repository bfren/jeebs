// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using D = Jeebs.Defaults.PagingValues;

namespace Jeebs;

/// <summary>
/// IEnumerable Extensions - Filter
/// </summary>
public static class EnumerableExtensions
{
	/// <summary>
	/// Filter out null items (and empty / whitespace strings) from a list
	/// </summary>
	/// <typeparam name="T">Input value type</typeparam>
	/// <typeparam name="TReturn">Output value type</typeparam>
	/// <param name="this">List</param>
	/// <param name="map">Mapping function</param>
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
	/// Filter out null items from a list
	/// </summary>
	/// <typeparam name="T">Input value type</typeparam>
	/// <typeparam name="TReturn">Output value type</typeparam>
	/// <param name="this">List</param>
	/// <param name="map">Mapping function</param>
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

	/// <summary>
	/// Convert a collection to an immutable list
	/// </summary>
	/// <typeparam name="T">Item type</typeparam>
	/// <param name="this">Collection</param>
	public static IImmutableList<T> ToImmutableList<T>(this IEnumerable<T> @this) =>
		new ImmutableList<T>(@this);

	/// <summary>
	/// Convert a collection to a paged list using <see cref="D.ItemsPer"/> and <see cref="D.PagesPer"/>
	/// </summary>
	/// <typeparam name="T">Item type</typeparam>
	/// <param name="this">Collection</param>
	/// <param name="page">Current page</param>
	public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> @this, ulong page) =>
		ToPagedList(@this, page, D.ItemsPer, D.PagesPer);

	/// <summary>
	/// Convert a collection to a paged list
	/// </summary>
	/// <typeparam name="T">Item type</typeparam>
	/// <param name="this">Collection</param>
	/// <param name="page">Current page</param>
	/// <param name="itemsPer">Items per page</param>
	/// <param name="pagesPer">Pages per screen</param>
	public static IPagedList<T> ToPagedList<T>(
		this IEnumerable<T> @this,
		ulong page,
		ulong itemsPer,
		ulong pagesPer
	)
	{
		// Return empty list
		if (!@this.Any())
		{
			return new PagedList<T>();
		}

		// Calculate values and get items
		var values = new PagingValues(items: (ulong)@this.Count(), page: page, itemsPer: itemsPer, pagesPer: pagesPer);
		var items = @this.Skip(values.Skip).Take(values.Take);

		// Create new paged list with only the necessary items
		return new PagedList<T>(values, items);
	}
}
