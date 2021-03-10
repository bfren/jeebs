// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jm.Option;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: Linq Async Methods
	/// </summary>
	public static class OptionExtensions_LinqAsync
	{
		/// <summary>
		/// Enables LINQ select on Option objects, e.g.
		/// <c>from x in Option</c>
		/// <c>select x</c>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="f">Return map function</param>
		public static Task<Option<U>> Select<T, U>(this Task<Option<T>> @this, Func<T, U> f) =>
			@this.MapAsync(f);

		/// <summary>
		/// Enables LINQ select many on Option objects, e.g.
		/// <c>from x in Option</c>
		/// <c>from y in Option</c>
		/// <c>select y</c>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <typeparam name="U">Interim type</typeparam>
		/// <typeparam name="V">Return type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="f">Interim bind function</param>
		/// <param name="g">Return map function</param>
		public static Task<Option<V>> SelectMany<T, U, V>(this Task<Option<T>> @this, Func<T, Task<Option<U>>> f, Func<T, U, V> g) =>
			@this.BindAsync(x => f(x).MapAsync(y => g(x, y)));

		/// <summary>
		/// Enables LINQ where on Option objects, e.g.
		/// <c>from x in Option</c>
		/// <c>where x == y</c>
		/// <c>select x</c>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="predicate">Select where predicate</param>
		public static Task<Option<T>> Where<T>(this Task<Option<T>> @this, Func<T, bool> predicate) =>
			@this.BindAsync(x => predicate(x) switch
			{
				true =>
					@this,

				false =>
					Task.FromResult((Option<T>)Option.None<T>(new PredicateWasFalseMsg()))
			});
	}
}
