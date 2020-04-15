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
		/// The total number of items that match the search
		/// </summary>
		public readonly long Items;

		/// <summary>
		/// The number of items to display on each page of results
		/// </summary>
		public readonly long ItemsPerPage;

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
		public readonly long CurrentPage;

		/// <summary>
		/// The number of pages needed to display all the items
		/// </summary>
		public readonly long Pages;

		/// <summary>
		/// The number of pages per group of page numbers
		/// </summary>
		public readonly long PagesPerGroup;

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
		/// <param name="currentPage">Current page</param>
		/// <param name="itemsCount">Total number of items</param>
		/// <param name="itemsPerPage">[Optional] Number of items per page</param>
		/// <param name="pagesPerGroup">[Optional] Number of page numbers before using next / previous</param>
		public PagingValues(long currentPage, long itemsCount, long itemsPerPage = 10, long pagesPerGroup = 10)
		{
			// Ensure a valid current page
			if (currentPage < 0)
			{
				throw new InvalidOperationException($"{nameof(currentPage)} must be greater than zero");
			}

			// If itemsPerPage is zero, use totalItems instead (i.e. no paging)
			Items = itemsCount;
			ItemsPerPage = itemsPerPage > 0 ? itemsPerPage : itemsCount;

			// Calculate the number of pages in total - if there are no items,
			// we still display one page, just with no results
			Pages = (Items == 0 || Items == ItemsPerPage) ? 1 : (long)Math.Ceiling((double)Items / ItemsPerPage);
			PagesPerGroup = pagesPerGroup > 0 ? pagesPerGroup : Pages;

			// Reduce the page number if it is greated than the Number of Pages
			CurrentPage = currentPage > 0 ? currentPage : 1;
			if (CurrentPage > Pages)
			{
				CurrentPage = Pages;
			}

			// Calculate the first and last item variables
			FirstItem = ((CurrentPage - 1) * ItemsPerPage) + 1;
			LastItem = CurrentPage * ItemsPerPage;
			if (LastItem > Items)
			{
				LastItem = Items;
			}

			// Calculate the upper and lower page bounds
			if (Pages < PagesPerGroup)
			{
				LowerPage = 1;
				UpperPage = Pages;
			}
			else
			{
				LowerPage = (long)(Math.Floor((double)(CurrentPage - 1) / PagesPerGroup) * PagesPerGroup) + 1;
				UpperPage = LowerPage + PagesPerGroup - 1;

				if (UpperPage > Pages)
				{
					UpperPage = Pages;
				}
			}

			// Set Skip and Take
			Skip = (int)(FirstItem > 0 ? FirstItem - 1 : 0);
			Take = (int)ItemsPerPage;
		}
	}
}
