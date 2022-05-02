// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Assert that <paramref name="actual"/> is equal to <paramref name="expected"/>
	/// </summary>
	/// <typeparam name="T">Value Type</typeparam>
	/// <param name="expected">Expected value</param>
	/// <param name="actual">Actual value</param>
	/// <exception cref="EqualTypeException"></exception>
	internal static void AssertEqual<T>(T expected, object? actual)
	{
		try
		{
			Assert.Equal(expected, actual);
		}
		catch (EqualException ex)
		{
			throw new EqualTypeException($"Expected '{expected}' but value was '{actual}'.", ex);
		}
	}
}
