// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Linq;

/// <summary>
/// Enumerable Extensions: FilterMap
/// </summary>
public static class EnumerableExtensionsFilterMap
{
	/// <inheritdoc cref="F.MaybeF.Enumerable.FilterMap{T, U}(IEnumerable{Maybe{T}}, Func{T, U}, Func{T, bool}?)"/>
	public static IEnumerable<U> FilterMap<T, U>(this IEnumerable<Maybe<T>> @this, Func<T, U> map) =>
		F.MaybeF.Enumerable.FilterMap(@this, map, null);

	/// <inheritdoc cref="F.MaybeF.Enumerable.FilterMap{T, U}(IEnumerable{Maybe{T}}, Func{T, U}, Func{T, bool}?)"/>
	public static IEnumerable<U> FilterMap<T, U>(this IEnumerable<Maybe<T>> @this, Func<T, U> map, Func<T, bool> predicate) =>
		F.MaybeF.Enumerable.FilterMap(@this, map, predicate);

	/// <inheritdoc cref="F.MaybeF.Enumerable.FilterMap{T, U}(IEnumerable{Maybe{T}}, Func{T, U}, Func{T, bool}?)"/>
	public static IEnumerable<Maybe<U>> FilterMap<T, U>(this IEnumerable<Maybe<T>> @this, Func<T, Maybe<U>> map) =>
		F.MaybeF.Enumerable.FilterMap(@this, map, null);

	/// <inheritdoc cref="F.MaybeF.Enumerable.FilterMap{T, U}(IEnumerable{Maybe{T}}, Func{T, U}, Func{T, bool}?)"/>
	public static IEnumerable<Maybe<U>> FilterMap<T, U>(this IEnumerable<Maybe<T>> @this, Func<T, Maybe<U>> map, Func<T, bool> predicate) =>
		F.MaybeF.Enumerable.FilterMap(@this, map, predicate);
}
