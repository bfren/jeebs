using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/> interface: Select
	/// </summary>
	public static class RExtensions_Select
	{
		/// <summary>
		/// Enables LINQ select on Result objects, e.g.
		/// <code>from x in Result</code>
		/// <code>select x</code>
		/// <para>NB: only works with <see cref="IOkV{TValue}"/> - otherwise will return <see cref="IError{TValue}"/></para>
		/// </summary>
		/// <typeparam name="T">Result value type</typeparam>
		/// <typeparam name="U">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="map">Map function</param>
		public static IR<U> Select<T, U>(this IR<T> @this, Func<T, U> map)
			=> @this switch
			{
				IOkV<T> x => x.OkV(map(x.Value)),
				_ => @this.Error<U>()
			};

		/// <summary>
		/// Enables LINQ select on Result objects, e.g.
		/// <code>from x in Result</code>
		/// <code>select x</code>
		/// <para>NB: only works with <see cref="IOkV{TValue, TState}"/> - otherwise will return <see cref="IError{TValue, TState}"/></para>
		/// </summary>
		/// <typeparam name="S">Result state type</typeparam>
		/// <typeparam name="T">Result value type</typeparam>
		/// <typeparam name="U">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="map">Map function</param>
		public static IR<U, S> Select<S, T, U>(this IR<T, S> @this, Func<T, U> map)
			=> @this switch
			{
				IOkV<T, S> x => x.OkV(map(x.Value)),
				_ => @this.Error<U>()
			};
	}
}
