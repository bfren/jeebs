// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;

namespace Jeebs.Functions;

public static partial class ListF
{
	/// <summary>
	/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="items"/>.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="items">Collection of items to add.</param>
	/// <returns>New <see cref="ImmutableList{T}"/> containing <paramref name="items"/>.</returns>
	public static ImmutableList<T> Create<T>(IEnumerable<T> items) =>
		new(items);

	/// <summary>
	/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="args"/>.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="args">Items to add.</param>
	/// <returns>New <see cref="ImmutableList{T}"/> containing <paramref name="args"/>.</returns>
	public static ImmutableList<T> Create<T>(params T[]? args) =>
		args switch
		{
			T[] =>
				new(args),

			_ =>
				new()
		};
}
