// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using static F.MaybeF;

namespace Jeebs;

/// <summary>
/// Object Extensions: ReturnIfAsync
/// </summary>
public static class ObjectExtensionsSomeIfAsync
{
	/// <inheritdoc cref="F.MaybeF.SomeIfAsync{T}(Func{bool}, Func{Task{T}}, Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<bool> predicate, Handler handler) =>
		F.MaybeF.SomeIfAsync(predicate, @this, handler);

	/// <inheritdoc cref="F.MaybeF.SomeIfAsync{T}(Func{bool}, Task{T}, Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<T, bool> predicate, Handler handler) =>
		F.MaybeF.SomeIfAsync(predicate, @this, handler);
}
