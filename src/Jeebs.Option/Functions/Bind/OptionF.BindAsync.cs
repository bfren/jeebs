// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="Bind{T, U}(Option{T}, Func{T, Option{U}})"/>
		public static Task<Option<U>> BindAsync<T, U>(Option<T> option, Func<T, Task<Option<U>>> bind) =>
			CatchAsync(() =>
				Switch(
					option,
					some: v => bind(v),
					none: r => new None<U>(r).AsTask
				),
				DefaultHandler
			);

		/// <inheritdoc cref="Bind{T, U}(Option{T}, Func{T, Option{U}})"/>
		public static async Task<Option<U>> BindAsync<T, U>(Task<Option<T>> option, Func<T, Task<Option<U>>> bind) =>
			await BindAsync(await option, bind);
	}
}
