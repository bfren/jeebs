// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Linq;

/// <summary>
/// Enumerable Extensions: ElementAtOrNone
/// </summary>
public static class EnumerableExtensionsElementAtOrNone
{
	/// <inheritdoc cref="F.MaybeF.Enumerable.ElementAtOrNone{T}(IEnumerable{T}, int)"/>
	public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> @this, int index) =>
		F.MaybeF.Enumerable.ElementAtOrNone(@this, index);
}
