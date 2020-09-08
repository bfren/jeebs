using System;
using System.Collections.Generic;
using System.Text;
using D = Jeebs.Defaults.PagingValues;

namespace Jeebs
{
	/// <summary>
	/// List that supports paging operations
	/// </summary>
	/// <typeparam name="T">Type of objects in the list</typeparam>
	public interface IPagedList<T> : IList<T>
	{
		/// <summary>
		/// IPagingValues used to create this list
		/// </summary>
		IPagingValues Values { get; }
	}
}
