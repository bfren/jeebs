// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using Xunit;
using static F.Rnd.NumberF;

namespace F.NumberF_Tests
{
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
				numbers.Add(Get());
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
				numbers.Add(Get());
			}

			var unique = numbers.Distinct();

			// Assert
			Assert.InRange(unique.Count(), numbers.Count - 2, numbers.Count);
			Assert.True(numbers.Min() >= 0);
			Assert.True(numbers.Max() <= 1);
		}
	}
}
