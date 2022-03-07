// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: IfSomeAsync
/// </summary>
public static class OptionExtensionsIfSomeAsync
{
	/// <inheritdoc cref="F.MaybeF.IfSomeAsync{T}(Task{Maybe{T}}, Func{T, Task})"/>
	public static Task<Maybe<T>> IfSomeAsync<T>(this Task<Maybe<T>> @this, Action<T> ifSome) =>
		F.MaybeF.IfSomeAsync(@this, x => { ifSome(x); return Task.CompletedTask; });

	/// <inheritdoc cref="F.MaybeF.IfSomeAsync{T}(Task{Maybe{T}}, Func{T, Task})"/>
	public static Task<Maybe<T>> IfSomeAsync<T>(this Task<Maybe<T>> @this, Func<T, Task> ifSome) =>
		F.MaybeF.IfSomeAsync(@this, ifSome);
}
