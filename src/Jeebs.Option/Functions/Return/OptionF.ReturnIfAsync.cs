// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="ReturnIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Task<Option<T>> ReturnIfAsync<T>(Func<bool> predicate, Func<Task<T>> value, Handler handler) =>
			CatchAsync(() =>
				predicate() switch
				{
					true =>
						ReturnAsync(value, handler),

					false =>
						None<T, Msg.PredicateWasFalseMsg>().AsTask
				},
				handler
			);

		/// <inheritdoc cref="ReturnIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Task<Option<T>> ReturnIfAsync<T>(Func<T, bool> predicate, Func<Task<T>> value, Handler handler) =>
			CatchAsync(
				async () =>
				{
					var v = await value();
					return predicate(v) switch
					{
						true =>
							Return(v),

						false =>
							None<T, Msg.PredicateWasFalseMsg>()
					};
				},
				handler
			);

		/// <inheritdoc cref="ReturnIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Task<Option<T>> ReturnIfAsync<T>(Func<bool> predicate, Task<T> value, Handler handler) =>
			ReturnIfAsync(predicate, () => value, handler);

		/// <inheritdoc cref="ReturnIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Task<Option<T>> ReturnIfAsync<T>(Func<T, bool> predicate, Task<T> value, Handler handler) =>
			ReturnIfAsync(predicate, () => value, handler);
	}
}
