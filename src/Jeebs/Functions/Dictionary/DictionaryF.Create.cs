// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;

namespace Jeebs.Functions;

public static partial class DictionaryF
{
	/// <summary>
	/// Create a new <see cref="ImmutableDictionary{TKey, TValue}"/> with the specified <paramref name="items"/>.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="items">Collection of items to add.</param>
	/// <returns>New <see cref="ImmutableDictionary{TKey, TValue}"/> containing <paramref name="items"/>.</returns>
	public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>(IDictionary<TKey, TValue> items)
		where TKey : notnull =>
		new(items);

	/// <summary>
	/// Create a new <see cref="ImmutableDictionary{TKey, TValue}"/> with the specified <paramref name="args"/>.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="args">Items to add.</param>
	/// <returns>New <see cref="ImmutableDictionary{TKey, TValue}"/> containing <paramref name="args"/>.</returns>
	public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>(params KeyValuePair<TKey, TValue>[]? args)
		where TKey : notnull =>
		args switch
		{
			{ } =>
				new(args),

			_ =>
				new()
		};
}
