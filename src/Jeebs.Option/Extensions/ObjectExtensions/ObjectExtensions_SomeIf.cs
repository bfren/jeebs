﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: ReturnIf
	/// </summary>
	public static class ObjectExtensions_SomeIf
	{
		/// <inheritdoc cref="F.OptionF.SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Option<T> SomeIf<T>(this T @this, Func<bool> predicate, Handler handler) =>
			F.OptionF.SomeIf(predicate, @this, handler);

		/// <inheritdoc cref="F.OptionF.SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Option<T> SomeIf<T>(this T @this, Func<T, bool> predicate, Handler handler) =>
			F.OptionF.SomeIf(() => predicate(@this), @this, handler);
	}
}