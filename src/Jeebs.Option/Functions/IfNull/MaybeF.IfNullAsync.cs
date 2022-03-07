// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Internals;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(Maybe<T> maybe, Func<Task<Maybe<T>>> ifNull) =>
		CatchAsync(() =>
			maybe switch
			{
				Some<T> x when x.Value is null =>
					ifNull(),

				None<T> x when x.Reason is M.NullValueMsg =>
					ifNull(),

				_ =>
					maybe.AsTask
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public static async Task<Maybe<T>> IfNullAsync<T>(Task<Maybe<T>> maybe, Func<Task<Maybe<T>>> ifNull) =>
		await IfNullAsync(await maybe.ConfigureAwait(false), ifNull).ConfigureAwait(false);

	/// <inheritdoc cref="IfNull{T, TMsg}(Maybe{T}, Func{TMsg})"/>
	public static async Task<Maybe<T>> IfNullAsync<T, TMsg>(Task<Maybe<T>> maybe, Func<TMsg> ifNull)
		where TMsg : Msg =>
		await IfNullAsync(await maybe.ConfigureAwait(false), () => None<T>(ifNull()).AsTask).ConfigureAwait(false);
}
