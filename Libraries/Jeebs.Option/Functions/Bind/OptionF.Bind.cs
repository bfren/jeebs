// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Use <paramref name="bind"/> to convert the value of <paramref name="option"/> to type <typeparamref name="U"/>,
		/// if it is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="bind">Binding function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		/// <param name="handler">[Optional] Exception handler</param>
		public static Option<U> Bind<T, U>(Option<T> option, Func<T, Option<U>> bind, Handler? handler) =>
			Catch(() =>
				Switch(
					option,
					some: v => bind(v),
					none: r => new None<U>(r)
				),
				handler
			);

		/// <inheritdoc cref="Bind{T, U}(Option{T}, Func{T, Option{U}}, Handler?)"/>
		public static Option<T> Bind<T>(Func<Option<T>> bind) =>
			Bind(True, _ => bind(), null);

		/// <inheritdoc cref="Bind{T, U}(Option{T}, Func{T, Option{U}}, Handler?)"/>
		public static Option<T> Bind<T>(Func<Option<T>> bind, Handler? handler) =>
			Bind(True, _ => bind(), handler);
	}
}
