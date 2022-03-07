// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: IfNullAsync
/// </summary>
public static class OptionExtensionsIfNullAsync
{
	/// <inheritdoc cref="F.MaybeF.IfNullAsync{T, TMsg}(Task{Maybe{T}}, Func{TMsg})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(this Task<Maybe<T>> @this, Func<Maybe<T>> ifNull) =>
		F.MaybeF.IfNullAsync(@this, () => Task.FromResult(ifNull()));

	/// <inheritdoc cref="F.MaybeF.IfNullAsync{T, TMsg}(Task{Maybe{T}}, Func{TMsg})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(this Task<Maybe<T>> @this, Func<Task<Maybe<T>>> ifNull) =>
		F.MaybeF.IfNullAsync(@this, ifNull);

	/// <inheritdoc cref="F.MaybeF.IfNullAsync{T, TMsg}(Task{Maybe{T}}, Func{TMsg})"/>
	public static Task<Maybe<T>> IfNullAsync<T, TMsg>(this Task<Maybe<T>> @this, Func<TMsg> ifNull)
		where TMsg : Msg =>
		F.MaybeF.IfNullAsync(@this, ifNull);
}
