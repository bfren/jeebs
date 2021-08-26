// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
