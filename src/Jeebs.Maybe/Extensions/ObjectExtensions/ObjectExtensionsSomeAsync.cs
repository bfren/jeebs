// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using static F.MaybeF;

namespace Jeebs;

/// <summary>
/// Object Extensions: ReturnAsync
/// </summary>
public static class ObjectExtensionsSomeAsync
{
	/// <inheritdoc cref="F.MaybeF.SomeAsync{T}(Func{Task{T}}, Handler)"/>
	public static Task<Maybe<T>> SomeAsync<T>(this Func<Task<T>> @this, Handler handler) =>
		F.MaybeF.SomeAsync(@this, handler);

	/// <inheritdoc cref="F.MaybeF.SomeAsync{T}(Func{Task{T}}, bool, Handler)"/>
	public static Task<Maybe<T?>> SomeAsync<T>(this Func<Task<T?>> @this, bool allowNull, Handler handler) =>
		F.MaybeF.SomeAsync(@this, allowNull, handler);
}
