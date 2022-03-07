// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Linq;

/// <summary>
/// Enumerable Extensions: Filter
/// </summary>
public static class EnumerableExtensionsFilter
{
	/// <inheritdoc cref="F.MaybeF.Enumerable.Filter{T}(IEnumerable{Maybe{T}}, Func{T, bool}?)"/>
	public static IEnumerable<T> Filter<T>(this IEnumerable<Maybe<T>> @this) =>
		F.MaybeF.Enumerable.Filter(@this, null);

	/// <inheritdoc cref="F.MaybeF.Enumerable.Filter{T}(IEnumerable{Maybe{T}}, Func{T, bool}?)"/>
	public static IEnumerable<T> Filter<T>(this IEnumerable<Maybe<T>> @this, Func<T, bool> predicate) =>
		F.MaybeF.Enumerable.Filter(@this, predicate);
}
