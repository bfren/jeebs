// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using static F.MaybeF;

namespace Jeebs;

/// <summary>
/// Object Extensions: ReturnIf
/// </summary>
public static class ObjectExtensionsSomeIf
{
	/// <inheritdoc cref="F.MaybeF.SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
	public static Maybe<T> SomeIf<T>(this T @this, Func<bool> predicate, Handler handler) =>
		F.MaybeF.SomeIf(predicate, @this, handler);

	/// <inheritdoc cref="F.MaybeF.SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
	public static Maybe<T> SomeIf<T>(this T @this, Func<T, bool> predicate, Handler handler) =>
		F.MaybeF.SomeIf(() => predicate(@this), @this, handler);
}
