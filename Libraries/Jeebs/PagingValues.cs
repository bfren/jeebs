using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Paging Values
	/// </summary>
	public struct PagingValues
	{
		/// <summary>
		/// The page number to display
		/// </summary>
		public readonly long CurrentPage;

		/// <summary>
		/// The total number of items that match the search
		/// </summary>
		public readonly long TotalItems;

		/// <summary>
		/// The number of items to display on each page of results
		/// </summary>
		public readonly long ItemsPerPage;

		/// <summary>
		/// The number of pages per group of page numbers
		/// </summary>
		public readonly long NumberOfPagesPerGroup;

		/// <summary>
		/// The index of the first item being displayed
		/// </summary>
		public readonly long FirstItem;

		/// <summary>
		/// The index + 1 of the last item being displayed
		/// </summary>
		public readonly long LastItem;

		/// <summary>
		/// The number of pages needed to display all the items
		/// </summary>
		public readonly long Pages;

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
		/// </summary>
		/// <param name="currentPage">Current page</param>
		/// <param name="totalItems">Total number of items</param>
		/// <param name="itemsPerPage">[Optional] Number of items per page</param>
		/// <param name="numberOfPagesPerGroup">[Optional] Number of page numbers before using next / previous</param>
		internal PagingValues(long currentPage, long totalItems, long itemsPerPage, long numberOfPagesPerGroup)
		{
			// Ensure a valid current page
			if (currentPage < 0)
			{
				throw new InvalidOperationException($"{nameof(currentPage)} must be greater than zero");
			}

			// Store base values
			CurrentPage = currentPage;
			TotalItems = totalItems;
			ItemsPerPage = itemsPerPage;
			NumberOfPagesPerGroup = numberOfPagesPerGroup;

			// Calculate the number of pages in total - if there are no items,
			// we still display one page, just with no results
			Pages = TotalItems == 0 ? 1 : (long)Math.Ceiling((double)TotalItems / ItemsPerPage);

			// Reduce the page number if it is greated than the Number of Pages
			if (CurrentPage > Pages)
			{
				CurrentPage = Pages;
			}

			// Calculate the first and last item variables
			FirstItem = ((CurrentPage - 1) * ItemsPerPage) + 1;
			LastItem = CurrentPage * ItemsPerPage;
			if (LastItem > TotalItems)
			{
				LastItem = TotalItems;
			}

			// Calculate the upper and lower page bounds
			if (Pages < NumberOfPagesPerGroup)
			{
				LowerPage = 1;
				UpperPage = Pages;
			}
			else
			{
				LowerPage = (long)(Math.Floor((double)(CurrentPage - 1) / NumberOfPagesPerGroup) * NumberOfPagesPerGroup) + 1;
				UpperPage = LowerPage + NumberOfPagesPerGroup - 1;

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
