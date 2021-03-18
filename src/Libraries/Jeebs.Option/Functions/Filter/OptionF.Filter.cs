// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
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
		public static Option<T> Filter<T>(Option<T> option, Func<T, bool> predicate) =>
			Catch(() =>
				Bind(
					option,
					x =>
						predicate(x) switch
						{
							true =>
								Return(x),

							false =>
								None<T, Msg.FilterPredicateWasFalseMsg>()
						}
				),
				DefaultHandler
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Predicate was false</summary>
			public sealed record FilterPredicateWasFalseMsg : IMsg { }
		}
	}
}
