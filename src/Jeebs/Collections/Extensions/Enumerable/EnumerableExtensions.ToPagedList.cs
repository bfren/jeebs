// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using D = Jeebs.Constants.PagingValues;

namespace Jeebs.Collections;

public static partial class EnumerableExtensions
{
	/// <summary>
	/// Convert a collection to a paged list using <see cref="D.ItemsPer"/> and <see cref="D.PagesPer"/>,
	/// using default paging values.
	/// </summary>
	/// <typeparam name="T">Item type.</typeparam>
	/// <param name="this">Collection.</param>
	/// <param name="page">Current page.</param>
	/// <returns>Paged List containing items on page <paramref name="page"/>.</returns>
	public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> @this, ulong page) =>
		ToPagedList(@this, page, D.ItemsPer, D.PagesPer);

	/// <summary>
	/// Convert a collection to a paged list.
	/// </summary>
	/// <typeparam name="T">Item type.</typeparam>
	/// <param name="this">Collection.</param>
	/// <param name="page">Current page.</param>
	/// <param name="itemsPer">Items per page.</param>
	/// <param name="pagesPer">Pages per screen.</param>
	/// <returns>Paged List containing items on page <paramref name="page"/>.</returns>
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
