// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Internals;

namespace F;

public static partial class OptionF
{
	/// <inheritdoc cref="IfSome{T}(Option{T}, Action{T})"/>
	public static Task<Option<T>> IfSomeAsync<T>(Option<T> option, Func<T, Task> ifSome) =>
		CatchAsync(async () =>
			{
				if (option is Some<T> some)
				{
					await ifSome(some.Value).ConfigureAwait(false);
				}

				return option;
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfSome{T}(Option{T}, Action{T})"/>
	public static async Task<Option<T>> IfSomeAsync<T>(Task<Option<T>> option, Func<T, Task> ifSome) =>
		await IfSomeAsync(await option.ConfigureAwait(false), ifSome).ConfigureAwait(false);
}
