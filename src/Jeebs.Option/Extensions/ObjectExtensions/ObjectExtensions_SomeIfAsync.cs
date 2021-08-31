// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: ReturnIfAsync
	/// </summary>
	public static class ObjectExtensions_SomeIfAsync
	{
		/// <inheritdoc cref="F.OptionF.SomeIfAsync{T}(Func{bool}, Func{Task{T}}, Handler)"/>
		public static Task<Option<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<bool> predicate, Handler handler) =>
			F.OptionF.SomeIfAsync(predicate, @this, handler);

		/// <inheritdoc cref="F.OptionF.SomeIfAsync{T}(Func{bool}, Task{T}, Handler)"/>
		public static Task<Option<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<T, bool> predicate, Handler handler) =>
			F.OptionF.SomeIfAsync(predicate, @this, handler);
	}
}
