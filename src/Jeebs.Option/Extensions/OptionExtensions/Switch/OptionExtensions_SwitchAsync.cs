// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Option{T}"/> Extensions: SwitchAsync
/// </summary>
public static class OptionExtensionsSwitchAsync
{
	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, U none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, U none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Task<U> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none);

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Task<U> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: _ => none);

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<U> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<U> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<Task<U>> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none());

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<Task<U>> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: _ => none());

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<Msg, U> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<Msg, U> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<Msg, Task<U>> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: none);

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<Msg, Task<U>> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: none);
}
