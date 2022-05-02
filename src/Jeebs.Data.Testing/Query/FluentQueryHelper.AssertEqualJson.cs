// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using Jeebs.Functions;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Assert that <paramref name="actual"/> and <paramref name="expected"/> are equal by
	/// serialising as JSON - to support anonymous types
	/// </summary>
	/// <param name="expected">Expected value</param>
	/// <param name="actual">Actual value</param>
	/// <exception cref="EqualJsonException"></exception>
	internal static void AssertEqualJson(object expected, object? actual)
	{
		try
		{
			Assert.Equal(
				expected: JsonF.Serialise(expected).UnsafeUnwrap(),
				actual: JsonF.Serialise(actual).UnsafeUnwrap()
			);
		}
		catch (EqualException ex)
		{
			throw new EqualJsonException($"Expected '{expected}' but value was '{actual}'.", ex);
		}
	}
}
