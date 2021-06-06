// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: FilterAsync
	/// </summary>
	public static class OptionExtensions_FilterAsync
	{
		/// <inheritdoc cref="F.OptionF.FilterAsync{T}(Option{T}, Func{T, Task{bool}})"/>
		public static Task<Option<T>> FilterAsync<T>(this Task<Option<T>> @this, Func<T, bool> predicate) =>
			F.OptionF.FilterAsync(@this, x => Task.FromResult(predicate(x)));

		/// <inheritdoc cref="F.OptionF.FilterAsync{T}(Option{T}, Func{T, Task{bool}})"/>
		public static Task<Option<T>> FilterAsync<T>(this Task<Option<T>> @this, Func<T, Task<bool>> predicate) =>
			F.OptionF.FilterAsync(@this, predicate);
	}
}
