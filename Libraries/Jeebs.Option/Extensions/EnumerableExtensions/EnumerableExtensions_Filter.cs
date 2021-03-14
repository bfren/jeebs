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
		/// <inheritdoc cref="F.OptionF.Enumerable.Filter{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this) =>
			F.OptionF.Enumerable.Filter(@this, null);

		/// <inheritdoc cref="F.OptionF.Enumerable.Filter{T, U}(IEnumerable{Option{T}}, Func{T, U}, Func{T, bool}?)"/>
		public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this, Func<T, bool> predicate) =>
			F.OptionF.Enumerable.Filter(@this, predicate);
	}
}
