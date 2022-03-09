// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Maybe;

namespace Jeebs.Collections;

/// <summary>
/// Immutable List
/// </summary>
/// <typeparam name="T">List Item type</typeparam>
public interface IImmutableList<T> : IEnumerable<T>
{
	/// <summary>
	/// Get the item at the specified index
	/// </summary>
	/// <param name="index">Item index</param>
	Maybe<T> this[int index] { get; }

	/// <summary>
	/// Return the number of items in this <see cref="IImmutableList{T}"/>
	/// </summary>
	int Count { get; }

	/// <summary>
	/// Return the <see cref="IImmutableList{T}"/> as an enumerable
	/// </summary>
	IEnumerable<T> AsEnumerable();

	/// <summary>
	/// Return the <see cref="IImmutableList{T}"/> as an array
	/// </summary>
	T[] ToArray();

	/// <summary>
	/// Return the <see cref="IImmutableList{T}"/> as an ordinary list
	/// </summary>
	List<T> ToList();

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> with the specified item added to the end
	/// </summary>
	/// <param name="itemToAdd">Item to add</param>
	IImmutableList<T> WithItem(T itemToAdd);

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> without the specified item
	/// </summary>
	/// <param name="itemToRemove">Item to remove</param>
	IImmutableList<T> WithoutItem(T itemToRemove);

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> with the specified items added to the end
	/// </summary>
	/// <param name="itemsToAdd">Collection of items to add</param>
	IImmutableList<T> WithRange(params T[] itemsToAdd);

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> with the specified items removed
	/// </summary>
	/// <param name="itemsToRemove">Collection of items to remove</param>
	IImmutableList<T> WithoutRange(params T[] itemsToRemove);

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> with one item replaced by another
	/// </summary>
	/// <param name="remove">Item to remove</param>
	/// <param name="add">Item to add</param>
	IImmutableList<T> Replace(T remove, T add);

	/// <summary>
	/// Filter the list based on <paramref name="predicate"/>
	/// </summary>
	/// <param name="predicate">Item predicate - if true will be included</param>
	IImmutableList<T> Filter(Func<T, bool> predicate);
}
