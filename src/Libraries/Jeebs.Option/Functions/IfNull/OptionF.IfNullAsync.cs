// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="IfNull{T}(Option{T}, Func{Option{T}})"/>
		public static Task<Option<T>> IfNullAsync<T>(Option<T> option, Func<Task<Option<T>>> ifNull) =>
			CatchAsync(() =>
				option switch
				{
					Some<T> x when x.Value is null =>
						ifNull(),

					None<T> x when x.Reason is Msg.NullValueMsg =>
						ifNull(),

					_ =>
						option.AsTask
				},
				DefaultHandler
			);

		/// <inheritdoc cref="IfNull{T}(Option{T}, Func{Option{T}})"/>
		public static async Task<Option<T>> IfNullAsync<T>(Task<Option<T>> option, Func<Task<Option<T>>> ifNull) =>
			await IfNullAsync(await option, ifNull);

		/// <inheritdoc cref="IfNull{T, TMsg}(Option{T}, Func{TMsg})"/>
		public static async Task<Option<T>> IfNullAsync<T, TMsg>(Task<Option<T>> option, Func<TMsg> ifNull)
			where TMsg : IMsg =>
			await IfNullAsync(await option, () => None<T>(ifNull()).AsTask);
	}
}
