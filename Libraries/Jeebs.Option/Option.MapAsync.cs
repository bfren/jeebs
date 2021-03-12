// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static JeebsF.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Use <paramref name="map"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="map">Mapping function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		/// <param name="handler">[Optional] Exception handler</param>
		internal Task<Option<U>> DoMapAsync<U>(Func<T, Task<U>> map, Handler? handler) =>
			CatchAsync(() =>
				Switch(
					some: async v => Return(await map(v)),
					none: r => new None<U>(r).AsTask
				),
				handler
			);

		/// <inheritdoc cref="DoMapAsync{U}(Func{T, Task{U}}, OptionF.Handler?)"/>
		public Task<Option<U>> MapAsync<U>(Func<T, Task<U>> map, Handler? handler = null) =>
			DoMapAsync(map, handler);
	}
}
