// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		public static partial class Enumerable
		{
			/// <summary>
			/// Return the element at <paramref name="index"/> or <see cref="Jeebs.Internals.None{T}"/>
			/// </summary>
			/// <typeparam name="T">Value type</typeparam>
			/// <param name="list">List of values</param>
			/// <param name="index">Index</param>
			public static Option<T> ElementAtOrNone<T>(IEnumerable<T> list, int index) =>
				Catch<T>(() =>
					list.Any() switch
					{
						true =>
							list.ElementAtOrDefault(index) switch
							{
								T x =>
									x,

								_ =>
									None<T, Msg.ElementAtIsNullMsg>()
							},

						false =>
							None<T, Msg.ListIsEmptyMsg>()
					},
					DefaultHandler
				);

			/// <summary>Messages</summary>
			public static partial class Msg
			{
				/// <summary>Null or no item found when doing ElementAtOrDefault()</summary>
				public sealed record class ElementAtIsNullMsg : IMsg { }

				/// <summary>The list is empty</summary>
				public sealed record class ListIsEmptyMsg : IMsg { }
			}
		}
	}
}
