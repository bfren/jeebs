// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="Switch{T, U}(Maybe{T}, Func{T, U}, Func{Msg, U})"/>
	public static Task<U> SwitchAsync<T, U>(Maybe<T> maybe, Func<T, Task<U>> some, Func<Msg, Task<U>> none) =>
		Switch(
			maybe,
			some: v => some(v),
			none: r => none(r)
		);

	/// <inheritdoc cref="Switch{T, U}(Maybe{T}, Func{T, U}, Func{Msg, U})"/>
	public static async Task<U> SwitchAsync<T, U>(Task<Maybe<T>> maybe, Func<T, Task<U>> some, Func<Msg, Task<U>> none) =>
		await SwitchAsync(await maybe.ConfigureAwait(false), some, none).ConfigureAwait(false);
}
