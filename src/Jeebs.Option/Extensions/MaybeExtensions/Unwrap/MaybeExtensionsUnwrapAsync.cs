// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using static F.MaybeF;

namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: UnwrapAsync
/// </summary>
public static class OptionExtensionsUnwrapAsync
{
	/// <inheritdoc cref="F.MaybeF.UnwrapAsync{T, U}(Task{Maybe{T}}, Func{FluentUnwrapAsync{T}, U})"/>
	public static Task<U> UnwrapAsync<T, U>(this Task<Maybe<T>> @this, Func<FluentUnwrapAsync<T>, U> unwrap) =>
		F.MaybeF.UnwrapAsync(@this, unwrap);
}
