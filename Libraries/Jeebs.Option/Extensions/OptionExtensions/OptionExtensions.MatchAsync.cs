// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: MatchAsync
	/// </summary>
	public static class OptionExtensions_MatchAsync
	{
		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		internal static Task<U> DoMatchAsync<T, U>(Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(@this, some, none);

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, U none) =>
			F.OptionF.MatchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none));

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, U none) =>
			F.OptionF.MatchAsync(@this, some: some, none: _ => Task.FromResult(none));

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Task<U> none) =>
			F.OptionF.MatchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none);

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Task<U> none) =>
			F.OptionF.MatchAsync(@this, some: some, none: _ => none);

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<U> none) =>
			F.OptionF.MatchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none()));

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<U> none) =>
			F.OptionF.MatchAsync(@this, some: some, none: _ => Task.FromResult(none()));

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<Task<U>> none) =>
			F.OptionF.MatchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none());

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<Task<U>> none) =>
			F.OptionF.MatchAsync(@this, some: some, none: _ => none());

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<IMsg?, U> none) =>
			F.OptionF.MatchAsync(@this, some: v => Task.FromResult(some(v)), none: r => Task.FromResult(none(r)));

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg?, U> none) =>
			F.OptionF.MatchAsync(@this, some: some, none: r => Task.FromResult(none(r)));

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(@this, some: v => Task.FromResult(some(v)), none: none);

		/// <inheritdoc cref="F.OptionF.MatchAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(@this, some: some, none: none);
	}
}
