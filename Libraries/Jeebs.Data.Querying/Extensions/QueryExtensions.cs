using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// <see cref="IQuery{T}"/> Extension methods
	/// </summary>
	public static class QueryExtensions
	{
		/// <summary>
		/// Returns the number of items to be retrieved by the current query parts
		/// </summary>
		/// <param name="this">Query</param>
		public static Task<IR<long>> GetCountAsync<T>(this IQuery<T> @this)
			=> @this.GetCountAsync(Result.Ok());

		/// <summary>
		/// Retrieves the items using the current query parts
		/// </summary>
		/// <param name="this">Query</param>
		public static Task<IR<List<T>>> ExecuteQueryAsync<T>(this IQuery<T> @this)
			=> @this.ExecuteQueryAsync(Result.Ok());

		/// <summary>
		/// Retrieves the items using the current query parts, using LIMIT / OFFSET to select only the items on a particular page
		/// </summary>
		/// <param name="this">Query</param>
		/// <param name="page">Current page number</param>
		public static Task<IR<PagedList<T>>> ExecuteQueryAsync<T>(this IQuery<T> @this, long page)
			=> @this.ExecuteQueryAsync(Result.Ok(), page);
	}
}
