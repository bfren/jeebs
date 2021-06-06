// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jeebs.WordPress.Data.Querying
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
		Task<Option<long>> GetCountAsync();

		/// <summary>
		/// Retrieves the items using the current query parts
		/// </summary>
		Task<Option<List<T>>> ExecuteQueryAsync();

		/// <summary>
		/// Retrieves the items using the current query parts, using LIMIT / OFFSET to select only the items on a particular page
		/// </summary>
		/// <param name="page">Current page number</param>
		Task<Option<IPagedList<T>>> ExecuteQueryAsync(long page);

		/// <summary>
		/// Executes the query and returns a scalar value
		/// </summary>
		Task<Option<T>> ExecuteScalarAsync();
	}
}
