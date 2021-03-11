// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class Option
	{
		/// <summary>
		/// Map without an initial value (i.e. it's not a proper map
		/// but works like <see cref="Option{T}.Map{U}(Func{T, U}, Handler?)"/>
		/// except at the beginning of an <see cref="Option{T}"/> chain).
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="map">Mapping function - will always run</param>
		/// <param name="handler">[Optional] Exception handler</param>
		public static Option<T> Map<T>(Func<T> map, Handler? handler = null) =>
			True.Map(_ => map(), handler);

		/// <inheritdoc cref="Map{T}(Func{T}, Handler?)"/>
		public static Task<Option<T>> MapAsync<T>(Func<Task<T>> map, Handler? handler = null) =>
			True.MapAsync(_ => map(), handler);
	}
}
