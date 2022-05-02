// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Assert that <paramref name="actual"/> is of type <typeparamref name="T"/> and equal to
	/// <paramref name="expected"/>
	/// </summary>
	/// <typeparam name="T">Value Type</typeparam>
	/// <param name="expected">Expected value</param>
	/// <param name="actual">Actual value</param>
	/// <exception cref="EqualTypeException"></exception>
	internal static void AssertEqualType<T>(T expected, object? actual)
	{
		try
		{
			Assert.Equal(
				expected: expected,
				actual: Assert.IsType<T>(actual)
			);
		}
		catch (IsTypeException ex)
		{
			throw new EqualTypeException($"Expected type '{typeof(T)}' but value was type '{actual?.GetType()}'.", ex);
		}
		catch (EqualException ex)
		{
			throw new EqualTypeException($"Expected '{expected}' but value was '{actual}'.", ex);
		}
	}
}
