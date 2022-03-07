// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// Return the current type if it is <see cref="Jeebs.Internals.Some{T}"/> and the predicate is true
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="predicate">Predicate to use with filter</param>
	public static Maybe<T> Filter<T>(Maybe<T> maybe, Func<T, bool> predicate) =>
		Bind(
			maybe,
			x =>
				predicate(x) switch
				{
					true =>
						Some(x),

					false =>
						None<T, M.FilterPredicateWasFalseMsg>()
				}
		);

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary>Predicate was false</summary>
		public sealed record class FilterPredicateWasFalseMsg : Msg;
	}
}
