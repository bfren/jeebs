// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// Enumerable Extensions: Filter
	/// </summary>
	public static class EnumerableExtensions_Filter
	{
		/// <inheritdoc cref="F.OptionF.Filter{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this) =>
			F.OptionF.Filter(@this, null);

		/// <inheritdoc cref="F.OptionF.Filter{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this, Func<T, bool> predicate) =>
			F.OptionF.Filter(@this, predicate);

		/// <inheritdoc cref="F.OptionF.Filter{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<U> Filter<T, U>(this IEnumerable<Option<T>> @this, Func<T, U> map) =>
			F.OptionF.Filter(@this, map, null);

		/// <inheritdoc cref="F.OptionF.Filter{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<U> Filter<T, U>(this IEnumerable<Option<T>> @this, Func<T, U> map, Func<T, bool> predicate) =>
			F.OptionF.Filter(@this, map, predicate);

		/// <inheritdoc cref="F.OptionF.Filter{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<Option<U>> Filter<T, U>(this IEnumerable<Option<T>> @this, Func<T, Option<U>> map) =>
			F.OptionF.Filter(@this, map, null);

		/// <inheritdoc cref="F.OptionF.Filter{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<Option<U>> Filter<T, U>(this IEnumerable<Option<T>> @this, Func<T, Option<U>> map, Func<T, bool> predicate) =>
			F.OptionF.Filter(@this, map, predicate);
	}
}
