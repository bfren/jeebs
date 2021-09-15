﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class OptionF
{
	/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler)"/>
	public static Task<Option<U>> MapAsync<T, U>(Option<T> option, Func<T, Task<U>> map, Handler handler) =>
		CatchAsync(() =>
			Switch(
				option,
				some: async v => { var x = await map(v); return Some(x); },
				none: r => None<U>(r).AsTask
			),
			handler
		);

	/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler)"/>
	public static async Task<Option<U>> MapAsync<T, U>(Task<Option<T>> option, Func<T, Task<U>> map, Handler handler) =>
		await MapAsync(await option, map, handler);
}
