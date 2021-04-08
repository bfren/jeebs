// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

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
		public PagedList() =>
			Values = new PagingValues();

		/// <summary>
		/// Create PagedList from a collection of items
		/// </summary>
		/// <param name="values">PagingValues</param>
		/// <param name="collection">Collection</param>
		public PagedList(IPagingValues values, IEnumerable<T> collection) : base(collection) =>
			Values = values;
	}
}
