// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using Xunit;

namespace JeebsF.StringF_Tests
{
	public class Random_Tests
	{
		[Fact]
		public void Valid_Length_Returns_String()
		{
			// Arrange
			const int length = 10;

			// Act
			var result = StringF.Random(length);

			// Assert
			Assert.Equal(length, result.Length);
		}

		[Fact]
		public void Invalid_Length_Throws_InvalidOperationException()
		{
			// Arrange
			const int length = 3;

			// Act
			Action result = () => StringF.Random(length, upper: true, numbers: true, special: true);

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Fact]
		public void Returns_Only_Lowercase_Characters()
		{
			// Arrange

			// Act
			var result = StringF.Random(12, upper: false);

			// Assert
			Assert.True(result.All(c => StringF.LowercaseChars.Contains(c)));
		}

		[Fact]
		public void Returns_Only_Lowercase_And_Uppercase_Characters()
		{
			// Arrange

			// Act
			var result = StringF.Random(64, upper: true);

			// Assert
			Assert.True(result.All(c => StringF.LowercaseChars.Contains(c) || StringF.UppercaseChars.Contains(c)));
		}

		[Fact]
		public void Returns_Only_Lowercase_And_Numeric_Characters()
		{
			// Arrange

			// Act
			var result = StringF.Random(64, upper: false, numbers: true);

			// Assert
			Assert.True(result.All(c => StringF.LowercaseChars.Contains(c) || StringF.NumberChars.Contains(c)));
		}

		[Fact]
		public void Returns_Only_Lowercase_And_Special_Characters()
		{
			// Arrange

			// Act
			var result = StringF.Random(64, upper: false, special: true);

			// Assert
			Assert.True(result.All(c => StringF.LowercaseChars.Contains(c) || StringF.SpecialChars.Contains(c)));
		}

		[Fact]
		public void ReturnsStringWithAllCharacters()
		{
			// Arrange

			// Act
			var result = StringF.Random(64, upper: true, numbers: true, special: true);

			// Assert
			Assert.False(result.All(c => StringF.LowercaseChars.Contains(c)));
			Assert.False(result.All(c => StringF.UppercaseChars.Contains(c)));
			Assert.False(result.All(c => StringF.NumberChars.Contains(c)));
			Assert.False(result.All(c => StringF.SpecialChars.Contains(c)));
			Assert.True(result.All(c => StringF.AllChars.Contains(c)));
		}
	}
}
