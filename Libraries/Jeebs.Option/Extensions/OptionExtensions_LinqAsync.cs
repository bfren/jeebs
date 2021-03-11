// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jm.Option;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: Linq Async Methods
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <inheritdoc cref="Select{T, U}(Option{T}, Func{T, U})"/>
		public static Task<Option<U>> Select<T, U>(this Task<Option<T>> @this, Func<T, U> f) =>
			@this.MapAsync(f);

		/// <inheritdoc cref="Select{T, U}(Option{T}, Func{T, U})"/>
		public static Task<Option<U>> Select<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> f) =>
			@this.MapAsync(f);

		/// <inheritdoc cref="SelectMany{T, U, V}(Option{T}, Func{T, Option{U}}, Func{T, U, V})"/>
		public static Task<Option<V>> SelectMany<T, U, V>(this Option<T> @this, Func<T, Task<Option<U>>> f, Func<T, U, V> g) =>
			@this.BindAsync(x => f(x).MapAsync(y => g(x, y)));

		/// <inheritdoc cref="SelectMany{T, U, V}(Option{T}, Func{T, Option{U}}, Func{T, U, V})"/>
		public static Task<Option<V>> SelectMany<T, U, V>(this Task<Option<T>> @this, Func<T, Option<U>> f, Func<T, U, V> g) =>
			@this.BindAsync(x => f(x).Map(y => g(x, y)));

		/// <inheritdoc cref="SelectMany{T, U, V}(Option{T}, Func{T, Option{U}}, Func{T, U, V})"/>
		public static Task<Option<V>> SelectMany<T, U, V>(this Task<Option<T>> @this, Func<T, Task<Option<U>>> f, Func<T, U, V> g) =>
			@this.BindAsync(x => f(x).MapAsync(y => g(x, y)));

		/// <inheritdoc cref="Where{T}(Option{T}, Func{T, bool})"/>
		public static Task<Option<T>> Where<T>(this Task<Option<T>> @this, Func<T, bool> predicate) =>
			@this.BindAsync(x => predicate(x) switch
			{
				true =>
					@this,

				false =>
					Task.FromResult(Option.None<T>(new PredicateWasFalseMsg()).AsOption)
			});
	}
}
