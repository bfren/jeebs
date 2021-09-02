// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.Rnd.BooleanF;

namespace F.BooleanF_Tests;

public class Get_Tests
{
	[Fact]
	public void Returns_True_Or_False()
	{
		// Arrange
		const int iterations = 100;
		var results = new List<bool>();

		// Act
		for (int i = 0; i < iterations; i++)
		{
			results.Add(Get());
		}

		// Assert
		Assert.Equal(2, results.Distinct().Count());
	}
}
