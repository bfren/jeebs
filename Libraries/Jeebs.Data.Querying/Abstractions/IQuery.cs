using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Performs queries - returning either the total number of matching rows, the results, or the paged results
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	public interface IQuery<T>
	{
		/// <summary>
		/// Returns the number of items to be retrieved by the current query parts
		/// </summary>
		/// <param name="r">Result object</param>
		public Task<IR<long>> GetCountAsync(IOk r);

		/// <summary>
		/// Retrieves the items using the current query parts
		/// </summary>
		/// <param name="r">Result object</param>
		public Task<IR<List<T>>> ExecuteQueryAsync(IOk r);

		/// <summary>
		/// Retrieves the items using the current query parts, using LIMIT / OFFSET to select only the items on a particular page
		/// </summary>
		/// <param name="r">Result object</param>
		/// <param name="page">Current page number</param>
		public Task<IR<PagedList<T>>> ExecuteQueryAsync(IOk r, long page);
	}
}
