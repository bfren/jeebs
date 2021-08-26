﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections;
using Jeebs.Linq;
using Sys = System.Collections.Immutable;

namespace Jeebs
{
	/// <summary>
	/// Alternative methods for creating an <see cref="ImmutableList{T}"/>
	/// </summary>
	public static class ImmutableList
	{
		/// <summary>
		/// Create an empty <see cref="ImmutableList{T}"/>
		/// </summary>
		/// <typeparam name="T">List Item type</typeparam>
		public static ImmutableList<T> Empty<T>() =>
			new();

		/// <summary>
		/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="items"/>
		/// </summary>
		/// <typeparam name="T">List Item type</typeparam>
		/// <param name="items">Collection of items to add</param>
		public static ImmutableList<T> Create<T>(IEnumerable<T> items) =>
			new(items);

		/// <summary>
		/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="args"/>
		/// </summary>
		/// <typeparam name="T">List Item type</typeparam>
		/// <param name="args">Items to add</param>
		public static ImmutableList<T> Create<T>(params T[] args) =>
			new(args);

		/// <summary>
		/// Merge multiple <see cref="ImmutableList{T}"/> objects into one
		/// </summary>
		/// <typeparam name="T">List Item type</typeparam>
		/// <param name="lists">Lists to merge</param>
		public static ImmutableList<T> Merge<T>(params IImmutableList<T>[] lists) =>
			new(lists.SelectMany(x => x));
	}

	/// <inheritdoc cref="IImmutableList{T}"/>
	public record class ImmutableList<T> : IImmutableList<T>, IEquatable<ImmutableList<T>>
	{
		internal Sys.ImmutableList<T> List { get; init; }

		/// <inheritdoc/>
		public Option<T> this[int index] =>
			List.ElementAtOrNone(index);

		/// <inheritdoc/>
		public int Count =>
			List.Count;

		/// <summary>
		/// Create a new, empty <see cref="ImmutableList{T}"/>
		/// </summary>
		public ImmutableList() : this(Sys.ImmutableList<T>.Empty) { }

		/// <summary>
		/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="collection"/>
		/// </summary>
		/// <param name="collection">Collection of items to add</param>
		public ImmutableList(IEnumerable<T> collection) : this(Sys.ImmutableList<T>.Empty.AddRange(collection)) { }

		internal ImmutableList(Sys.ImmutableList<T> list) =>
			List = list;

		/// <inheritdoc/>
		public IEnumerable<T> AsEnumerable() =>
			List.AsEnumerable();

		/// <inheritdoc/>
		public T[] ToArray() =>
			List.ToArray();

		/// <inheritdoc/>
		public List<T> ToList() =>
			List.ToList();

		/// <inheritdoc/>
		public IImmutableList<T> With(T add) =>
			this with { List = List.Add(add) };

		/// <inheritdoc/>
		public IImmutableList<T> Without(T remove) =>
			this with { List = List.Remove(remove) };

		/// <inheritdoc/>
		public IImmutableList<T> WithRange(params T[] add) =>
			this with { List = List.AddRange(add) };

		/// <inheritdoc/>
		public IImmutableList<T> WithoutRange(params T[] remove) =>
			this with { List = List.RemoveRange(remove) };

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
		/// Compare sequences
		/// </summary>
		/// <param name="other">Another list to compare with this one</param>
		public virtual bool Equals(ImmutableList<T>? other) =>
			other switch
			{
				ImmutableList<T> x =>
					List.SequenceEqual(x.List),

				_ =>
					false
			};

		/// <summary>
		/// Return list hash code
		/// </summary>
		public override int GetHashCode() =>
			List.GetHashCode();
	}
}
