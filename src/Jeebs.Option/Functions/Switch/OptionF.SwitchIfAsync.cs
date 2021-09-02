// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, Option{T}}?, Func{T, Option{T}}?)"/>
		public static async Task<Option<T>> SwitchIfAsync<T>(Task<Option<T>> option, Func<T, bool> check, Func<T, Option<T>>? ifTrue, Func<T, Option<T>>? ifFalse) =>
			SwitchIf(await option, check, ifTrue, ifFalse);

		/// <inheritdoc cref="SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, IMsg})"/>
		public static async Task<Option<T>> SwitchIfAsync<T>(Task<Option<T>> option, Func<T, bool> check, Func<T, IMsg> ifFalse) =>
			SwitchIf(await option, check, ifFalse);
	}
}
