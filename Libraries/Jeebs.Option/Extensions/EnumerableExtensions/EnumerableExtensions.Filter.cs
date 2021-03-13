// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;

namespace Jeebs
{
	public static class EnumerableExtensions_Filter
	{
		/// <summary>
		/// Filter elements to return only the values of those that are <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		internal static IEnumerable<T> DoFilter<T>(IEnumerable<Option<T>> @this, Func<T, bool>? predicate)
		{
			foreach (var option in @this)
			{
				foreach (var some in option)
				{
					if (predicate is null || predicate(some))
					{
						yield return some;
					}
				}
			}
		}

		/// <inheritdoc cref="DoFilter{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this) =>
			DoFilter(@this, null);

		/// <inheritdoc cref="DoFilter{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this, Func<T, bool> predicate) =>
			DoFilter(@this, predicate);

		/// <summary>
		/// Filter elements to return only <see cref="Some{T}"/> and transform using <paramref name="map"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="this">Option list</param>
		/// <param name="map">Mapping function</param>
		internal static IEnumerable<U> DoFilter<T, U>(IEnumerable<Option<T>> @this, Func<T, U> map)
		{
			foreach (var some in DoFilter(@this, null))
			{
				yield return map(some);
			}
		}

		/// <inheritdoc cref="DoFilter{T, U}(IEnumerable{Option{T}}, Func{T, U})"/>
		public static IEnumerable<U> Filter<T, U>(this IEnumerable<Option<T>> @this, Func<T, U> map) =>
			DoFilter(@this, map);

		/// <inheritdoc cref="DoFilter{T, U}(IEnumerable{Option{T}}, Func{T, U})"/>
		public static IEnumerable<Option<U>> Filter<T, U>(this IEnumerable<Option<T>> @this, Func<T, Option<U>> map) =>
			DoFilter(@this, map);
	}
}
