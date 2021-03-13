// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: MapAsync
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <inheritdoc cref="Option{T}.DoMapAsync{U}(Func{T, Task{U}}, Handler?)"/>
		/// <param name="this">Option (awaitable)</param>
		public static async Task<Option<U>> DoMapAsync<T, U>(
			Task<Option<T>> @this,
			Func<T, Task<U>> map,
			Handler? handler
		) =>
			await (await @this).DoMapAsync(map, handler);

		/// <inheritdoc cref="DoMapAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, U> map,
			Handler? handler = null
		) =>
			DoMapAsync(@this, x => Task.FromResult(map(x)), handler);

		/// <inheritdoc cref="DoMapAsync{T, U}(Task{Option{T}}, Func{T, Task{U}}, Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Task<U>> map,
			Handler? handler = null
		) =>
			DoMapAsync(@this, map, handler);
	}
}
