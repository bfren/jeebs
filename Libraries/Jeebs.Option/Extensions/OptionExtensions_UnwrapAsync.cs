// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: UnwrapAsync
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <summary>
		/// Unwrap the single value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="this">Option (awaitable)</param>
		/// <param name="unwrap">Fluent unwrap function</param>
		public static async Task<Option<U>> UnwrapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<FluentUnwrapAsync<T>, Option<U>> unwrap
		) =>
			unwrap(new FluentUnwrapAsync<T>(await @this));

		/// <summary>
		/// Fluent unwrapper
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public class FluentUnwrapAsync<T>
		{
			private readonly Option<T> option;

			internal FluentUnwrapAsync(Option<T> option) =>
				this.option = option;

			/// <inheritdoc cref="Option{T}.Unwrap(T)"/>
			public Option<T> Value(T ifNone) =>
				option.Unwrap(ifNone);

			/// <inheritdoc cref="Option{T}.Unwrap(Func{T})"/>
			public Option<T> Value(Func<T> ifNone) =>
				option.Unwrap(ifNone);

			/// <inheritdoc cref="Option{T}.Unwrap(Func{IMsg?, T})"/>
			public Option<T> Value(Func<IMsg?, T> ifNone) =>
				option.Unwrap(ifNone);

			/// <inheritdoc cref="Option{T}.DoUnwrapSingle{U}(Func{IMsg}?, Func{IMsg}?, Func{IMsg}?)"/>
			public Option<U> Single<U>(Func<IMsg>? noItems = null, Func<IMsg>? tooMany = null, Func<IMsg>? notAList = null) =>
				option.DoUnwrapSingle<U>(noItems, tooMany, notAList);
		}
	}
}
