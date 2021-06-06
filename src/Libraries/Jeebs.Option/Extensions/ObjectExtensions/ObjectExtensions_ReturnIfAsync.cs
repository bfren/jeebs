// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: ReturnIfAsync
	/// </summary>
	public static class ObjectExtensions_ReturnIfAsync
	{
		/// <inheritdoc cref="F.OptionF.ReturnIfAsync{T}(Func{bool}, Func{Task{T}}, Handler)"/>
		public static Task<Option<T>> ReturnIfAsync<T>(this Func<Task<T>> @this, Func<bool> predicate, Handler handler) =>
			F.OptionF.ReturnIfAsync(predicate, @this, handler);

		/// <inheritdoc cref="F.OptionF.ReturnIfAsync{T}(Func{bool}, Task{T}, Handler)"/>
		public static Task<Option<T>> ReturnIfAsync<T>(this Func<Task<T>> @this, Func<T, bool> predicate, Handler handler) =>
			F.OptionF.ReturnIfAsync(predicate, @this, handler);
	}
}
