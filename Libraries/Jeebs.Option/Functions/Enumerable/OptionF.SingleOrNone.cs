// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Return the single element or <see cref="Jeebs.None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="predicate">[Optional] Predicate to filter items</param>
		public static Option<T> SingleOrNone<T>(IEnumerable<T> list, Func<T, bool>? predicate) =>
			list.SingleOrDefault(x => predicate is null || predicate(x)) switch
			{
				T x =>
					x,

				_ =>
					None<T, Msg.NullOrMultipleItemsMsg>()
			};

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Null or multiple items found when doing SingleOrDefault()</summary>
			public sealed record NullOrMultipleItemsMsg : IMsg { }
		}
	}
}
