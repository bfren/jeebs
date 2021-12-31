// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace F;

public static partial class OptionF
{
	public static partial class Enumerable
	{
		/// <summary>
		/// Return the single element or <see cref="Jeebs.Internals.None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="predicate">[Optional] Predicate to filter items</param>
		public static Option<T> SingleOrNone<T>(IEnumerable<T> list, Func<T, bool>? predicate) =>
			Catch<T>(() =>
				list.Any() switch
				{
					true =>
						list.Where(x => predicate is null || predicate(x)) switch
						{
							{ } filtered when filtered.Count() == 1 =>
								filtered.SingleOrDefault() switch
								{
									T x =>
										x,

									_ =>
										None<T, M.NullItemMsg>()
								},

							{ } filtered when !filtered.Any() =>
								None<T, M.NoMatchingItemsMsg>(),

							_ =>
								None<T, M.MultipleItemsMsg>()
						},

					false =>
						None<T, M.ListIsEmptyMsg>()
				},
				DefaultHandler
			);

		/// <summary>Messages</summary>
		public static partial class M
		{
			/// <summary>Multiple items found when doing SingleOrDefault()</summary>
			public sealed record class MultipleItemsMsg : Msg;

			/// <summary>No items found matching the predicate</summary>
			public sealed record class NoMatchingItemsMsg : Msg;

			/// <summary>Null item found when doing SingleOrDefault()</summary>
			public sealed record class NullItemMsg : Msg;
		}
	}
}
