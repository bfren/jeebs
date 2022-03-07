// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// Catch any unhandled exceptions in the chain
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="f">The chain to execute</param>
	/// <param name="handler">Caught exception handler</param>
	internal static Maybe<T> Catch<T>(Func<Maybe<T>> f, Handler handler)
	{
		try
		{
			return f();
		}
		catch (Exception e)
		{
			return None<T>(handler(e));
		}
	}
}
