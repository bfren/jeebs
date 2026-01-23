// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Collections;

/// <summary>
/// Immutable Dictionary.
/// </summary>
/// <typeparam name="TKey">Key type.</typeparam>
/// <typeparam name="TValue">Value type.</typeparam>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public interface IImmutableDictionary<TKey, TValue>
	where TKey : notnull
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
{
	/// <summary>
	/// Get the item at the specified index.
	/// </summary>
	/// <param name="index">Item index.</param>
	Maybe<TValue> this[TKey key] { get; }

	/// <summary>
	/// Return the number of items in this <see cref="IImmutableList{T}"/>.
	/// </summary>
	int Count { get; }

	/// <summary>
	/// Return the <see cref="IImmutableList{T}"/> as an enumerable.
	/// </summary>
	IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable();

	/// <summary>
	/// Return the <see cref="IImmutableList{T}"/> as an array.
	/// </summary>
	KeyValuePair<TKey, TValue>[] ToArray();

	/// <summary>
	/// Return the <see cref="IImmutableList{T}"/> as an ordinary list.
	/// </summary>
	List<KeyValuePair<TKey, TValue>> ToList();

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> with the specified item added to the end.
	/// </summary>
	/// <param name="itemToAdd">Item to add.</param>
	IImmutableDictionary<TKey, TValue> WithItem(TKey keyToAdd, TValue valueToAdd);

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> without the specified item.
	/// </summary>
	/// <param name="itemToRemove">Item to remove.</param>
	IImmutableDictionary<TKey, TValue> WithoutItem(TKey keyToRemove);

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> with the specified items added to the end.
	/// </summary>
	/// <param name="itemsToAdd">Collection of items to add.</param>
	IImmutableDictionary<TKey, TValue> WithRange(params KeyValuePair<TKey, TValue>[] itemsToAdd);

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> with the specified items removed.
	/// </summary>
	/// <param name="itemsToRemove">Collection of items to remove.</param>
	IImmutableDictionary<TKey, TValue> WithoutRange(params TKey[] itemsToRemove);

	/// <summary>
	/// Create a new <see cref="IImmutableList{T}"/> with one item replaced by another.
	/// </summary>
	/// <param name="remove">Item to remove.</param>
	/// <param name="add">Item to add.</param>
	IImmutableDictionary<TKey, TValue> Replace(TKey remove, TKey addKey, TValue addValue);

	/// <summary>
	/// Filter the list based on <paramref name="predicate"/>.
	/// </summary>
	/// <param name="predicate">Item predicate - if true will be included.</param>
	IImmutableDictionary<TKey, TValue> Filter(Func<TKey, TValue, bool> predicate);

	/// <summary>
	/// Convert to a standard dictionary.
	/// </summary>
	Dictionary<TKey, TValue> ToDictionary();
}
