// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Xunit;
using static F.MaybeF;

namespace F.MaybeF_Tests;

public class True_Tests
{
	[Fact]
	public void Returns_Some_With_Value_True()
	{
		// Arrange

		// Act
		var result = True;

		// Assert
		result.AssertTrue();
	}
}
