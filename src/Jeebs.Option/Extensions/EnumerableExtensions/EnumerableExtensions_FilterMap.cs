// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Linq
{
	/// <summary>
	/// Enumerable Extensions: FilterMap
	/// </summary>
	public static class EnumerableExtensions_FilterMap
	{
		/// <inheritdoc cref="F.OptionF.Enumerable.FilterMap{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<U> FilterMap<T, U>(this IEnumerable<Option<T>> @this, Func<T, U> map) =>
			F.OptionF.Enumerable.FilterMap(@this, map, null);

		/// <inheritdoc cref="F.OptionF.Enumerable.FilterMap{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<U> FilterMap<T, U>(this IEnumerable<Option<T>> @this, Func<T, U> map, Func<T, bool> predicate) =>
			F.OptionF.Enumerable.FilterMap(@this, map, predicate);

		/// <inheritdoc cref="F.OptionF.Enumerable.FilterMap{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<Option<U>> FilterMap<T, U>(this IEnumerable<Option<T>> @this, Func<T, Option<U>> map) =>
			F.OptionF.Enumerable.FilterMap(@this, map, null);

		/// <inheritdoc cref="F.OptionF.Enumerable.FilterMap{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<Option<U>> FilterMap<T, U>(this IEnumerable<Option<T>> @this, Func<T, Option<U>> map, Func<T, bool> predicate) =>
			F.OptionF.Enumerable.FilterMap(@this, map, predicate);
	}
}
