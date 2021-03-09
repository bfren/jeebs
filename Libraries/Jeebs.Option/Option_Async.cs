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
		public async Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<U> none) =>
			this switch
			{
				Some<T> x =>
					await some(x.Value),

				None<T> =>
					none(),

				_ =>
					throw new Jx.Option.UnknownOptionException()
			};

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public async Task<U> MatchAsync<U>(Func<T, U> some, Func<Task<U>> none) =>
			this switch
			{
				Some<T> x =>
					some(x.Value),

				None<T> =>
					await none(),

				_ =>
					throw new Jx.Option.UnknownOptionException()
			};

		/// <summary>
		/// Perform an asynchronous match
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public async Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<Task<U>> none) =>
			this switch
			{
				Some<T> x =>
					await some(x.Value),

				None<T> =>
					await none(),

				_ =>
					throw new Jx.Option.UnknownOptionException()
			};

		/// <summary>
		/// Use <paramref name="map"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="map">Mapping function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public async Task<Option<U>> MapAsync<U>(Func<T, Task<U>> map) =>
			this switch
			{
				Some<T> x =>
					await map(x.Value),

				None<T> y =>
					Option.None<U>(y.Reason),

				_ =>
					throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};

		/// <summary>
		/// Use <paramref name="bind"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="bind">Binding function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public async Task<Option<U>> BindAsync<U>(Func<T, Task<Option<U>>> bind) =>
			this switch
			{
				Some<T> x =>
					await bind(x.Value),

				None<T> y =>
					Option.None<U>(y.Reason),

				_ =>
					throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};
	}
}
