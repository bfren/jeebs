// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: MapAsync
	/// </summary>
	public static class OptionExtensions_MapAsync
	{
		/// <inheritdoc cref="F.OptionF.MapAsync{T, U}(Option{T}, Func{T, Task{U}}, Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, U> map,
			Handler? handler = null
		) =>
			F.OptionF.MapAsync(@this, x => Task.FromResult(map(x)), handler);

		/// <inheritdoc cref="F.OptionF.MapAsync{T, U}(Option{T}, Func{T, Task{U}}, Handler?)"/>
		public static Task<Option<U>> MapAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Task<U>> map,
			Handler? handler = null
		) =>
			F.OptionF.MapAsync(@this, map, handler);
	}
}
