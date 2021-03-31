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
		public static Task<Option<T>> IfNullAsync<T>(Option<T> option, Func<Task<Option<T>>> nullValue) =>
			CatchAsync(() =>
				option switch
				{
					Some<T> x when x.Value is null =>
						nullValue(),

					None<T> x when x.Reason is Msg.NullValueMsg =>
						nullValue(),

					_ =>
						option.AsTask
				},
				DefaultHandler
			);

		/// <inheritdoc cref="IfNull{T}(Option{T}, Func{Option{T}})"/>
		public static async Task<Option<T>> IfNullAsync<T>(Task<Option<T>> option, Func<Task<Option<T>>> nullValue) =>
			await IfNullAsync(await option, nullValue);
	}
}
