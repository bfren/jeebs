﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Use <paramref name="map"/> to convert the value of <paramref name="option"/> to type <typeparamref name="U"/>,
		/// if it is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="map">Mapping function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		/// <param name="handler">[Optional] Exception handler</param>
		public static Option<U> Map<T, U>(Option<T> option, Func<T, U> map, Handler? handler) =>
			Catch(() =>
				Switch(
					option,
					some: v => Return(map(v)),
					none: r => new None<U>(r)
				),
				handler
			);

		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler?)"/>
		public static Option<T> Map<T>(Func<T> map) =>
			Map(True, _ => map(), null);

		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler?)"/>
		public static Option<T> Map<T>(Func<T> map, Handler handler) =>
			Map(True, _ => map(), handler);
	}
}