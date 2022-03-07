// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: SwitchIfAsync
/// </summary>
public static class OptionExtensionsSwitchIfAsync
{
	/// <inheritdoc cref="F.MaybeF.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Maybe{T}}?, Func{T, Maybe{T}}?)"/>
	public static Task<Maybe<T>> SwitchIfAsync<T>(
		this Task<Maybe<T>> @this,
		Func<T, bool> check,
		Func<T, Maybe<T>>? ifTrue = null,
		Func<T, Maybe<T>>? ifFalse = null
	) =>
		F.MaybeF.SwitchIfAsync(@this, check, ifTrue, ifFalse);

	/// <inheritdoc cref="F.MaybeF.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Msg})"/>
	public static Task<Maybe<T>> SwitchIfAsync<T>(
		this Task<Maybe<T>> @this,
		Func<T, bool> check,
		Func<T, Msg> ifFalse
	) =>
		F.MaybeF.SwitchIfAsync(@this, check, ifFalse);
}
