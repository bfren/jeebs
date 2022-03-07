// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Linq;

/// <summary>
/// Enumerable Extensions: FirstOrNone
/// </summary>
public static class EnumerableExtensionsFirstOrNone
{
	/// <inheritdoc cref="F.MaybeF.Enumerable.FirstOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> @this) =>
		F.MaybeF.Enumerable.FirstOrNone(@this, null);

	/// <inheritdoc cref="F.MaybeF.Enumerable.FirstOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
		F.MaybeF.Enumerable.FirstOrNone(@this, predicate);
}
