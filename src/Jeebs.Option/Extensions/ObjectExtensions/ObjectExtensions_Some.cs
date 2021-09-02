// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: Return
	/// </summary>
	public static class ObjectExtensions_Some
	{
		/// <inheritdoc cref="F.OptionF.Some{T}(T, bool)"/>
		public static Option<T> Some<T>(this T @this) =>
			F.OptionF.Some(@this);

		/// <inheritdoc cref="F.OptionF.Some{T}(T, bool)"/>
		public static Option<T?> Some<T>(this T @this, bool allowNull) =>
			F.OptionF.Some(@this, allowNull);
	}
}
