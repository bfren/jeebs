﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	public static partial class Enumerable
	{
		/// <summary>
		/// Return the first element or <see cref="Jeebs.Internals.None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="predicate">[Optional] Predicate to filter items</param>
		public static Maybe<T> FirstOrNone<T>(IEnumerable<T> list, Func<T, bool>? predicate) =>
			Catch<T>(() =>
				list.Any() switch
				{
					true =>
						list.FirstOrDefault(x => predicate is null || predicate(x)) switch
						{
							T x =>
								x,

							_ =>
								None<T, M.FirstItemIsNullMsg>()
						},

					false =>
						None<T, M.ListIsEmptyMsg>()
				},
				DefaultHandler
			);

		/// <summary>Messages</summary>
		public static partial class M
		{
			/// <summary>Null item found when doing FirstOrDefault()</summary>
			public sealed record class FirstItemIsNullMsg : Msg;
		}
	}
}