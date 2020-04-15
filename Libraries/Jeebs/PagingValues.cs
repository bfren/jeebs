using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Paging Values
	/// </summary>
	public sealed class PagingValues
	{
		/// <summary>
		/// Default values
		/// </summary>
		public static class Defaults
		{
			/// <summary>
			/// The number of items to display on each page of results
			/// </summary>
			public const long ItemsPer = 10;

			/// <summary>
			/// The number of pages per group of page numbers
			/// </summary>
			public const long PagesPer = 10;
		}

		/// <summary>
		/// The total number of items that match the search
		/// </summary>
		public readonly long Items;

		/// <summary>
		/// The number of items to display on each page of results
		/// </summary>
		public readonly long ItemsPer;

		/// <summary>
		/// The index of the first item being displayed
		/// </summary>
		public readonly long FirstItem;

		/// <summary>
		/// The index + 1 of the last item being displayed
		/// </summary>
		public readonly long LastItem;

		/// <summary>
		/// The page number to display
		/// </summary>
		public readonly long Page;

		/// <summary>
		/// The number of pages needed to display all the items
		/// </summary>
		public readonly long Pages;

		/// <summary>
		/// The number of pages per group of page numbers
		/// </summary>
		public readonly long PagesPer;

		/// <summary>
		/// The first page to display
		/// </summary>
		public readonly long LowerPage;

		/// <summary>
		/// The last page to display
		/// </summary>
		public readonly long UpperPage;

		/// <summary>
		/// The number of items to Skip() in a Linq query
		/// </summary>
		public readonly int Skip;

		/// <summary>
		/// The number of items to Take() in a Linq query
		/// </summary>
		public readonly int Take;

		/// <summary>
		/// Set and calculate values
		/// If itemsPerPage is 0, paging will not be used
		/// If numberOfPages
		/// </summary>
		/// <param name="items">Total number of items</param>
		/// <param name="page">Current page</param>
		/// <param name="itemsPer">[Optional] Number of items per page</param>
		/// <param name="pagesPer">[Optional] Number of page numbers before using next / previous</param>
		public PagingValues(long items, long page, long itemsPer = Defaults.ItemsPer, long pagesPer = Defaults.PagesPer)
		{
			// Ensure a valid current page
			if (page < 0)
			{
				throw new InvalidOperationException($"{nameof(page)} must be greater than zero");
			}

			// If itemsPerPage is zero, use totalItems instead (i.e. no paging)
			Items = items;
			ItemsPer = itemsPer > 0 ? itemsPer : items;

			// Calculate the number of pages in total - if there are no items,
			// we still display one page, just with no results
			Pages = (Items == 0 || Items == ItemsPer) ? 1 : (long)Math.Ceiling((double)Items / ItemsPer);
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
				LowerPage = (long)(Math.Floor((double)(Page - 1) / PagesPer) * PagesPer) + 1;
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
