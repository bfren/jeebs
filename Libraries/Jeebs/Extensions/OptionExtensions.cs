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
		/// <code>select x</code>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="f">Return selector</param>
		public static Option<U> Select<T, U>(this Option<T> @this, Func<T, U> f)
			=> @this.Map(f);

		/// <summary>
		/// Enables LINQ select many on Option classes, e.g.
		/// <code>from x in Option</code>
		/// <code>from y in Option</code>
		/// <code>select y</code>
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
