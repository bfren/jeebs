// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(Option<T> option, Func<T, Task<U>> map, Handler? handler) =>
			CatchAsync(() =>
				Switch(
					option,
					some: async v => Return(await map(v)),
					none: r => new None<U>(r).AsTask
				),
				handler
			);

		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler?)"/>
		public static async Task<Option<U>> MapAsync<T, U>(Task<Option<T>> option, Func<T, Task<U>> map, Handler? handler) =>
			await MapAsync(await option, map, handler);

		/// <inheritdoc cref="Map{T}(Func{T}, Handler?)"/>
		public static Task<Option<T>> MapAsync<T>(Func<Task<T>> map, Handler? handler) =>
			MapAsync(True, _ => map(), handler);
	}
}
