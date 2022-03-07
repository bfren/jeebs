// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Maybe{T}}?, Func{T, Maybe{T}}?)"/>
	public static async Task<Maybe<T>> SwitchIfAsync<T>(Task<Maybe<T>> maybe, Func<T, bool> check, Func<T, Maybe<T>>? ifTrue, Func<T, Maybe<T>>? ifFalse) =>
		SwitchIf(await maybe.ConfigureAwait(false), check, ifTrue, ifFalse);

	/// <inheritdoc cref="SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Msg})"/>
	public static async Task<Maybe<T>> SwitchIfAsync<T>(Task<Maybe<T>> maybe, Func<T, bool> check, Func<T, Msg> ifFalse) =>
		SwitchIf(await maybe.ConfigureAwait(false), check, ifFalse);
}
