// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.MathsF_Tests;

public class Factorial_Tests
{
	[Fact]
	public void Less_Than_Zero_Returns_Minus_One()
	{
		// Arrange

		// Act
		var result = MathsF.Factorial(-1);

		// Assert
		Assert.Equal(-1, result);
	}

	[Theory]
	[InlineData(0, 1)]
	[InlineData(1, 1)]
	[InlineData(2, 2)]
	[InlineData(3, 6)]
	[InlineData(4, 24)]
	[InlineData(5, 120)]
	public void Returns_Correct_Result(long input, long expected)
	{
		// Arrange

		// Act
		var result = MathsF.Factorial(input);

		// Assert
		Assert.Equal(expected, result);
	}
}
