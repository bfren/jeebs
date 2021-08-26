// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: MapAsync
	/// </summary>
	public static class OptionExtensions_MapAsync
	{
		/// <inheritdoc cref="F.OptionF.MapAsync{T, U}(Option{T}, Func{T, Task{U}}, Handler)"/>
		public static Task<Option<U>> MapAsync<T, U>(this Task<Option<T>> @this, Func<T, U> map, Handler handler) =>
			F.OptionF.MapAsync(@this, x => Task.FromResult(map(x)), handler);

		/// <inheritdoc cref="F.OptionF.MapAsync{T, U}(Option{T}, Func{T, Task{U}}, Handler)"/>
		public static Task<Option<U>> MapAsync<T, U>(this Task<Option<T>> @this, Func<T, Task<U>> map, Handler handler) =>
			F.OptionF.MapAsync(@this, map, handler);
	}
}
