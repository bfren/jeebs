// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace F.MathsF_Tests
{
	public class Permutations_Tests
	{
		[Fact]
		public void N_Less_Than_Zero_Returns_Minus_One()
		{
			// Arrange

			// Act
			var result = MathsF.Permutations(-1, Rnd.Lng);

			// Assert
			Assert.Equal(-1, result);
		}

		[Fact]
		public void R_Less_Than_Zero_Returns_Minus_One()
		{
			// Arrange

			// Act
			var result = MathsF.Permutations(Rnd.Lng, -1);

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
			var result = MathsF.Permutations(n, r);

			// Assert
			Assert.Equal(-1, result);
		}

		[Theory]
		[InlineData(5, 2, 20)]
		[InlineData(16, 3, 3360)]
		public void Returns_Correct_Result(long n, long r, long expected)
		{
			// Arrange

			// Act
			var result = MathsF.Permutations(n, r);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
