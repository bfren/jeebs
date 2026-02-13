// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Collections;

public static partial class EnumerableExtensions
{
	/// <summary>
	/// Convert a collection to an immutable list.
	/// </summary>
	/// <typeparam name="T">Item type.</typeparam>
	/// <param name="this">Collection.</param>
	/// <returns>Immutable List.</returns>
	public static IImmutableList<T> ToImmutableList<T>(this IEnumerable<T> @this) =>
		new ImmutableList<T>(@this);
}
