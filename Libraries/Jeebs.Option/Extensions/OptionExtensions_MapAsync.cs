﻿// Jeebs Rapid Application Development
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
		/// <inheritdoc cref="Option{T}.DoMapAsync{U}(Func{T, Task{U}}, Option.Handler?)"/>
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

		/// <inheritdoc cref="Option{T}.DoMapAsync{U}(Func{T, Task{U}}, Option.Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, U> map,
			Option.Handler? handler = null
		) =>
			DoMapAsync(@this, x => Task.FromResult(map(x)), handler);

		/// <inheritdoc cref="Option{T}.DoMapAsync{U}(Func{T, Task{U}}, Option.Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Task<U>> map,
			Option.Handler? handler = null
		) =>
			DoMapAsync(@this, map, handler);
	}
}
