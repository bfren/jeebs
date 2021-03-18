// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: ReturnIf
	/// </summary>
	public static class ObjectExtensions_ReturnIf
	{
		/// <inheritdoc cref="F.OptionF.ReturnIf{T}(Func{bool}, Func{T}, F.OptionF.Handler)"/>
		public static Option<T> ReturnIf<T>(this T @this, Func<bool> predicate) =>
			F.OptionF.ReturnIf(predicate, @this);

		/// <inheritdoc cref="F.OptionF.ReturnIf{T}(Func{bool}, Func{T}, F.OptionF.Handler)"/>
		public static Option<T> ReturnIf<T>(this T @this, Func<T, bool> predicate) =>
			F.OptionF.ReturnIf(() => predicate(@this), @this);
	}
}
