using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Query interface
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	public interface IQuery<T>
	{
		/// <summary>
		/// Returns the number of items to be retrieved by the current query parts
		/// </summary>
		public Task<IResult<long>> GetCount();

		/// <summary>
		/// Retrieves the items using the current query parts
		/// </summary>
		public Task<IResult<IEnumerable<T>>> ExecuteQuery();

		/// <summary>
		/// Retrieves the items using the current query parts, using LIMIT / OFFSET to select only the items on a particular page
		/// </summary>
		/// <param name="page">Current page number</param>
		public Task<IResult<IPagedList<T>>> ExecuteQuery(long page);
	}
}
