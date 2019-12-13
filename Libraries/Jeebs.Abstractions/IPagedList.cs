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
		double CurrentPage { get; }

		/// <summary>
		/// The total number of items that match the search
		/// </summary>
		double TotalItems { get; }

		/// <summary>
		/// The number of items to display on each page of results
		/// </summary>
		double ItemsPerPage { get; set; }

		/// <summary>
		/// The number of page numbers to display
		/// </summary>
		double NumberOfPagesPerGroup { get; set; }

		/// <summary>
		/// The index of the first item being displayed
		/// </summary>
		double FirstItem { get; }

		/// <summary>
		/// The number of items to Skip() in a Linq query
		/// </summary>
		int Skip { get; }

		/// <summary>
		/// The index + 1 of the last item being displayed
		/// </summary>
		double LastItem { get; }

		/// <summary>
		/// The number of items to Take() in a Linq query
		/// </summary>
		int Take { get; }

		/// <summary>
		/// The number of pages needed to display all the items
		/// </summary>
		double Pages { get; }

		/// <summary>
		/// The first page to display
		/// </summary>
		double LowerPage { get; }

		/// <summary>
		/// The last page to display
		/// </summary>
		double UpperPage { get; }

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
