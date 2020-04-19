using System;
using System.Collections.Generic;
using System.Linq;
using D = Jeebs.Defaults.PagingValues;

namespace Jeebs
{
	/// <inheritdoc cref="IPagedList{T}"/>
	public sealed class PagedList<T> : List<T>, IPagedList<T>
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

		/// <inheritdoc/>
		public (IPagedList<T> list, IPagingValues values) CalculateAndApply(long page, long itemsPer = D.ItemsPer, long pagesPer = D.PagesPer)
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
