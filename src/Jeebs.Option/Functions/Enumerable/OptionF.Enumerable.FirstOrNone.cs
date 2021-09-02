// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		public static partial class Enumerable
		{
			/// <summary>
			/// Return the first element or <see cref="Jeebs.Internals.None{T}"/>
			/// </summary>
			/// <typeparam name="T">Value type</typeparam>
			/// <param name="list">List of values</param>
			/// <param name="predicate">[Optional] Predicate to filter items</param>
			public static Option<T> FirstOrNone<T>(IEnumerable<T> list, Func<T, bool>? predicate) =>
				Catch<T>(() =>
					list.Any() switch
					{
						true =>
							list.FirstOrDefault(x => predicate is null || predicate(x)) switch
							{
								T x =>
									x,

								_ =>
									None<T, Msg.FirstItemIsNullMsg>()
							},

						false =>
							None<T, Msg.ListIsEmptyMsg>()
					},
					DefaultHandler
				);

			/// <summary>Messages</summary>
			public static partial class Msg
			{
				/// <summary>Null item found when doing FirstOrDefault()</summary>
				public sealed record class FirstItemIsNullMsg : IMsg { }
			}
		}
	}
}
