// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using SCI = System.Collections.Immutable;

namespace Jeebs.Collections;

/// <inheritdoc cref="IImmutableDictionary{TKey, TValue}"/>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public record class ImmutableDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
	where TKey : notnull
{
	internal SCI.ImmutableDictionary<TKey, TValue> Dictionary { get; init; }

	/// <inheritdoc/>
	public Maybe<TValue> this[TKey key] =>
		Dictionary.TryGetValue(key, out var value) ? value : M.None;

	/// <inheritdoc/>
	public int Count =>
		Dictionary.Count;

	/// <summary>
	/// Create a new, empty <see cref="ImmutableDictionary{TKey, TValue}"/>.
	/// </summary>
	public ImmutableDictionary() :
		this([])
	{ }

	/// <summary>
	/// Create a new <see cref="ImmutableDictionary{TKey, TValue}"/> with the specified <paramref name="dictionary"/>.
	/// </summary>
	/// <param name="dictionary">Dictionary to add.</param>
	public ImmutableDictionary(IDictionary<TKey, TValue> dictionary) :
		this(SCI.ImmutableDictionary<TKey, TValue>.Empty.AddRange(dictionary))
	{ }

	/// <summary>
	/// Create a new <see cref="ImmutableDictionary{TKey, TValue}"/> with the specified <paramref name="list"/>.
	/// </summary>
	/// <param name="list">List to add.</param>
	public ImmutableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> list) :
		this(SCI.ImmutableDictionary<TKey, TValue>.Empty.AddRange(list))
	{ }

	internal ImmutableDictionary(SCI.ImmutableDictionary<TKey, TValue> dictionary) =>
		Dictionary = dictionary;

	/// <inheritdoc/>
	public IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable() =>
		Dictionary.AsEnumerable();

	/// <inheritdoc/>
	public KeyValuePair<TKey, TValue>[] ToArray() =>
		[.. Dictionary];

	/// <inheritdoc/>
	public List<KeyValuePair<TKey, TValue>> ToList() =>
		[.. Dictionary];

	/// <inheritdoc/>
	public IImmutableDictionary<TKey, TValue> WithItem(TKey keyToAdd, TValue valueToAdd) =>
		this with { Dictionary = Dictionary.Add(keyToAdd, valueToAdd) };

	/// <inheritdoc/>
	public IImmutableDictionary<TKey, TValue> WithoutItem(TKey keyToRemove) =>
		this with { Dictionary = Dictionary.Remove(keyToRemove) };

	/// <inheritdoc/>
	public IImmutableDictionary<TKey, TValue> WithRange(params KeyValuePair<TKey, TValue>[] itemsToAdd) =>
		this with { Dictionary = Dictionary.AddRange(itemsToAdd) };

	/// <inheritdoc/>
	public IImmutableDictionary<TKey, TValue> WithoutRange(params TKey[] itemsToRemove) =>
		this with { Dictionary = Dictionary.RemoveRange(itemsToRemove) };

	/// <inheritdoc/>
	public IImmutableDictionary<TKey, TValue> Replace(TKey remove, TKey addKey, TValue addValue) =>
		this with { Dictionary = Dictionary.Remove(remove).Add(addKey, addValue) };

	/// <inheritdoc/>
	public IImmutableDictionary<TKey, TValue> Filter(Func<TKey, TValue, bool> predicate) =>
		new ImmutableDictionary<TKey, TValue>(
			Dictionary.Where(x => predicate(x.Key, x.Value)).ToDictionary(x => x.Key, x => x.Value)
		);

	/// <inheritdoc/>
	public Dictionary<TKey, TValue> ToDictionary() =>
		Dictionary.ToDictionary();

	/// <inheritdoc/>
	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
		Dictionary.GetEnumerator();

	/// <summary>
	/// Compare sequences.
	/// </summary>
	/// <param name="other">Another list to compare with this one.</param>
	/// <returns>True if <see cref="this"/> and <paramref name="other"/> are equal.</returns>
	public virtual bool Equals(Dictionary<TKey, TValue>? other) =>
		other switch
		{
			ImmutableDictionary<TKey, TValue> x =>
				Dictionary.SequenceEqual(x.Dictionary),

			_ =>
				false
		};

	/// <inheritdoc/>
	public override int GetHashCode() =>
		Dictionary.GetHashCode();
}
