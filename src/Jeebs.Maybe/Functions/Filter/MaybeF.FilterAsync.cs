// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="Filter{T}(Maybe{T}, Func{T, bool})"/>
	public static Task<Maybe<T>> FilterAsync<T>(Maybe<T> maybe, Func<T, Task<bool>> predicate) =>
		BindAsync(
			maybe,
			async x =>
				await predicate(x).ConfigureAwait(false) switch
				{
					true =>
						Some(x),

					false =>
						None<T, M.FilterPredicateWasFalseMsg>()
				}
		);

	/// <inheritdoc cref="Filter{T}(Maybe{T}, Func{T, bool})"/>
	public static async Task<Maybe<T>> FilterAsync<T>(Task<Maybe<T>> maybe, Func<T, Task<bool>> predicate) =>
		await FilterAsync(await maybe.ConfigureAwait(false), predicate).ConfigureAwait(false);
}
