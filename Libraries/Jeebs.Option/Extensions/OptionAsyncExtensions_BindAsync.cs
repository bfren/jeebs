// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class OptionAsyncExtensions
	{
		/// <summary>
		/// Perform an asynchronous bind, awaiting the current Option type first -
		/// if <paramref name="this"/> is <see cref="None{T}"/>, skips ahead to next type
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="bind">Bind function - receives value of <paramref name="this"/> if it is <see cref="Some{T}"/></param>
		public static async Task<Option<U>> BindAsync<T, U>(this Task<Option<T>> @this, Func<T, Option<U>> bind) =>
			await (await @this).BindAsync(async value => bind(value));

		/// <inheritdoc cref="BindAsync{T, U}(Task{Option{T}}, Func{T, Option{U}})"/>
		public static async Task<Option<U>> BindAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<Option<U>>> bind) =>
			await (await @this).BindAsync(bind);
	}
}
