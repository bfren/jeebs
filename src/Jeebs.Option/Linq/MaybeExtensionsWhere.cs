// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;

namespace Jeebs.Linq;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: Linq Methods
/// </summary>
public static partial class OptionExtensions
{
	/// <summary>
	/// Enables LINQ where on Maybe objects, e.g.
	/// <code>from x in Maybe<br/>
	/// where x == y<br/>
	/// select x</code>
	/// </summary>
	/// <typeparam name="T">Maybe type</typeparam>
	/// <param name="this">Maybe</param>
	/// <param name="predicate">Select where predicate</param>
	public static Maybe<T> Where<T>(this Maybe<T> @this, Func<T, bool> predicate) =>
		F.MaybeF.Filter(@this, predicate);

	/// <inheritdoc cref="Where{T}(Maybe{T}, Func{T, bool})"/>
	public static Task<Maybe<T>> Where<T>(this Maybe<T> @this, Func<T, Task<bool>> predicate) =>
		F.MaybeF.FilterAsync(@this, predicate);

	/// <inheritdoc cref="Where{T}(Maybe{T}, Func{T, bool})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="predicate">Select where predicate</param>
	public static Task<Maybe<T>> Where<T>(this Task<Maybe<T>> @this, Func<T, bool> predicate) =>
		F.MaybeF.FilterAsync(@this, x => Task.FromResult(predicate(x)));

	/// <inheritdoc cref="Where{T}(Maybe{T}, Func{T, bool})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="predicate">Select where predicate</param>
	public static Task<Maybe<T>> Where<T>(this Task<Maybe<T>> @this, Func<T, Task<bool>> predicate) =>
		F.MaybeF.FilterAsync(@this, predicate);
}
