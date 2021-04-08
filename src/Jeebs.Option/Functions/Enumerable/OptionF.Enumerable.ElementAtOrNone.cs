// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			/// Return the element at <paramref name="index"/> or <see cref="Jeebs.None{T}"/>
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
				public sealed record ElementAtIsNullMsg : IMsg { }

				/// <summary>The list is empty</summary>
				public sealed record ListIsEmptyMsg : IMsg { }
			}
		}
	}
}
