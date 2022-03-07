// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// Unwrap the value of <paramref name="maybe"/> - if it is a <see cref="Jeebs.Internals.Some{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="U">Single value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="unwrap">Fluent unwrap function</param>
	public static async Task<U> UnwrapAsync<T, U>(
		Task<Maybe<T>> maybe,
		Func<FluentUnwrapAsync<T>, U> unwrap
	) =>
		unwrap(new FluentUnwrapAsync<T>(await maybe.ConfigureAwait(false)));

	/// <summary>
	/// Fluent unwrapper
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	public class FluentUnwrapAsync<T>
	{
		private readonly Maybe<T> maybe;

		internal FluentUnwrapAsync(Maybe<T> maybe) =>
			this.maybe = maybe;

		/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{Msg, T})"/>
		public T Value(T ifNone) =>
			Unwrap(maybe, ifNone: _ => ifNone);

		/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{Msg, T})"/>
		public T Value(Func<T> ifNone) =>
			Unwrap(maybe, ifNone: _ => ifNone());

		/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{Msg, T})"/>
		public T Value(Func<Msg, T> ifNone) =>
			Unwrap(maybe, ifNone);

		/// <inheritdoc cref="UnwrapSingle{T, U}(Maybe{T}, Func{Msg}?, Func{Msg}?, Func{Msg}?)"/>
		public Maybe<U> Single<U>(Func<Msg>? noItems = null, Func<Msg>? tooMany = null, Func<Msg>? notAList = null) =>
			UnwrapSingle<T, U>(maybe, noItems, tooMany, notAList);
	}
}
