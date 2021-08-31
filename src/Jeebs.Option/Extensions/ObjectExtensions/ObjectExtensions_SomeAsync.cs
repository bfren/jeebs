﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: ReturnAsync
	/// </summary>
	public static class ObjectExtensions_SomeAsync
	{
		/// <inheritdoc cref="F.OptionF.SomeAsync{T}(Func{Task{T}}, Handler)"/>
		public static Task<Option<T>> SomeAsync<T>(this Func<Task<T>> @this, Handler handler) =>
			F.OptionF.SomeAsync(@this, handler);

		/// <inheritdoc cref="F.OptionF.SomeAsync{T}(Func{Task{T}}, bool, Handler)"/>
		public static Task<Option<T?>> SomeAsync<T>(this Func<Task<T?>> @this, bool allowNull, Handler handler) =>
			F.OptionF.SomeAsync(@this, allowNull, handler);
	}
}