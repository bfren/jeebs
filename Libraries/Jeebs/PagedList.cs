using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs
{
	/// <summary>
	/// List that supports paging operations
	/// </summary>
	/// <typeparam name="T">Type of objects in the list</typeparam>
	public sealed class PagedList<T> : List<T>, IPagedList<T>
	{
		/// <summary>
		/// The page number to display
		/// </summary>
		public double CurrentPage { get; private set; }

		/// <summary>
		/// The total number of items that match the search
		/// </summary>
		public double TotalItems { get; }

		/// <summary>
		/// The number of items to display on each page of results
		/// </summary>
		public double ItemsPerPage { get; set; }

		/// <summary>
		/// The number of pages per group of page numbers
		/// </summary>
		public double NumberOfPagesPerGroup { get; set; }

		/// <summary>
		/// The index of the first item being displayed
		/// </summary>
		public double FirstItem { get; private set; }

		/// <summary>
		/// The number of items to Skip() in a Linq query
		/// </summary>
		public int Skip { get => (int)(FirstItem > 0 ? FirstItem - 1 : 0); }

		/// <summary>
		/// The index + 1 of the last item being displayed
		/// </summary>
		public double LastItem { get; private set; }

		/// <summary>
		/// The number of items to Take() in a Linq query
		/// </summary>
		public int Take { get => (int)ItemsPerPage; }

		/// <summary>
		/// The number of pages needed to display all the items
		/// </summary>
		public double Pages { get; private set; }

		/// <summary>
		/// The first page to display
		/// </summary>
		public double LowerPage { get; private set; }

		/// <summary>
		/// The last page to display
		/// </summary>
		public double UpperPage { get; private set; }

		/// <summary>
		/// Create PagedList from a collection of items
		/// </summary>
		/// <param name="collection">Collection</param>
		/// <param name="currentPage">Current page</param>
		public PagedList(IEnumerable<T> collection, double currentPage) : base(collection)
		{
			TotalItems = Count;
			CurrentPage = currentPage;
		}

		/// <summary>
		/// Set required parameters and calculate values
		/// </summary>
		/// <param name="currentPage">Current page</param>
		/// <param name="totalItems">Total number of items</param>
		/// <param name="itemsPerPage">[Optional] Number of items per page</param>
		/// <param name="numberOfPagesPerGroup">[Optional] Number of page numbers before using next / previous</param>
		public PagedList(double currentPage, double totalItems, double itemsPerPage = 10, double numberOfPagesPerGroup = 10)
		{
			CurrentPage = currentPage;
			TotalItems = totalItems;
			ItemsPerPage = itemsPerPage;
			NumberOfPagesPerGroup = numberOfPagesPerGroup;
		}

		/// <summary>
		/// Calculate the various paging values
		/// </summary>
		public void Calculate()
		{
			// Ensure a valid current page
			if (CurrentPage < 0)
			{
				throw new InvalidOperationException($"{nameof(CurrentPage)} must be greater than zero");
			}

			if (CurrentPage == 0)
			{
				CurrentPage = 1;
			}

			// Calculate the number of pages in total - if there are no items,
			// we still display one page, just with no results
			Pages = TotalItems == 0 ? 1 : Math.Ceiling(TotalItems / ItemsPerPage);

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
				LowerPage = (Math.Floor((CurrentPage - 1) / NumberOfPagesPerGroup) * NumberOfPagesPerGroup) + 1;
				UpperPage = LowerPage + NumberOfPagesPerGroup - 1;

				if (UpperPage > Pages)
				{
					UpperPage = Pages;
				}
			}
		}

		/// <summary>
		/// Calculate the various paging values and apply them values to the list items
		/// </summary>
		public void CalculateAndApply()
		{
			Calculate();

			if (Count > 0 && Pages > 0)
			{
				var items = this.Skip(Skip).Take(Take).ToList();
				Clear();
				AddRange(items);
			}
		}
	}
}
