// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: ReturnIf
	/// </summary>
	public static class ObjectExtensions_ReturnIf
	{
		/// <inheritdoc cref="F.OptionF.ReturnIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Option<T> ReturnIf<T>(this T @this, Func<bool> predicate, Handler handler) =>
			F.OptionF.ReturnIf(predicate, @this, handler);

		/// <inheritdoc cref="F.OptionF.ReturnIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Option<T> ReturnIf<T>(this T @this, Func<T, bool> predicate, Handler handler) =>
			F.OptionF.ReturnIf(() => predicate(@this), @this, handler);
	}
}
