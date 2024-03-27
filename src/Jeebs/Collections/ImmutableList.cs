// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SCI = System.Collections.Immutable;

namespace Jeebs.Collections;

/// <inheritdoc cref="IImmutableList{T}"/>
public record class ImmutableList<T> : IImmutableList<T>, IEquatable<ImmutableList<T>>
{
	internal SCI.ImmutableList<T> List { get; init; }

	/// <inheritdoc/>
	public Maybe<T> this[int index] =>
		List.ElementAtOrNone(index);

	/// <inheritdoc/>
	public int Count =>
		List.Count;

	/// <summary>
	/// Create a new, empty <see cref="ImmutableList{T}"/>.
	/// </summary>
	public ImmutableList() : this(SCI.ImmutableList<T>.Empty) { }

	/// <summary>
	/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="collection"/>.
	/// </summary>
	/// <param name="collection">Collection of items to add.</param>
	public ImmutableList(IEnumerable<T> collection) : this(SCI.ImmutableList<T>.Empty.AddRange(collection)) { }

	internal ImmutableList(SCI.ImmutableList<T> list) =>
		List = list;

	/// <inheritdoc/>
	public IEnumerable<T> AsEnumerable() =>
		List.AsEnumerable();

	/// <inheritdoc/>
	public T[] ToArray() =>
		[.. List];

	/// <inheritdoc/>
	public List<T> ToList() =>
		[.. List];

	/// <inheritdoc/>
	public IImmutableList<T> WithItem(T itemToAdd) =>
		this with { List = List.Add(itemToAdd) };

	/// <inheritdoc/>
	public IImmutableList<T> WithoutItem(T itemToRemove) =>
		this with { List = List.Remove(itemToRemove) };

	/// <inheritdoc/>
	public IImmutableList<T> WithRange(params T[] itemsToAdd) =>
		this with { List = List.AddRange(itemsToAdd) };

	/// <inheritdoc/>
	public IImmutableList<T> WithoutRange(params T[] itemsToRemove) =>
		this with { List = List.RemoveRange(itemsToRemove) };

	/// <inheritdoc/>
	public IImmutableList<T> Replace(T remove, T add) =>
		this with { List = List.Replace(remove, add) };

	/// <inheritdoc/>
	public IImmutableList<T> Filter(Func<T, bool> predicate) =>
		new ImmutableList<T>(List.Where(predicate));

	/// <inheritdoc/>
	public IEnumerator<T> GetEnumerator() =>
		List.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();

	/// <summary>
	/// Compare sequences.
	/// </summary>
	/// <param name="other">Another list to compare with this one.</param>
	/// <returns>True if <see cref="this"/> and <paramref name="other"/> are equal.</returns>
	public virtual bool Equals(ImmutableList<T>? other) =>
		other switch
		{
			ImmutableList<T> x =>
				List.SequenceEqual(x.List),

			_ =>
				false
		};

	/// <inheritdoc/>
	public override int GetHashCode() =>
		List.GetHashCode();
}
