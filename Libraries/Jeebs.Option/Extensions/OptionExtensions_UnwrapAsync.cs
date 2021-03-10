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
		/// <param name="this">Option value (awaitable)</param>
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

			/// <summary>
			/// Unwrap value
			/// </summary>
			public Option<T> Value(Func<T> ifNone) =>
				option.Unwrap(ifNone);

			/// <summary>
			/// Unwrap single value (only works if <typeparamref name="T"/> implements IEnumerable
			/// </summary>
			/// <typeparam name="U">Next option type</typeparam>
			public Option<U> Single<U>(Func<IMsg>? tooMany = null, Func<IMsg>? notAList = null) =>
				option.UnwrapSingle<U>(tooMany, notAList);
		}
	}
}
