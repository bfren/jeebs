using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs
{
	/// <summary>
	/// List that supports paging operations
	/// </summary>
	/// <typeparam name="T">Type of objects in the list</typeparam>
	public sealed class PagedList<T> : List<T>
	{
		/// <summary>
		/// The paging values
		/// </summary>
		public readonly PagingValues Values;

		/// <summary>
		/// Create PagedList from a collection of items
		/// </summary>
		/// <param name="collection">Collection</param>
		/// <param name="currentPage">Current page</param>
		/// <param name="itemsPerPage">[Optional] Number of items per page</param>
		/// <param name="numberOfPagesPerGroup">[Optional] Number of page numbers before using next / previous</param>
		public PagedList(IEnumerable<T> collection, long currentPage, long itemsPerPage = 10, long numberOfPagesPerGroup = 10)
			: base(collection)
		{
			Values = new PagingValues(currentPage, Count, itemsPerPage, numberOfPagesPerGroup);
		}

		/// <summary>
		/// Set required parameters and calculate values
		/// </summary>
		/// <param name="totalItems">Total number of items</param>
		/// <param name="currentPage">Current page</param>
		/// <param name="itemsPerPage">[Optional] Number of items per page</param>
		/// <param name="numberOfPagesPerGroup">[Optional] Number of page numbers before using next / previous</param>
		public PagedList(long totalItems, long currentPage, long itemsPerPage = 10, long numberOfPagesPerGroup = 10)
		{
			Values = new PagingValues(currentPage, totalItems, itemsPerPage, numberOfPagesPerGroup);
		}

		/// <summary>
		/// Calculate the various paging values and apply them values to the list items
		/// </summary>
		public PagedList<T> CalculateAndApply()
		{
			// Return empty list
			if (Count == 0 || Values.Pages == 0)
			{
				return new PagedList<T>(0, 0, 0, 0);
			}

			// Return new paged list with only the necessary items
			return new PagedList<T>(
				this.Skip(Values.Skip).Take(Values.Take),
				Values.CurrentPage,
				Values.ItemsPerPage,
				Values.NumberOfPagesPerGroup
			);
		}
	}
}
