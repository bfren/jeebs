// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: BindAsync
	/// </summary>
	public static class OptionExtensions_BindAsync
	{
		/// <summary>
		/// Use <paramref name="bind"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="bind">Binding function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		/// <param name="handler">[Optional] Exception handler</param>
		private static Task<Option<U>> BindAsyncPrivate<T, U>(
			Task<Option<T>> @this,
			Func<T, Task<Option<U>>> bind,
			Option.Handler? handler = null
		) =>
			Option.CatchAsync(async () =>
				await @this switch
				{
					Some<T> some =>
						await bind(some.Value),

					None<T> none =>
						new None<U>(none.Reason),

					_ =>
						throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
				},
				handler
			);

		/// <inheritdoc cref="BindAsyncPrivate{T, U}(Task{Option{T}}, Func{T, Task{Option{U}}}, Option.Handler?)"/>
		public static Task<Option<U>> BindAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Option<U>> bind,
			Option.Handler? handler = null
		) =>
			BindAsyncPrivate(@this, x => Task.FromResult(bind(x)), handler);

		/// <inheritdoc cref="BindAsyncPrivate{T, U}(Task{Option{T}}, Func{T, Task{Option{U}}}, Option.Handler?)"/>
		public static Task<Option<U>> BindAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Task<Option<U>>> bind,
			Option.Handler? handler = null
		) =>
			BindAsyncPrivate(@this, bind, handler);
	}
}
