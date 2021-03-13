// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// Enumerable Extensions: SingleOrNone
	/// </summary>
	public static class EnumerableExtensions_SingleOrNone
	{
		/// <inheritdoc cref="F.OptionF.SingleOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> SingleOrNone<T>(this IEnumerable<T> @this) =>
			F.OptionF.SingleOrNone(@this, null);

		/// <inheritdoc cref="F.OptionF.SingleOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> SingleOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
			F.OptionF.SingleOrNone(@this, predicate);
	}
}
