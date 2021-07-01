// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: SwitchIfAsync
	/// </summary>
	public static class OptionExtensions_SwitchIfAsync
	{
		/// <inheritdoc cref="F.OptionF.SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, Option{T}}?, Func{T, Option{T}}?)"/>
		public static Task<Option<T>> SwitchIfAsync<T>(
			this Task<Option<T>> @this,
			Func<T, bool> check,
			Func<T, Option<T>>? ifTrue = null,
			Func<T, Option<T>>? ifFalse = null
		) =>
			F.OptionF.SwitchIfAsync(@this, check, ifTrue, ifFalse);

		/// <inheritdoc cref="F.OptionF.SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, IMsg})"/>
		public static Task<Option<T>> SwitchIfAsync<T>(
			this Task<Option<T>> @this,
			Func<T, bool> check,
			Func<T, IMsg> ifFalse
		) =>
			F.OptionF.SwitchIfAsync(@this, check, ifFalse);
	}
}
