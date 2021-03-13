// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using static F.OptionF;

namespace Jeebs
{
	public static partial class EnumerableExtensions
	{
		/// <summary>
		/// Return the first element or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="this">List of items</param>
		/// <param name="predicate">[Optional] Predicate to filter items</param>
		internal static Option<T> DoFirstOrNone<T>(IEnumerable<T> @this, Func<T, bool>? predicate) =>
			@this.FirstOrDefault(x => predicate is null || predicate(x)) switch
			{
				T x =>
					x,

				_ =>
					None<T, Msg.FirstItemIsNullMsg>()
			};

		/// <inheritdoc cref="DoFirstOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> FirstOrNone<T>(this IEnumerable<T> @this) =>
			DoFirstOrNone(@this, null);

		/// <inheritdoc cref="DoFirstOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> FirstOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
			DoFirstOrNone(@this, predicate);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Null item found when doing FirstOrDefault()</summary>
			public sealed record FirstItemIsNullMsg : IMsg { }
		}
	}
}
