using System;
using System.Collections.Generic;
using System.Linq;
using D = Jeebs.Defaults.PagingValues;

namespace Jeebs
{
	/// <inheritdoc cref="IPagedList{T}"/>
	public sealed class PagedList<T> : List<T>, IPagedList<T>
	{
		/// <inheritdoc/>
		public IPagingValues Values { get; }

		/// <summary>
		/// Create an empty PagedList
		/// </summary>
		public PagedList()
			=> Values = new PagingValues();

		/// <summary>
		/// Create PagedList from a collection of items
		/// </summary>
		/// <param name="values">PagingValues</param>
		/// <param name="collection">Collection</param>
		public PagedList(IPagingValues values, IEnumerable<T> collection) : base(collection)
			=> Values = values;
	}
}
