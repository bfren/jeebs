// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using D = Jeebs.Defaults.PagingValues;

namespace Jeebs
{
	/// <inheritdoc cref="IPagingValues"/>
	public readonly record struct PagingValues : IPagingValues
	{
		/// <inheritdoc/>
		public ulong Items { get; init; } = 0;

		/// <inheritdoc/>
		public ulong ItemsPer { get; init; } = 0;

		/// <inheritdoc/>
		public ulong FirstItem { get; init; } = 0;

		/// <inheritdoc/>
		public ulong LastItem { get; init; } = 0;

		/// <inheritdoc/>
		public ulong Page { get; init; } = 0;

		/// <inheritdoc/>
		public ulong Pages { get; init; } = 0;

		/// <inheritdoc/>
		public ulong PagesPer { get; init; } = 0;

		/// <inheritdoc/>
		public ulong LowerPage { get; init; } = 0;

		/// <inheritdoc/>
		public ulong UpperPage { get; init; } = 0;

		/// <inheritdoc/>
		public int Skip { get; init; } = 0;

		/// <inheritdoc/>
		public int Take { get; init; } = 0;

		/// <summary>
		/// Create empty values
		/// </summary>
		public PagingValues() { }

		/// <summary>
		/// Set and calculate values
		/// If itemsPerPage is 0, paging will not be used
		/// If numberOfPages
		/// </summary>
		/// <param name="items">Total number of items</param>
		/// <param name="page">Current page</param>
		/// <param name="itemsPer">[Optional] Number of items per page</param>
		/// <param name="pagesPer">[Optional] Number of page numbers before using next / previous</param>
		public PagingValues(ulong items, ulong page, ulong itemsPer = D.ItemsPer, ulong pagesPer = D.PagesPer)
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
}
