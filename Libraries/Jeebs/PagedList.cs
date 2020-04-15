using System;
using System.Collections.Generic;
using System.Linq;
using D = Jeebs.PagingValues.Defaults;

namespace Jeebs
{
	/// <summary>
	/// List that supports paging operations
	/// </summary>
	/// <typeparam name="T">Type of objects in the list</typeparam>
	public sealed class PagedList<T> : List<T>
	{
		/// <summary>
		/// Create empty PagedList
		/// </summary>
		public PagedList() { }

		/// <summary>
		/// Create PagedList from a collection of items
		/// </summary>
		/// <param name="collection">Collection</param>
		public PagedList(IEnumerable<T> collection) : base(collection) { }

		/// <summary>
		/// Apply paging values to the list items and return a new PagedList
		/// </summary>
		/// <param name="page">Current page</param>
		/// <param name="itemsPer">[Optional] Number of items per page</param>
		/// <param name="pagesPer">[Optional] Number of page numbers before using next / previous</param>
		public (PagedList<T> list, PagingValues values) CalculateAndApply(long page, long itemsPer = D.ItemsPer, long pagesPer = D.PagesPer)
		{
			// Return empty list
			if (Count == 0)
			{
				return (new PagedList<T>(), new PagingValues(0, 0));
			}

			// Calculate values
			var values = new PagingValues(items: Count, page: page, itemsPer: itemsPer, pagesPer: pagesPer);

			// Create new paged list with only the necessary items
			var newList = new PagedList<T>(this.Skip(values.Skip).Take(values.Take));

			return (newList, new PagingValues(items: newList.Count, page: page, itemsPer: itemsPer, pagesPer: pagesPer));
		}
	}
}
