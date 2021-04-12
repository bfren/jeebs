// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, Option{T}})"/>
		public static async Task<Option<T>> SwitchIfAsync<T>(Task<Option<T>> option, Func<T, bool> check, Func<T, Option<T>> ifFalse) =>
			SwitchIf(await option, check, ifFalse);

		/// <inheritdoc cref="SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, Option{T}})"/>
		public static async Task<Option<T>> SwitchIfAsync<T>(Task<Option<T>> option, Func<T, bool> check, Func<T, IMsg> ifFalse) =>
			SwitchIf(await option, check, ifFalse);
	}
}
