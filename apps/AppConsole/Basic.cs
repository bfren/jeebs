// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using RndF;
using Wrap;

namespace AppConsole;

public sealed class Basic
{
	public static FailureValue CreateFail() =>
		new("Basic failure: '{Str}' - '{Guid}'.", Rnd.Str, Rnd.Guid);

	public static FailureValue CreateFailWithException() =>
		new FailureValue(new InvalidOperationException()) with { Message = "Exception message." };

	public static string DoSomething(string input) =>
		throw new NullReferenceException(nameof(input));
}
