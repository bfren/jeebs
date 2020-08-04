using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extension Methods
	/// </summary>
	public static class OptionExtensions
	{
		/// <summary>
		/// Enables LINQ select on Option classes, e.g.
		/// <code>from x in Option</code>
		/// <code>from y in Option</code>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <typeparam name="U">Interim type</typeparam>
		/// <typeparam name="V">Return type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="f">Interim selector</param>
		/// <param name="g">Return selector</param>
		public static Option<V> SelectMany<T, U, V>(this Option<T> @this, Func<T, Option<U>> f, Func<T, U, V> g)
			=> @this.Bind(x => f(x).Map(y => g(x, y)));
	}
}
