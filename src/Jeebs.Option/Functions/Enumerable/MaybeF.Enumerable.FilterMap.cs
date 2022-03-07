// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	public static partial class Enumerable
	{
		/// <summary>
		/// Filter elements to return only <see cref="Jeebs.Internals.Some{T}"/> and transform using <paramref name="map"/>
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="list">Maybe list</param>
		/// <param name="map">Mapping function</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static IEnumerable<U> FilterMap<T, U>(IEnumerable<Maybe<T>> list, Func<T, U> map, Func<T, bool>? predicate)
		{
			foreach (var some in Filter(list, predicate))
			{
				yield return map(some);
			}
		}
	}
}
