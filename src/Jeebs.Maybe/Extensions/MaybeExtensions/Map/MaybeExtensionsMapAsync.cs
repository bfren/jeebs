﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using static F.MaybeF;

namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: MapAsync
/// </summary>
public static class OptionExtensionsMapAsync
{
	/// <inheritdoc cref="F.MaybeF.MapAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Handler)"/>
	public static Task<Maybe<TReturn>> MapAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> map, Handler handler) =>
		F.MaybeF.MapAsync(@this, x => Task.FromResult(map(x)), handler);

	/// <inheritdoc cref="F.MaybeF.MapAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Handler)"/>
	public static Task<Maybe<TReturn>> MapAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> map, Handler handler) =>
		F.MaybeF.MapAsync(@this, map, handler);
}
