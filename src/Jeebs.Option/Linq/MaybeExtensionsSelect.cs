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
	/// Enables LINQ select on Maybe objects, e.g.
	/// <code>from x in Maybe<br/>
	/// select x</code>
	/// </summary>
	/// <typeparam name="T">Maybe type</typeparam>
	/// <typeparam name="U">Return type</typeparam>
	/// <param name="this">Maybe</param>
	/// <param name="f">Return map function</param>
	public static Maybe<U> Select<T, U>(this Maybe<T> @this, Func<T, U> f) =>
		F.MaybeF.Map(@this, f, F.MaybeF.DefaultHandler);

	/// <inheritdoc cref="Select{T, U}(Maybe{T}, Func{T, U})"/>
	public static Task<Maybe<U>> Select<T, U>(this Maybe<T> @this, Func<T, Task<U>> f) =>
		F.MaybeF.MapAsync(@this, f, F.MaybeF.DefaultHandler);

	/// <inheritdoc cref="Select{T, U}(Maybe{T}, Func{T, U})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Return map function</param>
	public static Task<Maybe<U>> Select<T, U>(this Task<Maybe<T>> @this, Func<T, U> f) =>
		F.MaybeF.MapAsync(@this, x => Task.FromResult(f(x)), F.MaybeF.DefaultHandler);

	/// <inheritdoc cref="Select{T, U}(Maybe{T}, Func{T, U})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Return map function</param>
	public static Task<Maybe<U>> Select<T, U>(this Task<Maybe<T>> @this, Func<T, Task<U>> f) =>
		F.MaybeF.MapAsync(@this, f, F.MaybeF.DefaultHandler);
}
