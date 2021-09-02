// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;

namespace F;

public static partial class OptionF
{
	/// <inheritdoc cref="Filter{T}(Option{T}, Func{T, bool})"/>
	public static Task<Option<T>> FilterAsync<T>(Option<T> option, Func<T, Task<bool>> predicate) =>
		BindAsync(
			option,
			async x =>
				await predicate(x) switch
				{
					true =>
						Some(x),

					false =>
						None<T, Msg.FilterPredicateWasFalseMsg>()
				}
		);

	/// <inheritdoc cref="Filter{T}(Option{T}, Func{T, bool})"/>
	public static async Task<Option<T>> FilterAsync<T>(Task<Option<T>> option, Func<T, Task<bool>> predicate) =>
		await FilterAsync(await option, predicate);
}
