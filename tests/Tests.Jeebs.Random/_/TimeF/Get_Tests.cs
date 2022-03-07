// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static F.Rnd.TimeF;

namespace F.TimeF_Tests;

public class Get_Tests
{
	private static void Never_Returns_Number_Out_Of_Bounds(Func<TimeOnly, int> value, int min, int max)
	{
		// Arrange
		const int iterations = 100000;
		var numbers = new List<int>();

		// Act
		for (int i = 0; i < iterations; i++)
		{
			var t = Get();
			numbers.Add(value(t));
		}

		// Assert
		Assert.True(numbers.Min() == min);
		Assert.True(numbers.Max() == max);
	}

	[Fact]
	public void Never_Returns_Hour_Out_Of_Bounds() =>
		Never_Returns_Number_Out_Of_Bounds(dt => dt.Hour, 0, 23);

	[Fact]
	public void Never_Returns_Minute_Out_Of_Bounds() =>
		Never_Returns_Number_Out_Of_Bounds(dt => dt.Minute, 0, 59);

	[Fact]
	public void Never_Returns_Second_Out_Of_Bounds() =>
		Never_Returns_Number_Out_Of_Bounds(dt => dt.Second, 0, 59);
}
