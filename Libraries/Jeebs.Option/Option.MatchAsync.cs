// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public abstract partial record Option<T>
	{
		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		private Task<U> MatchAsyncPrivate<U>(Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			this switch
			{
				Some<T> x =>
					some(x.Value),

				None<T> y =>
					none(y.Reason),

				_ =>
					throw new Jx.Option.UnknownOptionException()
			};

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<U> none) =>
			MatchAsyncPrivate(
				some: some,
				none: _ => Task.FromResult(none())
			);

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public Task<U> MatchAsync<U>(Func<T, U> some, Func<Task<U>> none) =>
			MatchAsyncPrivate(
				some: x => Task.FromResult(some(x)),
				none: _ => none()
			);

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<Task<U>> none) =>
			MatchAsyncPrivate(
				some: some,
				none: _ => none()
			);

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, U> none) =>
			MatchAsyncPrivate(
				some: some,
				none: x => Task.FromResult(none(x))
			);

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public Task<U> MatchAsync<U>(Func<T, U> some, Func<IMsg?, Task<U>> none) =>
			MatchAsyncPrivate(
				some: x => Task.FromResult(some(x)),
				none: none
			);

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			MatchAsyncPrivate(
				some: some,
				none: none
			);
	}
}
