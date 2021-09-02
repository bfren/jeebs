// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs
{
	/// <inheritdoc cref="IPagedList{T}"/>
	public sealed record class PagedList<T> : ImmutableList<T>, IPagedList<T>
	{
		/// <inheritdoc/>
		public IPagingValues Values { get; init; }

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
