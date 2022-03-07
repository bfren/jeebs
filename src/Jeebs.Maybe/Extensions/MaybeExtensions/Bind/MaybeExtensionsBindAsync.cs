﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: BindAsync
/// </summary>
public static class OptionExtensionsBindAsync
{
	/// <inheritdoc cref="F.MaybeF.BindAsync{T, U}(Maybe{T}, Func{T, Task{Maybe{U}}})"/>
	public static Task<Maybe<U>> BindAsync<T, U>(this Task<Maybe<T>> @this, Func<T, Maybe<U>> bind) =>
		F.MaybeF.BindAsync(@this, x => Task.FromResult(bind(x)));

	/// <inheritdoc cref="F.MaybeF.BindAsync{T, U}(Maybe{T}, Func{T, Task{Maybe{U}}})"/>
	public static Task<Maybe<U>> BindAsync<T, U>(this Task<Maybe<T>> @this, Func<T, Task<Maybe<U>>> bind) =>
		F.MaybeF.BindAsync(@this, bind);
}