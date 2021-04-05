// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: UnwrapAsync
	/// </summary>
	public static class OptionExtensions_UnwrapAsync
	{
		/// <inheritdoc cref="F.OptionF.UnwrapAsync{T, U}(Task{Option{T}}, Func{FluentUnwrapAsync{T}, U})"/>
		/// <param name="this">Option (awaitable)</param>
		public static Task<U> UnwrapAsync<T, U>(this Task<Option<T>> @this, Func<FluentUnwrapAsync<T>, U> unwrap) =>
			F.OptionF.UnwrapAsync(@this, unwrap);
	}
}
