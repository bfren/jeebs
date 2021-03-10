// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

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
		private Option<U> MapPrivate<U>(Func<T, U> map, Option.Handler? handler = null) =>
			Option.Catch(() =>
				SwitchFunc(
					some: v => Option.Wrap(map(v)),
					none: r => Option.None<U>(r)
				),
				handler
			);

		/// <inheritdoc cref="MapPrivate{U}(Func{T, U}, Option.Handler?)"/>
		public Option<U> Map<U>(Func<U> map, Option.Handler? handler = null) =>
			MapPrivate(_ => map(), handler);

		/// <inheritdoc cref="MapPrivate{U}(Func{T, U}, Option.Handler?)"/>
		public Option<U> Map<U>(Func<T, U> map, Option.Handler? handler = null) =>
			MapPrivate(map, handler);
	}
}
