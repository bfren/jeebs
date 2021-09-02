﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: IfSomeAsync
	/// </summary>
	public static class OptionExtensions_IfSomeAsync
	{
		/// <inheritdoc cref="F.OptionF.IfSomeAsync{T}(Task{Option{T}}, Func{T, Task})"/>
		public static Task<Option<T>> IfSomeAsync<T>(this Task<Option<T>> @this, Action<T> ifSome) =>
			F.OptionF.IfSomeAsync(@this, x => { ifSome(x); return Task.CompletedTask; });

		/// <inheritdoc cref="F.OptionF.IfSomeAsync{T}(Task{Option{T}}, Func{T, Task})"/>
		public static Task<Option<T>> IfSomeAsync<T>(this Task<Option<T>> @this, Func<T, Task> ifSome) =>
			F.OptionF.IfSomeAsync(@this, ifSome);
	}
}
