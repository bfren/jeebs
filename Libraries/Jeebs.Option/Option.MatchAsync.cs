// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		internal Task<U> DoMatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			Switch(
				some: v => some(v),
				none: r => none(r)
			);

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Value to use if <see cref="None{T}"/></param>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, U none) =>
			DoMatchAsync(
				some: some,
				none: _ => Task.FromResult(none)
			);

		/// <inheritdoc cref="MatchAsync{U}(Func{T, Task{U}}, U)"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Task<U> none) =>
			DoMatchAsync(
				some: v => Task.FromResult(some(v)),
				none: _ => none
			);

		/// <inheritdoc cref="MatchAsync{U}(Func{T, Task{U}}, U)"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Task<U> none) =>
			DoMatchAsync(
				some: some,
				none: _ => none
			);

		/// <inheritdoc cref="DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<U> none) =>
			DoMatchAsync(
				some: some,
				none: _ => Task.FromResult(none())
			);

		/// <inheritdoc cref="DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Func<Task<U>> none) =>
			DoMatchAsync(
				some: v => Task.FromResult(some(v)),
				none: _ => none()
			);

		/// <inheritdoc cref="DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<Task<U>> none) =>
			DoMatchAsync(
				some: some,
				none: _ => none()
			);

		/// <inheritdoc cref="DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Func<IMsg?, Task<U>> none) =>
			DoMatchAsync(
				some: v => Task.FromResult(some(v)),
				none: none
			);

		/// <inheritdoc cref="DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, U> none) =>
			DoMatchAsync(
				some: some,
				none: r => Task.FromResult(none(r))
			);

		/// <inheritdoc cref="DoMatchAsync{U}(Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			DoMatchAsync(
				some: some,
				none: none
			);
	}
}
