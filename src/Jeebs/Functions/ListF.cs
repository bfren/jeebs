// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs.Collections;

namespace Jeebs.Functions;

/// <summary>
/// Alternative methods for creating and manipulating <see cref="ImmutableList{T}"/> objects.
/// </summary>
public static class ListF
{
	/// <summary>
	/// Create an empty <see cref="ImmutableList{T}"/>.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	public static ImmutableList<T> Empty<T>() =>
		new();

	/// <summary>
	/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="items"/>.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="items">Collection of items to add.</param>
	public static ImmutableList<T> Create<T>(IEnumerable<T> items) =>
		new(items);

	/// <summary>
	/// Create a new <see cref="ImmutableList{T}"/> with the specified <paramref name="args"/>.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="args">Items to add.</param>
	public static ImmutableList<T> Create<T>(params T[]? args) =>
		args switch
		{
			T[] =>
				new(args),

			_ =>
				new()
		};

	/// <summary>
	/// Deserialise a JSON list into an ImmutableList.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="json">JSON list.</param>
	public static ImmutableList<T> Deserialise<T>(string json) =>
		JsonF.Deserialise<List<T>>(json)
			.Match(
				ok: x => Create(items: x),
				fail: _ => new()
			);

	/// <summary>
	/// Merge multiple <see cref="ImmutableList{T}"/> objects into one.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="lists">Lists to merge.</param>
	public static ImmutableList<T> Merge<T>(params IImmutableList<T>[] lists) =>
		new(lists.SelectMany(x => x));
}
