using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/> interface: SelectMany
	/// </summary>
	public static class RExtensions_SelectMany
	{
		/// <summary>
		/// Enables LINQ select many on Result objects, e.g.
		/// <code>from x in Result A</code>
		/// <code>from y in Result B</code>
		/// <code>select x + y</code>
		/// <para>NB: only works with <see cref="IOkV{TValue}"/> - otherwise will return <see cref="IError{TValue}"/></para>
		/// </summary>
		/// <typeparam name="T">Result value type</typeparam>
		/// <typeparam name="U">Interim result value type</typeparam>
		/// <typeparam name="V">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="bind">Interim bind function</param>
		/// <param name="map">Next map function</param>
		public static IR<V> SelectMany<T, U, V>(this IR<T> @this, Func<T, IR<U>> bind, Func<T, U, V> map)
			=> @this switch
			{
				IOkV<T> x => bind(x.Value) switch
				{
					IOkV<U> y => y.OkV(map(x.Value, y.Value)),
					{ } y => y.Error<V>()
				},
				_ => @this.Error<V>()
			};

		/// <summary>
		/// Enables LINQ select many on Result objects, e.g.
		/// <code>from x in Result A</code>
		/// <code>from y in Result B</code>
		/// <code>select x + y</code>
		/// <para>NB: only works with <see cref="IOkV{TValue, TState}"/> - otherwise will return <see cref="IError{TValue, TState}"/></para>
		/// </summary>
		/// <typeparam name="S">Result state value</typeparam>
		/// <typeparam name="T">Result value type</typeparam>
		/// <typeparam name="U">Interim result value type</typeparam>
		/// <typeparam name="V">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="bind">Interim bind function</param>
		/// <param name="map">Next map function</param>
		public static IR<V, S> SelectMany<S, T, U, V>(this IR<T, S> @this, Func<T, IR<U, S>> bind, Func<T, U, V> map)
			=> @this switch
			{
				IOkV<T, S> x => bind(x.Value) switch
				{
					IOkV<U, S> y => y.OkV(map(x.Value, y.Value)),
					{ } y => y.Error<V>()
				},
				_ => @this.Error<V>()
			};
	}
}
