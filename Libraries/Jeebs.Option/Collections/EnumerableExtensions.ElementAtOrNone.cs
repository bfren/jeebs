// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static F.OptionF;
using Jeebs.Linq;
using Msg = Jeebs.EnumerableExtensionsMsg;

namespace Jeebs
{
	public static partial class EnumerableExtensions
	{
		/// <summary>
		/// Filter elements to return only <see cref="Some{T}"/>
		/// and then return the element at <paramref name="index"/> or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="index">Index</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static Option<T> DoElementAtOrNone<T>(this IEnumerable<Option<T>> @this, int index, Func<T, bool>? predicate) =>
			DoFilter(@this, predicate).ElementAtOrDefault(index) ?? None<T, Msg.ElementAtIsNullMsg>();

		/// <inheritdoc cref="DoElementAtOrNone{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static Option<T> ElementAtOrNone<T>(this IEnumerable<Option<T>> @this, int index) =>
			DoElementAtOrNone(@this, index, null);

		/// <inheritdoc cref="DoElementAtOrNone{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static Option<T> ElementAtOrNone<T>(this IEnumerable<Option<T>> @this, int index, Func<T, bool> predicate) =>
			DoElementAtOrNone(@this, index, predicate);
	}

	namespace EnumerableExtensionsMsg
	{
		/// <summary>Null item found when doing ElementAtOrDefault()</summary>
		public sealed record ElementAtIsNullMsg : IMsg { }
	}
}
