// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="Match{T, U}(Option{T}, Func{T, U}, Func{IMsg, U})"/>
		public static Task<U> MatchAsync<T, U>(Option<T> option, Func<T, Task<U>> some, Func<IMsg, Task<U>> none) =>
			Switch(
				option,
				some: v => some(v),
				none: r => none(r)
			);

		/// <inheritdoc cref="Match{T, U}(Option{T}, Func{T, U}, Func{IMsg, U})"/>
		public static async Task<U> MatchAsync<T, U>(Task<Option<T>> option, Func<T, Task<U>> some, Func<IMsg, Task<U>> none) =>
			await MatchAsync(await option, some, none);
	}
}
