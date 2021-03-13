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
		/// and then return the first element or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static Option<T> DoFirstOrNone<T>(this IEnumerable<Option<T>> @this, Func<T, bool>? predicate) =>
			DoFilter(@this, predicate).FirstOrDefault() ?? None<T, Msg.FirstItemIsNullMsg>();

		/// <inheritdoc cref="DoFirstOrNone{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static Option<T> FirstOrNone<T>(this IEnumerable<Option<T>> @this) =>
			DoFirstOrNone(@this, null);

		/// <inheritdoc cref="DoFirstOrNone{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static Option<T> FirstOrNone<T>(this IEnumerable<Option<T>> @this, Func<T, bool> predicate) =>
			DoFirstOrNone(@this, predicate);
	}

	namespace EnumerableExtensionsMsg
	{
		/// <summary>Null item found when doing FirstOrDefault()</summary>
		public sealed record FirstItemIsNullMsg : IMsg { }
	}
}
