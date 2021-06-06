// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Linq;
using Xunit;
using static F.Rnd.StringF;

namespace F.StringF_Tests
{
	public class Get_Tests
	{
		[Fact]
		public void Valid_Length_Returns_String()
		{
			// Arrange
			const int length = 10;

			// Act
			var result = Get(length);

			// Assert
			Assert.Equal(length, result.Length);
		}

		[Fact]
		public void Invalid_Length_Throws_InvalidOperationException()
		{
			// Arrange
			const int length = 3;

			// Act
			static void result() => Get(length, upper: true, numbers: true, special: true);

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Fact]
		public void Returns_Only_Lowercase_Characters()
		{
			// Arrange

			// Act
			var result = Get(12, upper: false);

			// Assert
			Assert.True(result.All(c => LowercaseChars.Contains(c)));
		}

		[Fact]
		public void Returns_Only_Lowercase_And_Uppercase_Characters()
		{
			// Arrange

			// Act
			var result = Get(64, upper: true);

			// Assert
			Assert.True(result.All(c => LowercaseChars.Contains(c) || UppercaseChars.Contains(c)));
		}

		[Fact]
		public void Returns_Only_Lowercase_And_Numeric_Characters()
		{
			// Arrange

			// Act
			var result = Get(64, upper: false, numbers: true);

			// Assert
			Assert.True(result.All(c => LowercaseChars.Contains(c) || NumberChars.Contains(c)));
		}

		[Fact]
		public void Returns_Only_Lowercase_And_Special_Characters()
		{
			// Arrange

			// Act
			var result = Get(64, upper: false, special: true);

			// Assert
			Assert.True(result.All(c => LowercaseChars.Contains(c) || SpecialChars.Contains(c)));
		}

		[Fact]
		public void ReturnsStringWithAllCharacters()
		{
			// Arrange

			// Act
			var result = Get(64, upper: true, numbers: true, special: true);

			// Assert
			Assert.False(result.All(c => LowercaseChars.Contains(c)));
			Assert.False(result.All(c => UppercaseChars.Contains(c)));
			Assert.False(result.All(c => NumberChars.Contains(c)));
			Assert.False(result.All(c => SpecialChars.Contains(c)));
			Assert.True(result.All(c => AllChars.Contains(c)));
		}
	}
}
