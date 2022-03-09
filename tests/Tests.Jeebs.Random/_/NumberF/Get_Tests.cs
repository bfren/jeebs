// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Random.Rnd_Tests.NumberF_Tests;

public class Get_Tests
{
	[Fact]
	public void Never_Returns_Number_Out_Of_Bounds()
	{
		// Arrange
		const int iterations = 1000000;
		var numbers = new List<double>();

		// Act
		for (int i = 0; i < iterations; i++)
		{
			numbers.Add(Rnd.NumberF.Get());
		}

		// Assert
		Assert.True(numbers.Min() >= 0);
		Assert.True(numbers.Max() <= 1);
	}

	[Fact]
	public void Returns_Different_Number_Each_Time()
	{
		// Arrange
		const int iterations = 10000;
		var numbers = new List<double>();

		// Act
		for (int i = 0; i < iterations; i++)
		{
			numbers.Add(Rnd.NumberF.Get());
		}

		var unique = numbers.Distinct();

		// Assert
		Assert.InRange(unique.Count(), numbers.Count - 2, numbers.Count);
		Assert.True(numbers.Min() >= 0);
		Assert.True(numbers.Max() <= 1);
	}
}
