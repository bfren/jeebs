// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: BindAsync
	/// </summary>
	public static class OptionExtensions_BindAsync
	{
		/// <inheritdoc cref="F.OptionF.BindAsync{T, U}(Option{T}, Func{T, Task{Option{U}}}, Handler?)"/>
		public static Task<Option<U>> BindAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Option<U>> bind,
			Handler? handler = null
		) =>
			F.OptionF.BindAsync(@this, x => Task.FromResult(bind(x)), handler);

		/// <inheritdoc cref="F.OptionF.BindAsync{T, U}(Option{T}, Func{T, Task{Option{U}}}, Handler?)"/>
		public static Task<Option<U>> BindAsync<T, U>(
			this Task<Option<T>> @this,
			Func<T, Task<Option<U>>> bind,
			Handler? handler = null
		) =>
			F.OptionF.BindAsync(@this, bind, handler);
	}
}
