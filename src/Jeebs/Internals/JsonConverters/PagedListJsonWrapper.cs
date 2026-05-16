// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters;

/// <summary>
/// Wrapper for serialising and deserialising <see cref="IPagedList{T}"/> and <see cref="PagedList{T}"/> objects.
/// </summary>
/// <typeparam name="T">List item type.</typeparam>
/// <param name="Values">Paging values.</param>
/// <param name="Items">List of items.</param>
internal readonly record struct PagedListJsonWrapper<T>(PagingValues Values, IEnumerable<T> Items);
