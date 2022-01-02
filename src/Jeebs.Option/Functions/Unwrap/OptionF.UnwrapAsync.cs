// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class OptionF
{
	/// <summary>
	/// Unwrap the value of <paramref name="option"/> - if it is a <see cref="Jeebs.Internals.Some{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <typeparam name="U">Single value type</typeparam>
	/// <param name="option">Input option</param>
	/// <param name="unwrap">Fluent unwrap function</param>
	public static async Task<U> UnwrapAsync<T, U>(
		Task<Option<T>> option,
		Func<FluentUnwrapAsync<T>, U> unwrap
	) =>
		unwrap(new FluentUnwrapAsync<T>(await option.ConfigureAwait(false)));

	/// <summary>
	/// Fluent unwrapper
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public class FluentUnwrapAsync<T>
	{
		private readonly Option<T> option;

		internal FluentUnwrapAsync(Option<T> option) =>
			this.option = option;

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{Msg, T})"/>
		public T Value(T ifNone) =>
			Unwrap(option, ifNone: _ => ifNone);

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{Msg, T})"/>
		public T Value(Func<T> ifNone) =>
			Unwrap(option, ifNone: _ => ifNone());

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{Msg, T})"/>
		public T Value(Func<Msg, T> ifNone) =>
			Unwrap(option, ifNone);

		/// <inheritdoc cref="UnwrapSingle{T, U}(Option{T}, Func{Msg}?, Func{Msg}?, Func{Msg}?)"/>
		public Option<U> Single<U>(Func<Msg>? noItems = null, Func<Msg>? tooMany = null, Func<Msg>? notAList = null) =>
			UnwrapSingle<T, U>(option, noItems, tooMany, notAList);
	}
}
