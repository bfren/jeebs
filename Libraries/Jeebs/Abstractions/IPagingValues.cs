// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// Paging Values interface
	/// </summary>
	public interface IPagingValues
	{
		/// <summary>
		/// The total number of items that match the search
		/// </summary>
		long Items { get; }

		/// <summary>
		/// The number of items to display on each page of results
		/// </summary>
		long ItemsPer { get; }

		/// <summary>
		/// The index of the first item being displayed
		/// </summary>
		long FirstItem { get; }

		/// <summary>
		/// The index + 1 of the last item being displayed
		/// </summary>
		long LastItem { get; }

		/// <summary>
		/// The page number to display
		/// </summary>
		long Page { get; }

		/// <summary>
		/// The number of pages needed to display all the items
		/// </summary>
		long Pages { get; }

		/// <summary>
		/// The number of pages per group of page numbers
		/// </summary>
		long PagesPer { get; }

		/// <summary>
		/// The first page to display
		/// </summary>
		long LowerPage { get; }

		/// <summary>
		/// The last page to display
		/// </summary>
		long UpperPage { get; }

		/// <summary>
		/// The number of items to Skip() in a Linq query
		/// </summary>
		int Skip { get; }

		/// <summary>
		/// The number of items to Take() in a Linq query
		/// </summary>
		int Take { get; }
	}
}
