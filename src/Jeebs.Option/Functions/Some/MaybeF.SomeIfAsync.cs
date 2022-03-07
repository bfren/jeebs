// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(Func<bool> predicate, Func<Task<T>> value, Handler handler) =>
		CatchAsync(() =>
			predicate() switch
			{
				true =>
					SomeAsync(value, handler),

				false =>
					None<T, M.PredicateWasFalseMsg>().AsTask
			},
			handler
		);

	/// <inheritdoc cref="SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(Func<T, bool> predicate, Func<Task<T>> value, Handler handler) =>
		CatchAsync(
			async () =>
			{
				var v = await value().ConfigureAwait(false);
				return predicate(v) switch
				{
					true =>
						Some(v),

					false =>
						None<T, M.PredicateWasFalseMsg>()
				};
			},
			handler
		);

	/// <inheritdoc cref="SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(Func<bool> predicate, Task<T> value, Handler handler) =>
		SomeIfAsync(predicate, () => value, handler);

	/// <inheritdoc cref="SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(Func<T, bool> predicate, Task<T> value, Handler handler) =>
		SomeIfAsync(predicate, () => value, handler);
}
