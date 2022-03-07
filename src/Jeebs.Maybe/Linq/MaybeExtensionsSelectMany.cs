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
	/// Enables LINQ select many on Maybe objects, e.g.
	/// <code>from x in Maybe<br/>
	/// from y in Maybe<br/>
	/// select y</code>
	/// </summary>
	/// <typeparam name="T">Maybe type</typeparam>
	/// <typeparam name="TInner">Interim type</typeparam>
	/// <typeparam name="TReturn">Return type</typeparam>
	/// <param name="this">Maybe</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Maybe<TReturn> SelectMany<T, TInner, TReturn>(this Maybe<T> @this, Func<T, Maybe<TInner>> f, Func<T, TInner, TReturn> g) =>
		F.MaybeF.Bind(@this,
			x =>
				f(x)
					.Map(y => g(x, y), F.MaybeF.DefaultHandler)
		);

	/// <inheritdoc cref="SelectMany{T, U, V}(Maybe{T}, Func{T, Maybe{U}}, Func{T, U, V})"/>
	public static Task<Maybe<TReturn>> SelectMany<T, TInner, TReturn>(this Maybe<T> @this, Func<T, Task<Maybe<TInner>>> f, Func<T, TInner, TReturn> g) =>
		F.MaybeF.BindAsync(@this,
			x =>
				f(x)
					.MapAsync(y => g(x, y), F.MaybeF.DefaultHandler)
		);

	/// <inheritdoc cref="SelectMany{T, U, V}(Maybe{T}, Func{T, Maybe{U}}, Func{T, U, V})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Task<Maybe<TReturn>> SelectMany<T, TInner, TReturn>(this Task<Maybe<T>> @this, Func<T, Maybe<TInner>> f, Func<T, TInner, TReturn> g) =>
		F.MaybeF.BindAsync(@this,
			x =>
				Task.FromResult(
					f(x)
						.Map(y => g(x, y), F.MaybeF.DefaultHandler)
				)
		);

	/// <inheritdoc cref="SelectMany{T, U, V}(Maybe{T}, Func{T, Maybe{U}}, Func{T, U, V})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Task<Maybe<TReturn>> SelectMany<T, TInner, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<Maybe<TInner>>> f, Func<T, TInner, TReturn> g) =>
		F.MaybeF.BindAsync(@this,
			x =>
				f(x)
					.MapAsync(y => g(x, y), F.MaybeF.DefaultHandler)
		);
}
