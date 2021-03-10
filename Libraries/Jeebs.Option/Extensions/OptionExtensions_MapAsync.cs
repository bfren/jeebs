// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: MapAsync
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <summary>
		/// Perform an asynchronous map, awaiting the current Option type first -
		/// if <paramref name="this"/> is <see cref="None{T}"/>, skips ahead to next type
		/// </summary>
		/// <typeparam name="T">Original value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="map">Map function - receives value of <paramref name="this"/> if it is <see cref="Some{T}"/></param>
		/// <param name="handler">[Optional] Exception handler</param>
		public static Task<Option<U>> DoMapAsync<T, U>(
			Task<Option<T>> @this,
			Func<T, Task<U>> map,
			Option.Handler? handler = null
		) =>
			Option.CatchAsync(async () =>
				await @this switch
				{
					Some<T> some =>
						Option.Wrap(await map(some.Value)),

					None<T> none =>
						new None<U>(none.Reason),

					_ =>
						throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
				},
				handler
			);

		/// <inheritdoc cref="DoMapAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Option.Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, U> map,
			Option.Handler? handler = null
		) =>
			DoMapAsync(@this, x => Task.FromResult(map(x)), handler);

		/// <inheritdoc cref="DoMapAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Option.Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Task<U>> map,
			Option.Handler? handler = null
		) =>
			DoMapAsync(@this, map, handler);
	}
}
