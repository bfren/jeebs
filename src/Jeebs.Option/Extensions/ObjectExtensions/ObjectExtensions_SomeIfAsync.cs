// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs;

/// <summary>
/// Object Extensions: ReturnIfAsync
/// </summary>
public static class ObjectExtensionsSomeIfAsync
{
	/// <inheritdoc cref="F.OptionF.SomeIfAsync{T}(Func{bool}, Func{Task{T}}, Handler)"/>
	public static Task<Option<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<bool> predicate, Handler handler) =>
		F.OptionF.SomeIfAsync(predicate, @this, handler);

	/// <inheritdoc cref="F.OptionF.SomeIfAsync{T}(Func{bool}, Task{T}, Handler)"/>
	public static Task<Option<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<T, bool> predicate, Handler handler) =>
		F.OptionF.SomeIfAsync(predicate, @this, handler);
}
