// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace F.NumberF_Tests;

public class Combinations_Tests
{
	[Fact]
	public void N_Less_Than_Zero_Returns_Minus_One()
	{
		// Arrange

		// Act
		var result = MathsF.Combinations(-1, Rnd.Lng);

		// Assert
		Assert.Equal(-1, result);
	}

	[Fact]
	public void R_Less_Than_Zero_Returns_Minus_One()
	{
		// Arrange

		// Act
		var result = MathsF.Combinations(Rnd.Lng, -1);

		// Assert
		Assert.Equal(-1, result);
	}

	[Fact]
	public void N_Less_Than_R_Returns_Minus_One()
	{
		// Arrange
		var n = Rnd.NumberF.GetInt64(min: 2, max: 10);
		var r = n + 1;

		// Act
		var result = MathsF.Combinations(n, r);

		// Assert
		Assert.Equal(-1, result);
	}

	[Theory]
	[InlineData(5, 2, 10)]
	[InlineData(16, 3, 560)]
	public void Returns_Correct_Result(long n, long r, long expected)
	{
		// Arrange

		// Act
		var result = MathsF.Combinations(n, r);

		// Assert
		Assert.Equal(expected, result);
	}
}
