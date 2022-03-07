// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: FilterAsync
/// </summary>
public static class OptionExtensionsFilterAsync
{
	/// <inheritdoc cref="F.MaybeF.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public static Task<Maybe<T>> FilterAsync<T>(this Task<Maybe<T>> @this, Func<T, bool> predicate) =>
		F.MaybeF.FilterAsync(@this, x => Task.FromResult(predicate(x)));

	/// <inheritdoc cref="F.MaybeF.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public static Task<Maybe<T>> FilterAsync<T>(this Task<Maybe<T>> @this, Func<T, Task<bool>> predicate) =>
		F.MaybeF.FilterAsync(@this, predicate);
}
