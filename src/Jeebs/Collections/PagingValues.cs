// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using D = Jeebs.Collections.Defaults.PagingValues;

namespace Jeebs.Collections;

/// <inheritdoc cref="IPagingValues"/>
public sealed record class PagingValues : IPagingValues
{
	/// <inheritdoc/>
	public ulong Items { get; init; }

	/// <inheritdoc/>
	public ulong ItemsPer { get; init; }

	/// <inheritdoc/>
	public ulong FirstItem { get; init; }

	/// <inheritdoc/>
	public ulong LastItem { get; init; }

	/// <inheritdoc/>
	public ulong Page { get; init; }

	/// <inheritdoc/>
	public ulong Pages { get; init; }

	/// <inheritdoc/>
	public ulong PagesPer { get; init; }

	/// <inheritdoc/>
	public ulong LowerPage { get; init; }

	/// <inheritdoc/>
	public ulong UpperPage { get; init; }

	/// <inheritdoc/>
	public int Skip { get; init; }

	/// <inheritdoc/>
	public int Take { get; init; }

	/// <summary>
	/// Create an empty object
	/// </summary>
	public PagingValues() { }

	/// <summary>
	/// Set and calculate values using <see cref="D.ItemsPer"/> and <see cref="D.PagesPer"/>
	/// </summary>
	/// <param name="items">Total number of items</param>
	/// <param name="page">Current page</param>
	public PagingValues(ulong items, ulong page) : this(items, page, D.ItemsPer, D.PagesPer) { }

	/// <summary>
	/// Set and calculate values
	/// If <paramref name="itemsPer"/> is 0, paging will not be used
	/// </summary>
	/// <param name="items">Total number of items</param>
	/// <param name="page">Current page</param>
	/// <param name="itemsPer">Number of items per page</param>
	/// <param name="pagesPer">Number of page numbers before using next / previous</param>
	public PagingValues(ulong items, ulong page, ulong itemsPer, ulong pagesPer)
	{
		// If itemsPerPage is zero, use totalItems instead (i.e. no paging)
		Items = items;
		ItemsPer = itemsPer > 0 ? itemsPer : items;

		// Calculate the number of pages in total - if there are no items,
		// we still display one page, just with no results
		Pages = (Items == 0 || Items == ItemsPer) ? 1 : (ulong)Math.Ceiling((double)Items / ItemsPer);
		PagesPer = pagesPer > 0 ? pagesPer : Pages;

		// Reduce the page number if it is greated than the Number of Pages
		Page = page > 0 ? page : 1;
		if (Page > Pages)
		{
			Page = Pages;
		}

		// Calculate the first and last item variables
		FirstItem = ((Page - 1) * ItemsPer) + 1;
		LastItem = Page * ItemsPer;
		if (LastItem > Items)
		{
			LastItem = Items;
		}

		// Calculate the upper and lower page bounds
		if (Pages < PagesPer)
		{
			LowerPage = 1;
			UpperPage = Pages;
		}
		else
		{
			LowerPage = (ulong)(Math.Floor((double)(Page - 1) / PagesPer) * PagesPer) + 1;
			UpperPage = LowerPage + PagesPer - 1;

			if (UpperPage > Pages)
			{
				UpperPage = Pages;
			}
		}

		// Set Skip and Take
		Skip = (int)(FirstItem > 0 ? FirstItem - 1 : 0);
		Take = (int)ItemsPer;
	}
}
