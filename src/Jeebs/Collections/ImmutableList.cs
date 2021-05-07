// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		public static ImmutableList<T> Create<T>() =>
			new();

		/// <summary>
		/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="collection"/>
		/// </summary>
		/// <typeparam name="T">List Item type</typeparam>
		/// <param name="collection">Collection of items to add</param>
		public static ImmutableList<T> Create<T>(IEnumerable<T> collection) =>
			new(collection);
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

		///// <inheritdoc/>
		//public IImmutableList<T> WithRange(IEnumerable<T> add)
		//{
		//	var newList = new List<T>(List);
		//	newList.AddRange(add);

		//	return new ImmutableList<T>(newList);
		//}

		/// <inheritdoc/>
		public IImmutableList<T> WithRange(params T[] add)
		{
			var newList = new List<T>(List);
			newList.AddRange(add);

			return new ImmutableList<T>(newList);
		}

		/// <inheritdoc/>
		public IEnumerator<T> GetEnumerator() =>
			List.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();
	}
}
