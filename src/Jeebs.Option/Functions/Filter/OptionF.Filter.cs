// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;

namespace F;

public static partial class OptionF
{
	/// <summary>
	/// Return the current type if it is <see cref="Jeebs.Internals.Some{T}"/> and the predicate is true
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <param name="option">Input option</param>
	/// <param name="predicate">Predicate to use with filter</param>
	public static Option<T> Filter<T>(Option<T> option, Func<T, bool> predicate) =>
		Bind(
			option,
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
