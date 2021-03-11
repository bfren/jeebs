// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: MatchAsync
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		internal static async Task<U> DoMatchAsync<T, U>(Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			await @this switch
			{
				Some<T> x =>
					await some(x.Value),

				None<T> x =>
					await none(x.Reason),

				_ =>
					throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};

		/// <inheritdoc cref="Option{T}.MatchAsync{U}(Func{T, Task{U}}, U)"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, U none) =>
			DoMatchAsync(
				@this,
				some: v => Task.FromResult(some(v)),
				none: _ => Task.FromResult(none)
			);

		/// <inheritdoc cref="Option{T}.MatchAsync{U}(Func{T, Task{U}}, U)"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, U none) =>
			DoMatchAsync(
				@this,
				some: some,
				none: _ => Task.FromResult(none)
			);

		/// <inheritdoc cref="Option{T}.MatchAsync{U}(Func{T, Task{U}}, U)"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Task<U> none) =>
			DoMatchAsync(
				@this,
				some: v => Task.FromResult(some(v)),
				none: _ => none
			);

		/// <inheritdoc cref="Option{T}.MatchAsync{U}(Func{T, Task{U}}, U)"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Task<U> none) =>
			DoMatchAsync(
				@this,
				some: some,
				none: _ => none
			);

		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<U> none) =>
			DoMatchAsync(
				@this,
				some: v => Task.FromResult(some(v)),
				none: _ => Task.FromResult(none())
			);

		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<U> none) =>
			DoMatchAsync(
				@this,
				some: some,
				none: _ => Task.FromResult(none())
			);

		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<Task<U>> none) =>
			DoMatchAsync(
				@this,
				some: v => Task.FromResult(some(v)),
				none: _ => none()
			);

		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<Task<U>> none) =>
			DoMatchAsync(
				@this,
				some: some,
				none: _ => none()
			);

		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<IMsg?, U> none) =>
			DoMatchAsync(
				@this,
				some: v => Task.FromResult(some(v)),
				none: r => Task.FromResult(none(r))
			);

		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg?, U> none) =>
			DoMatchAsync(
				@this,
				some: some,
				none: r => Task.FromResult(none(r))
			);

		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<IMsg?, Task<U>> none) =>
			DoMatchAsync(
				@this,
				some: v => Task.FromResult(some(v)),
				none: none
			);

		/// <inheritdoc cref="Option{T}.DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			DoMatchAsync(
				@this,
				some: some,
				none: none
			);
	}
}
