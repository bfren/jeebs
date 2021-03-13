// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using static F.OptionF;
using Msg = Jeebs.EnumerableExtensionsMsg;

namespace Jeebs
{
	public static partial class EnumerableExtensions
	{
		/// <summary>
		/// Return the element at <paramref name="index"/> or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="this">List of items</param>
		/// <param name="index">Index</param>
		internal static Option<T> DoElementAtOrNone<T>(IEnumerable<T> @this, int index) =>
			@this.ElementAtOrDefault(index) switch
			{
				T x =>
					x,

				_ =>
					None<T, Msg.ElementAtIsNullMsg>()
			};

		/// <inheritdoc cref="DoElementAtOrNone{T}(IEnumerable{T}, int)"/>
		public static Option<T> ElementAtOrNone<T>(this IEnumerable<T> @this, int index) =>
			DoElementAtOrNone(@this, index);
	}

	namespace EnumerableExtensionsMsg
	{
		/// <summary>Null item found when doing ElementAtOrDefault()</summary>
		public sealed record ElementAtIsNullMsg : IMsg { }
	}
}
