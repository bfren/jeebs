// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Validate a call to <see cref="IFluentQuery{TEntity, TId}.Maximum"/>
	/// </summary>
	/// <param name="call">Call</param>
	/// <param name="expected">Expected value</param>
	public static void AssertMaximum(ICall call, ulong expected)
	{
		// Check the method
		AssertMethodName(call, nameof(IFake.Maximum));

		// Check each predicate
		Assert.Collection(call.GetArguments(),

			// Check that the correct value is being used
			actual => AssertEqual(expected, actual)
		);
	}
}
