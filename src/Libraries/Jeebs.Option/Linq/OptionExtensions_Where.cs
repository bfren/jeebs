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
		/// Enables LINQ where on Option objects, e.g.
		/// <code>from x in Option<br/>
		/// where x == y<br/>
		/// select x</code>
		/// </summary>
		/// <typeparam name="T">Option type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="predicate">Select where predicate</param>
		public static Option<T> Where<T>(this Option<T> @this, Func<T, bool> predicate) =>
			F.OptionF.Filter(@this, predicate);

		/// <inheritdoc cref="Where{T}(Option{T}, Func{T, bool})"/>
		public static Task<Option<T>> Where<T>(this Option<T> @this, Func<T, Task<bool>> predicate) =>
			F.OptionF.FilterAsync(@this, predicate);

		/// <inheritdoc cref="Where{T}(Option{T}, Func{T, bool})"/>
		/// <param name="this">Option (awaitable)</param>
		public static Task<Option<T>> Where<T>(this Task<Option<T>> @this, Func<T, bool> predicate) =>
			F.OptionF.FilterAsync(@this, x => Task.FromResult(predicate(x)));

		/// <inheritdoc cref="Where{T}(Option{T}, Func{T, bool})"/>
		/// <param name="this">Option (awaitable)</param>
		public static Task<Option<T>> Where<T>(this Task<Option<T>> @this, Func<T, Task<bool>> predicate) =>
			F.OptionF.FilterAsync(@this, predicate);
	}
}
