// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Assert that <paramref name="call"/> has one generic argument, of type <typeparamref name="TExpected"/>.
	/// </summary>
	/// <typeparam name="TExpected">Generic argument.</typeparam>
	/// <param name="call">Call.</param>
	/// <exception cref="GenericArgumentException"></exception>
	internal static void AssertGenericArgument<TExpected>(ICall call)
	{
		// Check correct number of generic arguments
		var args = call.GetMethodInfo().GetGenericArguments();
		if (args.Length == 0)
		{
			throw new GenericArgumentException("Expected one generic argument but found none.");
		}
		if (args.Length > 1)
		{
			throw new GenericArgumentException($"Expected one generic argument but found {args.Length}.");
		}

		// Check generic argument
		var expected = typeof(TExpected);
		var actual = args[0];
		try
		{
			Assert.Equal(expected, actual);
		}
		catch (EqualException ex)
		{
			throw new GenericArgumentException($"Expected type '{expected}' but found '{actual}'.", ex);
		}
	}
}
