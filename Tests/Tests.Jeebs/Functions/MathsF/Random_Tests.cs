// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using Xunit;
using static JeebsF.MathsF;

namespace JeebsF.MathsF_Tests
{
	public class Random_Tests
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
				numbers.Add(Random());
			}

			// Assert
			Assert.True(numbers.Min() >= 0);
			Assert.True(numbers.Max() <= 1);
		}

		[Fact]
		public void Returns_Different_Number_Each_Time()
		{
			// Arrange
			const int iterations = 1000;
			var numbers = new List<double>();

			// Act
			for (int i = 0; i < iterations; i++)
			{
				numbers.Add(Random());
			}

			var unique = numbers.Distinct();

			// Assert
			Assert.Equal(unique.Count(), numbers.Count);
			Assert.True(numbers.Min() >= 0);
			Assert.True(numbers.Max() <= 1);
		}
	}
}
