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
		/// Filter out null items from a list
		/// </summary>
		/// <typeparam name="T">List value type</typeparam>
		/// <param name="this">List</param>
		public static IEnumerable<T> Filter<T>(this IEnumerable<T> @this) =>
			@this.Where(x => x is T);

		/// <summary>
		/// Like <see cref="Filter{T}(IEnumerable{T})"/>, but removes values unless they are <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">List</param>
		public static IEnumerable<Option<T>> Filter<T>(this IEnumerable<Option<T>> @this) =>
			@this.Where(x => x is Some<T>);

		/// <summary>
		/// Filter out null items (and empty / whitespace strings) from a list
		/// </summary>
		/// <typeparam name="T">Input value type</typeparam>
		/// <typeparam name="U">Output value type</typeparam>
		/// <param name="this">List</param>
		/// <param name="map">Mapping function</param>
		public static IEnumerable<U> Filter<T, U>(this IEnumerable<T> @this, Func<T, U?> map)
			where U : class
		{
			foreach (var x in @this)
			{
				if (map(x) is U y)
				{
					if (y is string z)
					{
						if (!string.IsNullOrWhiteSpace(z))
						{
							yield return y;
						}
					}
					else
					{
						yield return y;
					}
				}
			}
		}

		/// <summary>
		/// Filter out null items from a list
		/// </summary>
		/// <typeparam name="T">Input value type</typeparam>
		/// <typeparam name="U">Output value type</typeparam>
		/// <param name="this">List</param>
		/// <param name="map">Mapping function</param>
		public static IEnumerable<U> Filter<T, U>(this IEnumerable<T> @this, Func<T, U?> map)
			where U : struct
		{
			foreach (var x in @this)
			{
				if (map(x) is U y)
				{
					yield return y;
				}
			}
		}

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
