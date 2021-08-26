// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Linq
{
	/// <summary>
	/// Enumerable Extensions: SingleOrNone
	/// </summary>
	public static class EnumerableExtensions_SingleOrNone
	{
		/// <inheritdoc cref="F.OptionF.Enumerable.SingleOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> SingleOrNone<T>(this IEnumerable<T> @this) =>
			F.OptionF.Enumerable.SingleOrNone(@this, null);

		/// <inheritdoc cref="F.OptionF.Enumerable.SingleOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
		public static Option<T> SingleOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
			F.OptionF.Enumerable.SingleOrNone(@this, predicate);
	}
}
