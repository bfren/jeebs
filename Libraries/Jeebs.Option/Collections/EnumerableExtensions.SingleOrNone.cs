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
		/// and then return the only element or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static Option<T> DoSingleOrNone<T>(this IEnumerable<Option<T>> @this, Func<T, bool>? predicate) =>
			DoFilter(@this, predicate).SingleOrDefault() ?? None<T, Msg.NullOrMultipleItemsMsg>();

		/// <inheritdoc cref="DoSingleOrNone{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static Option<T> SingleOrNone<T>(this IEnumerable<Option<T>> @this) =>
			DoSingleOrNone(@this, null);

		/// <inheritdoc cref="DoSingleOrNone{T}(IEnumerable{Option{T}}, Func{T, bool}?)"/>
		public static Option<T> SingleOrNone<T>(this IEnumerable<Option<T>> @this, Func<T, bool> predicate) =>
			DoSingleOrNone(@this, predicate);
	}

	namespace EnumerableExtensionsMsg
	{
		/// <summary>Null or multiple items found when doing SingleOrDefault()</summary>
		public sealed record NullOrMultipleItemsMsg : IMsg { }
	}
}
