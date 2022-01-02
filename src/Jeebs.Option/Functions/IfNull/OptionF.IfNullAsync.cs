// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Internals;

namespace F;

public static partial class OptionF
{
	/// <inheritdoc cref="IfNull{T}(Option{T}, Func{Option{T}})"/>
	public static Task<Option<T>> IfNullAsync<T>(Option<T> option, Func<Task<Option<T>>> ifNull) =>
		CatchAsync(() =>
			option switch
			{
				Some<T> x when x.Value is null =>
					ifNull(),

				None<T> x when x.Reason is M.NullValueMsg =>
					ifNull(),

				_ =>
					option.AsTask
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T}(Option{T}, Func{Option{T}})"/>
	public static async Task<Option<T>> IfNullAsync<T>(Task<Option<T>> option, Func<Task<Option<T>>> ifNull) =>
		await IfNullAsync(await option.ConfigureAwait(false), ifNull).ConfigureAwait(false);

	/// <inheritdoc cref="IfNull{T, TMsg}(Option{T}, Func{TMsg})"/>
	public static async Task<Option<T>> IfNullAsync<T, TMsg>(Task<Option<T>> option, Func<TMsg> ifNull)
		where TMsg : Msg =>
		await IfNullAsync(await option.ConfigureAwait(false), () => None<T>(ifNull()).AsTask).ConfigureAwait(false);
}
