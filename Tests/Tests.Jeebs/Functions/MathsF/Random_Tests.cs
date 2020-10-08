using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static F.MathsF;

namespace F.MathsF_Tests
{
	public class Random_Tests
	{
		[Fact]
		public void Returns_Different_Number_Each_Time()
		{
			// Arrange
			var numbers = new List<double>();

			// Act
			for (int i = 0; i < 1000000; i++)
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
