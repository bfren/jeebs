// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections;

/// <summary>
/// Extension methods for <see cref="PagedList{T}"/> objects.
/// </summary>
public static class PagedListExtensions
{
	/// <summary>
	/// Modify 'pages per' in pagination (the other paging values cannot be changed).
	/// </summary>
	/// <typeparam name="T">List item type.</typeparam>
	/// <param name="this">IPagedList.</param>
	/// <param name="pagesPer">The new number of pages per pagination.</param>
	/// <returns>New <see cref="IPagedList{T}"/> with updated pages per screen value.</returns>
	public static IPagedList<T> WithPagesPer<T>(this IPagedList<T> @this, ulong pagesPer) =>
		new PagedList<T>(new PagingValues(@this.Values.Items, @this.Values.Page, @this.Values.ItemsPer, pagesPer), @this);
}
