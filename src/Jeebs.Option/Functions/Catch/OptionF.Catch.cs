// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;

namespace F;

public static partial class OptionF
{
	/// <summary>
	/// Catch any unhandled exceptions in the chain
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <param name="f">The chain to execute</param>
	/// <param name="handler">Caught exception handler</param>
	internal static Option<T> Catch<T>(Func<Option<T>> f, Handler handler)
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
