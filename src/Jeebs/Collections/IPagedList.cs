// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections;

/// <summary>
/// List that supports paging operations.
/// </summary>
/// <typeparam name="T">Type of objects in the list.</typeparam>
public interface IPagedList<T> : IImmutableList<T>
{
	/// <summary>
	/// IPagingValues used to create this list.
	/// </summary>
	IPagingValues Values { get; }
}
