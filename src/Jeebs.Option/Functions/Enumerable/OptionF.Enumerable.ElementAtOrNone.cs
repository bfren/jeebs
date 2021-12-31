// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace F;

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
								None<T, M.ElementAtIsNullMsg>()
						},

					false =>
						None<T, M.ListIsEmptyMsg>()
				},
				DefaultHandler
			);

		/// <summary>Messages</summary>
		public static partial class M
		{
			/// <summary>Null or no item found when doing ElementAtOrDefault()</summary>
			public sealed record class ElementAtIsNullMsg : Msg;

			/// <summary>The list is empty</summary>
			public sealed record class ListIsEmptyMsg : Msg;
		}
	}
}
