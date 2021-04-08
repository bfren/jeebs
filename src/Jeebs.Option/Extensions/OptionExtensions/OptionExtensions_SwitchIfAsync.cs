// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: SwitchIfAsync
	/// </summary>
	public static class OptionExtensions_SwitchIfAsync
	{
		/// <inheritdoc cref="F.OptionF.SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, None{T}})"/>
		public static Task<Option<T>> SwitchIfAsync<T>(this Task<Option<T>> @this, Func<T, bool> check, Func<T, None<T>> ifFalse) =>
			F.OptionF.SwitchIfAsync(@this, check, ifFalse);

		/// <inheritdoc cref="F.OptionF.SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, None{T}})"/>
		public static Task<Option<T>> SwitchIfAsync<T>(this Task<Option<T>> @this, Func<T, bool> check, Func<T, IMsg> ifFalse) =>
			F.OptionF.SwitchIfAsync(@this, check, ifFalse);
	}
}
