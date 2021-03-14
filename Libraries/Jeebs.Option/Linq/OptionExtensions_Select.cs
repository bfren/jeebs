// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

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
			F.OptionF.Map(@this, f, null);

		/// <inheritdoc cref="Select{T, U}(Option{T}, Func{T, U})"/>
		public static Task<Option<U>> Select<T, U>(this Option<T> @this, Func<T, Task<U>> f) =>
			F.OptionF.MapAsync(@this, f, null);

		/// <inheritdoc cref="Select{T, U}(Option{T}, Func{T, U})"/>
		public static Task<Option<U>> Select<T, U>(this Task<Option<T>> @this, Func<T, U> f) =>
			F.OptionF.MapAsync(@this, x => Task.FromResult(f(x)), null);

		/// <inheritdoc cref="Select{T, U}(Option{T}, Func{T, U})"/>
		public static Task<Option<U>> Select<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> f) =>
			F.OptionF.MapAsync(@this, f, null);
	}
}
