// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using static F.OptionF;
using Msg = Jeebs.EnumerableExtensionsMsg;

namespace Jeebs
{
	public static partial class EnumerableExtensions
	{
		/// <summary>
		/// Return the single element or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="this">List of items</param>
		/// <param name="predicate">[Optional] Predicate to filter items</param>
		internal static Option<T> DoSingleOrNone<T>(IEnumerable<T> @this, Func<T, bool>? predicate) =>
			@this.SingleOrDefault(x => predicate is null || predicate(x)) switch
			{
				T x =>
					x,

				_ =>
					None<T, Msg.NullOrMultipleItemsMsg>()
			};

		/// <inheritdoc cref="DoSingleOrNone{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static Option<T> SingleOrNone<T>(this IEnumerable<T> @this) =>
			DoSingleOrNone(@this, null);

		/// <inheritdoc cref="DoSingleOrNone{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static Option<T> SingleOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
			DoSingleOrNone(@this, predicate);
	}

	namespace EnumerableExtensionsMsg
	{
		/// <summary>Null or multiple items found when doing SingleOrDefault()</summary>
		public sealed record NullOrMultipleItemsMsg : IMsg { }
	}
}
