// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Linq;

/// <summary>
/// Enumerable Extensions: SingleOrNone
/// </summary>
public static class EnumerableExtensionsSingleOrNone
{
	/// <inheritdoc cref="F.MaybeF.Enumerable.SingleOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> @this) =>
		F.MaybeF.Enumerable.SingleOrNone(@this, null);

	/// <inheritdoc cref="F.MaybeF.Enumerable.SingleOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
		F.MaybeF.Enumerable.SingleOrNone(@this, predicate);
}
