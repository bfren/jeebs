// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Random.Rnd_Tests.DateF_Tests;

public class Get_Tests
{
	private static void Never_Returns_Number_Out_Of_Bounds(Func<DateOnly, int> value, int min, int max)
	{
		// Arrange
		const int iterations = 100000;
		var numbers = new List<int>();

		// Act
		for (int i = 0; i < iterations; i++)
		{
			var d = Rnd.DateF.Get();
			numbers.Add(value(d));
		}

		// Assert
		Assert.True(numbers.Min() == min);
		Assert.True(numbers.Max() == max);
	}

	[Fact]
	public void Never_Returns_Year_Out_Of_Bounds() =>
		Never_Returns_Number_Out_Of_Bounds(dt => dt.Year, 1, 9999);

	[Fact]
	public void Never_Returns_Month_Out_Of_Bounds() =>
		Never_Returns_Number_Out_Of_Bounds(dt => dt.Month, 1, 12);

	[Fact]
	public void Never_Returns_Day_Out_Of_Bounds() =>
		Never_Returns_Number_Out_Of_Bounds(dt => dt.Day, 1, 28);
}
