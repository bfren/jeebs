// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs;

/// <summary>
/// Maybe Extensions: AssertBoolean
/// </summary>
public static class MaybeExtensionsAssertBoolean
{
	/// <summary>
	/// Assert that <paramref name="this"/> is <see cref="Internals.Some{T}"/> and the value is false
	/// </summary>
	/// <param name="this">Maybe</param>
	public static void AssertFalse(this Maybe<bool> @this) =>
		Assert.False(@this.AssertSome());

	/// <summary>
	/// Assert that <paramref name="this"/> is <see cref="Internals.Some{T}"/> and the value is true
	/// </summary>
	/// <param name="this"></param>
	public static void AssertTrue(this Maybe<bool> @this) =>
		Assert.True(@this.AssertSome());
}
