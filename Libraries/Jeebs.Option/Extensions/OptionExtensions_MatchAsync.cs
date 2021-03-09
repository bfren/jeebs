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
		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Value to return if <see cref="None{T}"/></param>
		public static async Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, U none) =>
			(await @this).Match(some, none);

		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public static async Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<U> none) =>
			(await @this).Match(some, none);

		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public static async Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<IMsg?, U> none) =>
			(await @this).Match(some, none);

		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<U> none) =>
			@this.MatchAsync(some, none);

		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public static Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg?, U> none) =>
			@this.MatchAsync(some, none);

		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public static async Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<Task<U>> none) =>
			await (await @this).MatchAsync(some, none);

		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public static async Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, U> some, Func<IMsg?, Task<U>> none) =>
			await (await @this).MatchAsync(some, none);

		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public static async Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<Task<U>> none) =>
			await (await @this).MatchAsync(some, none);

		/// <summary>
		/// Perform an asynchronous match, awaiting the current Option type first
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public static async Task<U> MatchAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			await (await @this).MatchAsync(some, none);
	}
}
