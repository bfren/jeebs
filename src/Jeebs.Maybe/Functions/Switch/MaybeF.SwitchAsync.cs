// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="Switch{T, U}(Maybe{T}, Func{T, U}, Func{Msg, U})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(Maybe<T> maybe, Func<T, Task<TReturn>> some, Func<Msg, Task<TReturn>> none) =>
		Switch(
			maybe,
			some: v => some(v),
			none: r => none(r)
		);

	/// <inheritdoc cref="Switch{T, U}(Maybe{T}, Func{T, U}, Func{Msg, U})"/>
	public static async Task<TReturn> SwitchAsync<T, TReturn>(Task<Maybe<T>> maybe, Func<T, Task<TReturn>> some, Func<Msg, Task<TReturn>> none) =>
		await SwitchAsync(await maybe.ConfigureAwait(false), some, none).ConfigureAwait(false);
}
