using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static F.MathsF;

namespace F.MathsF_Tests
{
	public class RandomInteger_Tests
	{
		[Fact]
		public void Min_GreaterThan_Max_Throws_ArgumentOutOfRangeException()
		{
			// Arrange
			const long min = 3L;
			const long max = 2L;

			// Act
			static void action() => RandomInteger(min, max);

			// Assert
			var ex = Assert.Throws<ArgumentOutOfRangeException>(action);
			Assert.Equal("Minimium value must be less than the maximum value. (Parameter 'min')\r\nActual value was 3.", ex.Message);
		}

		[Fact]
		public void Min_LessThan_Zero_Throws_ArgumentException()
		{
			// Arrange
			const long min = long.MinValue;

			// Act
			static void action() => RandomInteger(min: min);

			// Assert
			var ex = Assert.Throws<ArgumentException>(action);
			Assert.Equal("Minimum value must be at least 0. (Parameter 'min')", ex.Message);
		}

		[Fact]
		public void Never_Returns_Number_Out_Of_Bounds()
		{
			// Arrange
			const long min = 1L;
			const long max = 10L;
			var numbers = new List<double>();

			// Act
			for (int i = 0; i < 1000000; i++)
			{
				numbers.Add(RandomInteger(min,max));
			}

			// Assert
			Assert.True(numbers.Min() == min);
			Assert.True(numbers.Max() == max);
		}

		[Fact]
		public void Returns_Different_Number_Each_Time()
		{
			// Arrange
			var numbers = new List<double>();

			// Act
			for (int i = 0; i < 1000000; i++)
			{
				numbers.Add(RandomInteger());
			}

			var unique = numbers.Distinct();

			// Assert
			Assert.Equal(unique.Count(), numbers.Count);
		}
	}
}
