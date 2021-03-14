// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: Return
	/// </summary>
	public static class ObjectExtensions_Return
	{
		/// <inheritdoc cref="F.OptionF.Return{T}(T, bool)"/>
		public static Option<T> Return<T>(this T @this) =>
			F.OptionF.Return(@this);

		/// <inheritdoc cref="F.OptionF.Return{T}(T, bool)"/>
		public static Option<T> Return<T>(this T @this, bool allowNull) =>
			F.OptionF.Return(@this, allowNull);
	}
}
