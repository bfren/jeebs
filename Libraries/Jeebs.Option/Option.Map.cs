// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static F.OptionF;

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
		internal Option<U> DoMap<U>(Func<T, U> map, Handler? handler = null) =>
			Catch(() =>
				Switch(
					some: v => Return(map(v)),
					none: r => new None<U>(r)
				),
				handler
			);

		/// <inheritdoc cref="DoMap{U}(Func{T, U}, OptionF.Handler?)"/>
		public Option<U> Map<U>(Func<T, U> map, Handler? handler = null) =>
			DoMap(map, handler);
	}
}
