// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Linq;

/// <summary>
/// Enumerable Extensions: Filter
/// </summary>
public static class EnumerableExtensions_Filter
{
	/// <inheritdoc cref="F.OptionF.Enumerable.Filter{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
	public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this) =>
		F.OptionF.Enumerable.Filter(@this, null);

	/// <inheritdoc cref="F.OptionF.Enumerable.Filter{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
	public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this, Func<T, bool> predicate) =>
		F.OptionF.Enumerable.Filter(@this, predicate);
}
