// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Linq;

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
	public class ImmutableList<T> : IImmutableList<T>
	{
		internal List<T> List { get; private init; }

		/// <inheritdoc/>
		public Option<T> this[int index] =>
			List.ElementAtOrNone(index);

		/// <inheritdoc/>
		public int Count =>
			List.Count;

		/// <summary>
		/// Create a new, empty <see cref="ImmutableList{T}"/>
		/// </summary>
		public ImmutableList() =>
			List = new();

		/// <summary>
		/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="collection"/>
		/// </summary>
		/// <param name="collection">Collection of items to add</param>
		public ImmutableList(IEnumerable<T> collection) =>
			List = new(collection);

		/// <inheritdoc/>
		public IEnumerable<T> AsEnumerable() =>
			ToList().AsEnumerable();

		/// <inheritdoc/>
		public IImmutableList<T> Clone() =>
			new ImmutableList<T>(List);

		/// <inheritdoc/>
		public T[] ToArray() =>
			ToList().ToArray();

		/// <inheritdoc/>
		public List<T> ToList() =>
			new(List);

		/// <inheritdoc/>
		public IImmutableList<T> With(T add) =>
			new ImmutableList<T>(new List<T>(List) { add });

		/// <inheritdoc/>
		public IImmutableList<T> WithRange(params T[] add)
		{
			var newList = new List<T>(List);
			newList.AddRange(add);

			return new ImmutableList<T>(newList);
		}

		/// <inheritdoc/>
		public IImmutableList<T> Filter(Func<T, bool> predicate) =>
			new ImmutableList<T>(List.Where(predicate));

		/// <inheritdoc/>
		public IEnumerator<T> GetEnumerator() =>
			List.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();
	}
}
