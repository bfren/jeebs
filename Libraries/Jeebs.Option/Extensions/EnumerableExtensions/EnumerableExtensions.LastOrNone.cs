﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;

namespace Jeebs
{
	public static class EnumerableExtensions_LastOrNone
	{
		/// <inheritdoc cref="F.OptionF.LastOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> LastOrNone<T>(this IEnumerable<T> @this) =>
			F.OptionF.LastOrNone(@this, null);

		/// <inheritdoc cref="F.OptionF.LastOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> LastOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
			F.OptionF.LastOrNone(@this, predicate);
	}
}