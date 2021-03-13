// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Return the current type if it is <see cref="Some{T}"/> and the predicate is true
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="predicate">Predicate to use with filter</param>
		/// <param name="handler">[Optional] Exception handler</param>
		public static Option<T> Filter<T>(Option<T> option, Func<T, bool> predicate, Handler? handler) =>
			Bind(
				option,
				x =>
					predicate(x) switch
					{
						true =>
							Return(x),

						false =>
							None<T, Msg.FilterPredicateWasFalseMsg>()
					},
				handler
			);

		/// <summary>
		/// Filter elements to return only the values of those that are <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="list">List of Option values</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static IEnumerable<T> Filter<T>(IEnumerable<Option<T>> list, Func<T, bool>? predicate)
		{
			foreach (var option in list)
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

		/// <summary>
		/// Filter elements to return only <see cref="Some{T}"/> and transform using <paramref name="map"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="list">Option list</param>
		/// <param name="map">Mapping function</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static IEnumerable<U> Filter<T, U>(IEnumerable<Option<T>> list, Func<T, U> map, Func<T, bool>? predicate)
		{
			foreach (var some in Filter(list, predicate))
			{
				yield return map(some);
			}
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Predicate was false</summary>
			public sealed record FilterPredicateWasFalseMsg : IMsg { }
		}
	}
}
