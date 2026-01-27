// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Assert that <paramref name="call"/> is a call to a method called <paramref name="expected"/>.
	/// </summary>
	/// <param name="call">Call.</param>
	/// <param name="expected">Expected method name.</param>
	/// <exception cref="MethodNameException"></exception>
	internal static void AssertMethodName(ICall call, string expected)
	{
		var actual = call.GetMethodInfo().Name;

		try
		{
			Assert.Equal(expected, actual);
		}
		catch (EqualException ex)
		{
			throw new MethodNameException($"Expected call to '{expected}' but received call to '{actual}'.", ex);
		}
	}
}
