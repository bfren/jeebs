// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// <see cref="Option{T}"/> Extensions: SwitchAsync
/// </summary>
public static class OptionExtensions_SwitchAsync
{
	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, U none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, U none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Task<U> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none);

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Task<U> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: _ => none);

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<U> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<U> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<Task<U>> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none());

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<Task<U>> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: _ => none());

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<IMsg, U> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg, U> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<IMsg, Task<U>> none) =>
		F.OptionF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: none);

	/// <inheritdoc cref="F.OptionF.SwitchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
	public static Task<U> SwitchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg, Task<U>> none) =>
		F.OptionF.SwitchAsync(@this, some: some, none: none);
}
