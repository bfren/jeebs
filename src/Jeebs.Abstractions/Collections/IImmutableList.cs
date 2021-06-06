// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;

namespace Jeebs
{
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
		Option<T> this[int index] { get; }

		/// <summary>
		/// Return the number of items in this <see cref="IImmutableList{T}"/>
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Return the <see cref="IImmutableList{T}"/> as an enumerable
		/// </summary>
		IEnumerable<T> AsEnumerable();

		/// <summary>
		/// Clone the <see cref="IImmutableList{T}"/>
		/// </summary>
		IImmutableList<T> Clone();

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
		/// <param name="add">Item to add</param>
		IImmutableList<T> With(T add);

		///// <summary>
		///// Create a new <see cref="IImmutableList{T}"/> with the specified items added to the end
		///// </summary>
		///// <param name="collection">Collection of items to add</param>
		//IImmutableList<T> WithRange(IEnumerable<T> collection);

		/// <summary>
		/// Create a new <see cref="IImmutableList{T}"/> with the specified items added to the end
		/// </summary>
		/// <param name="collection">Collection of items to add</param>
		IImmutableList<T> WithRange(params T[] collection);

		/// <summary>
		/// Filter the list based on <paramref name="predicate"/>
		/// </summary>
		/// <param name="predicate">Item predicate - if true will be included</param>
		IImmutableList<T> Filter(Func<T, bool> predicate);
	}
}
