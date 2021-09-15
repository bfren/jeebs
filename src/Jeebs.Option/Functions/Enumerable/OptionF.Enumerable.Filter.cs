// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs;

namespace F;

public static partial class OptionF
{
	public static partial class Enumerable
	{
		/// <summary>
		/// Filter elements to return only the values of those that are <see cref="Jeebs.Internals.Some{T}"/>
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
	}
}
