// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using static F.OptionF;

namespace Jeebs
{
	public static class EnumerableExtensions_LastOrNone
	{
		/// <summary>
		/// Return the last element or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="this">List of items</param>
		/// <param name="predicate">[Optional] Predicate to filter items</param>
		internal static Option<T> DoLastOrNone<T>(IEnumerable<T> @this, Func<T, bool>? predicate) =>
			@this.FirstOrDefault(x => predicate is null || predicate(x)) switch
			{
				T x =>
					x,

				_ =>
					None<T, Msg.LastItemIsNullMsg>()
			};

		/// <inheritdoc cref="DoLastOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> LastOrNone<T>(this IEnumerable<T> @this) =>
			DoLastOrNone(@this, null);

		/// <inheritdoc cref="DoLastOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> LastOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
			DoLastOrNone(@this, predicate);

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Null item found when doing LastOrDefault()</summary>
			public sealed record LastItemIsNullMsg : IMsg { }
		}
	}
}
