using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D = Jeebs.Defaults.PagingValues;

namespace Jeebs
{
	/// <summary>
	/// IEnumerable Extensions - ToPagedList
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Convert a collection to a paged list
		/// </summary>
		/// <typeparam name="T">Item type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="page">Current page</param>
		/// <param name="itemsPer">Items per page</param>
		/// <param name="pagesPer">Pages per screen</param>
		public static IPagedList<T> ToPagedList<T>(
			this IEnumerable<T> @this,
			long page,
			long itemsPer = D.ItemsPer,
			long pagesPer = D.PagesPer
		)
		{
			// Return empty list
			if (!@this.Any())
			{
				return new PagedList<T>();
			}

			// Calculate values and get items
			var values = new PagingValues(items: @this.Count(), page: page, itemsPer: itemsPer, pagesPer: pagesPer);
			var items = @this.Skip(values.Skip).Take(values.Take);

			// Create new paged list with only the necessary items
			return new PagedList<T>(values, items);
		}
	}
}
