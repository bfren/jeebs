using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Func extensions
	/// </summary>
	public static class FuncExtensions
	{
		/// <summary>
		/// Function Composition = do f, then do g - i.e. takes <typeparamref name="T1"/> and composes it to end up with <typeparamref name="T3"/>
		/// <para>Can be chained together</para>
		/// </summary>
		/// <typeparam name="T1">Type 1</typeparam>
		/// <typeparam name="T2">Type 2</typeparam>
		/// <typeparam name="T3">Type 3</typeparam>
		/// <param name="f">Function 1</param>
		/// <param name="g">Function 2</param>
		public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T2, T3> f, Func<T1, T2> g) => x => f(g(x));

		/// <summary>
		/// Function Pipeline = take <typeparamref name="T1"/> p and then do f, returning <typeparamref name="T2"/>
		/// </summary>
		/// <typeparam name="T1">Type 1</typeparam>
		/// <typeparam name="T2">Type 2</typeparam>
		/// <param name="p">Initial value</param>
		/// <param name="f">Function to act on value</param>
		public static T2 Pipe<T1, T2>(this T1 p, Func<T1, T2> f) => f(p);
	}
}
