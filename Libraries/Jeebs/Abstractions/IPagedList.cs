using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// List that supports paging operations
	/// </summary>
	/// <typeparam name="T">Type of objects in the list</typeparam>
	public interface IPagedList<T> : IList<T>
	{
		/// <summary>
		/// The page number to display
		/// </summary>
		long CurrentPage { get; }

		/// <summary>
		/// The total number of items that match the search
		/// </summary>
		long TotalItems { get; }

		/// <summary>
		/// The number of items to display on each page of results
		/// </summary>
		long ItemsPerPage { get; set; }

		/// <summary>
		/// The number of page numbers to display
		/// </summary>
		long NumberOfPagesPerGroup { get; set; }

		/// <summary>
		/// The index of the first item being displayed
		/// </summary>
		long FirstItem { get; }

		/// <summary>
		/// The number of items to Skip() in a Linq query
		/// </summary>
		int Skip { get; }

		/// <summary>
		/// The index + 1 of the last item being displayed
		/// </summary>
		long LastItem { get; }

		/// <summary>
		/// The number of items to Take() in a Linq query
		/// </summary>
		int Take { get; }

		/// <summary>
		/// The number of pages needed to display all the items
		/// </summary>
		long Pages { get; }

		/// <summary>
		/// The first page to display
		/// </summary>
		long LowerPage { get; }

		/// <summary>
		/// The last page to display
		/// </summary>
		long UpperPage { get; }

		/// <summary>
		/// Calculate the various paging values
		/// </summary>
		void Calculate();

		/// <summary>
		/// Calculate the various paging values and apply them values to the list items
		/// </summary>
		void CalculateAndApply();
	}
}
