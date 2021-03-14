// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// Enumerable Extensions: ElementAtOrNone
	/// </summary>
	public static class EnumerableExtensions_ElementAtOrNone
	{
		/// <inheritdoc cref="F.OptionF.Enumerable.ElementAtOrNone{T}(IEnumerable{T}, int)"/>
		public static Option<T> ElementAtOrNone<T>(this IEnumerable<T> @this, int index) =>
			F.OptionF.Enumerable.ElementAtOrNone(@this, index);
	}
}
