// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Linq;

/// <summary>
/// Enumerable Extensions: LastOrNone
/// </summary>
public static class EnumerableExtensionsLastOrNone
{
	/// <inheritdoc cref="F.OptionF.Enumerable.LastOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Option<T> LastOrNone<T>(this IEnumerable<T> @this) =>
		F.OptionF.Enumerable.LastOrNone(@this, null);

	/// <inheritdoc cref="F.OptionF.Enumerable.LastOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Option<T> LastOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
		F.OptionF.Enumerable.LastOrNone(@this, predicate);
}
