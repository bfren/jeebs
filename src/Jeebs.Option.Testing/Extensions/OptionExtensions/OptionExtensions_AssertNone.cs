// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Internals;
using Xunit;

namespace Jeebs;

/// <summary>
/// Option Extensions: AssertNone
/// </summary>
public static class OptionExtensionsAssertNone
{
	/// <summary>
	/// Assert that <paramref name="this"/> is <see cref="None{T}"/> and return the Reason
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <param name="this">Option</param>
	public static Msg AssertNone<T>(this Option<T> @this) =>
		Assert.IsType<None<T>>(@this).Reason;
}
