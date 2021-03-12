// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static F.OptionF;

namespace Jeebs.Linq
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: Linq Methods
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <summary>
		/// Enables LINQ select on Option objects, e.g.
		/// <code>from x in Option<br/>
		/// select x</code>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="f">Return map function</param>
		public static Option<U> Select<T, U>(this Option<T> @this, Func<T, U> f) =>
			@this.Map(f);

		/// <summary>
		/// Enables LINQ select many on Option objects, e.g.
		/// <code>from x in Option<br/>
		/// from y in Option<br/>
		/// select y</code>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <typeparam name="U">Interim type</typeparam>
		/// <typeparam name="V">Return type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="f">Interim bind function</param>
		/// <param name="g">Return map function</param>
		public static Option<V> SelectMany<T, U, V>(this Option<T> @this, Func<T, Option<U>> f, Func<T, U, V> g) =>
			@this.Bind(x => f(x).Map(y => g(x, y)));

		/// <summary>
		/// Enables LINQ where on Option objects, e.g.
		/// <code>from x in Option<br/>
		/// where x == y<br/>
		/// select x</code>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="predicate">Select where predicate</param>
		public static Option<T> Where<T>(this Option<T> @this, Func<T, bool> predicate) =>
			@this.Bind(x => predicate(x) switch
			{
				true =>
					@this,

				false =>
					None<T>(new PredicateWasFalseMsg())
			});

		#region Messages

		public sealed record PredicateWasFalseMsg : IMsg { }

		#endregion
	}
}
