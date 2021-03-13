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
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static IEnumerable<Option<T>> DoFilter<T>(IEnumerable<Option<T>> @this, Func<T, bool>? predicate) =>
			@this.Where(o => o is Some<T> s && (predicate == null || predicate(s.Value)));

		/// <inheritdoc cref="DoFilter{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static IEnumerable<Option<T>> Filter<T>(this IEnumerable<Option<T>> @this) =>
			DoFilter(@this, null);

		/// <inheritdoc cref="DoFilter{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static IEnumerable<Option<T>> Filter<T>(this IEnumerable<Option<T>> @this, Func<T, bool> predicate) =>
			DoFilter(@this, predicate);
	}
}
