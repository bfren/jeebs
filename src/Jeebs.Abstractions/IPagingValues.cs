// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		ulong Items { get; }

		/// <summary>
		/// The number of items to display on each page of results
		/// </summary>
		ulong ItemsPer { get; }

		/// <summary>
		/// The index of the first item being displayed
		/// </summary>
		ulong FirstItem { get; }

		/// <summary>
		/// The index + 1 of the last item being displayed
		/// </summary>
		ulong LastItem { get; }

		/// <summary>
		/// The page number to display
		/// </summary>
		ulong Page { get; }

		/// <summary>
		/// The number of pages needed to display all the items
		/// </summary>
		ulong Pages { get; }

		/// <summary>
		/// The number of pages per group of page numbers
		/// </summary>
		ulong PagesPer { get; }

		/// <summary>
		/// The first page to display
		/// </summary>
		ulong LowerPage { get; }

		/// <summary>
		/// The last page to display
		/// </summary>
		ulong UpperPage { get; }

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
