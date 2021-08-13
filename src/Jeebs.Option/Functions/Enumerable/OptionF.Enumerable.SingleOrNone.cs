// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		public static partial class Enumerable
		{
			/// <summary>
			/// Return the single element or <see cref="Jeebs.None{T}"/>
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
											None<T, Msg.NullItemMsg>()
									},

								{ } filtered when !filtered.Any() =>
									None<T, Msg.NoMatchingItemsMsg>(),

								_ =>
									None<T, Msg.MultipleItemsMsg>()
							},

						false =>
							None<T, Msg.ListIsEmptyMsg>()
					},
					DefaultHandler
				);

			/// <summary>Messages</summary>
			public static partial class Msg
			{
				/// <summary>Multiple items found when doing SingleOrDefault()</summary>
				public sealed record class MultipleItemsMsg : IMsg { }

				/// <summary>No items found matching the predicate</summary>
				public sealed record class NoMatchingItemsMsg : IMsg { }

				/// <summary>Null item found when doing SingleOrDefault()</summary>
				public sealed record class NullItemMsg : IMsg { }
			}
		}
	}
}
