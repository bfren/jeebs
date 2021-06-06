// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.Linq
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
