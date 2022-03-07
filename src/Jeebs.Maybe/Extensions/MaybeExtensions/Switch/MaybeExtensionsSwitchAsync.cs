// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: SwitchAsync
/// </summary>
public static class OptionExtensionsSwitchAsync
{
	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, TReturn none) =>
		F.MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, TReturn none) =>
		F.MaybeF.SwitchAsync(@this, some: some, none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Task<TReturn> none) =>
		F.MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none);

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Task<TReturn> none) =>
		F.MaybeF.SwitchAsync(@this, some: some, none: _ => none);

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<TReturn> none) =>
		F.MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<TReturn> none) =>
		F.MaybeF.SwitchAsync(@this, some: some, none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<Task<TReturn>> none) =>
		F.MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none());

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<Task<TReturn>> none) =>
		F.MaybeF.SwitchAsync(@this, some: some, none: _ => none());

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<Msg, TReturn> none) =>
		F.MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<Msg, TReturn> none) =>
		F.MaybeF.SwitchAsync(@this, some: some, none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<Msg, Task<TReturn>> none) =>
		F.MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: none);

	/// <inheritdoc cref="F.MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<Msg, Task<TReturn>> none) =>
		F.MaybeF.SwitchAsync(@this, some: some, none: none);
}
