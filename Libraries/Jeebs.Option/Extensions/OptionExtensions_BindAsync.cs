// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Option.Exceptions;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: BindAsync
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <inheritdoc cref="Option{T}.DoBindAsync{U}(Func{T, Task{Option{U}}}, Handler?)"/>
		/// <param name="this">Option (awaitable)</param>
		internal static async Task<Option<U>> DoBindAsync<T, U>(
			Task<Option<T>> @this,
			Func<T, Task<Option<U>>> bind,
			Handler? handler
		) =>
			await (await @this).DoBindAsync(bind, handler);

		/// <inheritdoc cref="DoBindAsync{T, U}(Task{Option{T}}, Func{T, Task{Option{U}}}, Handler?)"/>
		public static Task<Option<U>> BindAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Option<U>> bind,
			Handler? handler = null
		) =>
			DoBindAsync(@this, x => Task.FromResult(bind(x)), handler);

		/// <inheritdoc cref="DoBindAsync{T, U}(Task{Option{T}}, Func{T, Task{Option{U}}}, Handler?)"/>
		public static Task<Option<U>> BindAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Task<Option<U>>> bind,
			Handler? handler = null
		) =>
			DoBindAsync(@this, bind, handler);
	}
}
