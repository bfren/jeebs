// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class OptionF
{
	/// <inheritdoc cref="Switch{T, U}(Option{T}, Func{T, U}, Func{Msg, U})"/>
	public static Task<U> SwitchAsync<T, U>(Option<T> option, Func<T, Task<U>> some, Func<Msg, Task<U>> none) =>
		Switch(
			option,
			some: v => some(v),
			none: r => none(r)
		);

	/// <inheritdoc cref="Switch{T, U}(Option{T}, Func{T, U}, Func{Msg, U})"/>
	public static async Task<U> SwitchAsync<T, U>(Task<Option<T>> option, Func<T, Task<U>> some, Func<Msg, Task<U>> none) =>
		await SwitchAsync(await option, some, none);
}
