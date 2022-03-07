// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="Map{T, U}(Maybe{T}, Func{T, U}, Handler)"/>
	public static Task<Maybe<U>> MapAsync<T, U>(Maybe<T> maybe, Func<T, Task<U>> map, Handler handler) =>
		CatchAsync(() =>
			Switch(
				maybe,
				some: async v => { var x = await map(v).ConfigureAwait(false); return Some(x); },
				none: r => None<U>(r).AsTask
			),
			handler
		);

	/// <inheritdoc cref="Map{T, U}(Maybe{T}, Func{T, U}, Handler)"/>
	public static async Task<Maybe<U>> MapAsync<T, U>(Task<Maybe<T>> maybe, Func<T, Task<U>> map, Handler handler) =>
		await MapAsync(await maybe.ConfigureAwait(false), map, handler).ConfigureAwait(false);
}
