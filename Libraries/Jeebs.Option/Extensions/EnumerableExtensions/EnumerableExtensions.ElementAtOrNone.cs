// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// Enumerable Extensions
	/// </summary>
	public static class EnumerableExtensions_ElementAtOrNone
	{
		/// <inheritdoc cref="F.OptionF.ElementAtOrNone{T}(IEnumerable{T}, int)"/>
		public static Option<T> ElementAtOrNone<T>(this IEnumerable<T> @this, int index) =>
			F.OptionF.ElementAtOrNone(@this, index);
	}
}
