// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="Catch{T}(Func{Maybe{T}}, Handler?)"/>
	internal static async Task<Maybe<T>> CatchAsync<T>(Func<Task<Maybe<T>>> f, Handler handler)
	{
		try
		{
			return await f().ConfigureAwait(false);
		}
		catch (Exception e)
		{
			return None<T>(handler(e));
		}
	}
}
