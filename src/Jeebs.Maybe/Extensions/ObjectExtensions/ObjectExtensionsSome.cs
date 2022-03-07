// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Object Extensions: Return
/// </summary>
public static class ObjectExtensionsSome
{
	/// <inheritdoc cref="F.MaybeF.Some{T}(T, bool)"/>
	public static Maybe<T> Some<T>(this T @this) =>
		F.MaybeF.Some(@this);

	/// <inheritdoc cref="F.MaybeF.Some{T}(T, bool)"/>
	public static Maybe<T?> Some<T>(this T @this, bool allowNull) =>
		F.MaybeF.Some(@this, allowNull);
}
