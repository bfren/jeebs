// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Internals;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="IfSome{T}(Maybe{T}, Action{T})"/>
	public static Task<Maybe<T>> IfSomeAsync<T>(Maybe<T> maybe, Func<T, Task> ifSome) =>
		CatchAsync(async () =>
			{
				if (maybe is Some<T> some)
				{
					await ifSome(some.Value).ConfigureAwait(false);
				}

				return maybe;
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfSome{T}(Maybe{T}, Action{T})"/>
	public static async Task<Maybe<T>> IfSomeAsync<T>(Task<Maybe<T>> maybe, Func<T, Task> ifSome) =>
		await IfSomeAsync(await maybe.ConfigureAwait(false), ifSome).ConfigureAwait(false);
}
