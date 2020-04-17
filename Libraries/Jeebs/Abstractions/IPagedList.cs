using System;
using System.Collections.Generic;
using System.Text;
using D = Jeebs.Defaults.PagingValues;

namespace Jeebs
{
	public interface IPagedList<T> : IList<T>
	{
		/// <summary>
		/// Apply paging values to the list items and return a new PagedList
		/// </summary>
		/// <param name="page">Current page</param>
		/// <param name="itemsPer">[Optional] Number of items per page</param>
		/// <param name="pagesPer">[Optional] Number of page numbers before using next / previous</param>
		public (IPagedList<T> list, IPagingValues values) CalculateAndApply(long page, long itemsPer = D.ItemsPer, long pagesPer = D.PagesPer);
	}
}
