// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public abstract partial record Option<T>
	{
		/// <summary>
		/// Use <paramref name="map"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="map">Mapping function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		/// <param name="handler">[Optional] Exception handler</param>
		internal Task<Option<U>> DoMapAsync<U>(Func<T, Task<U>> map, Option.Handler? handler = null) =>
			Option.CatchAsync(() =>
				Switch(
					some: async v => Option.Wrap(await map(v)),
					none: async r => (Option<U>)new None<U>(r)
				),
				handler
			);

		/// <inheritdoc cref="DoMapAsync{U}(Func{T, Task{U}}, Option.Handler?)"/>
		public Task<Option<U>> MapAsync<U>(Func<Task<U>> map, Option.Handler? handler = null) =>
			DoMapAsync(_ => map(), handler);

		/// <inheritdoc cref="DoMapAsync{U}(Func{T, Task{U}}, Option.Handler?)"/>
		public Task<Option<U>> MapAsync<U>(Func<T, Task<U>> map, Option.Handler? handler = null) =>
			DoMapAsync(map, handler);
	}
}
