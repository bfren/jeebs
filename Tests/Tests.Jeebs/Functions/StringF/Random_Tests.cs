// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using Xunit;

namespace F.StringF_Tests
{
	public class Random_Tests
	{
		[Fact]
		public void ValidLength_ReturnsString()
		{
			// Arrange
			const int length = 10;

			// Act
			var result = StringF.Random(length);

			// Assert
			Assert.Equal(length, result.Length);
		}

		[Fact]
		public void InvalidLength_ThrowsInvalidOperationException()
		{
			// Arrange
			const int length = 3;

			// Act
			Action result = () => StringF.Random(length, upper: true, numbers: true, special: true);

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Fact]
		public void ReturnsOnlyLowercaseCharacters()
		{
			// Arrange

			// Act
			var result = StringF.Random(12, upper: false);

			// Assert
			Assert.True(result.All(c => StringF.LowercaseChars.Contains(c)));
		}

		[Fact]
		public void ReturnsOnlyLowercaseAndUppercaseCharacters()
		{
			// Arrange

			// Act
			var result = StringF.Random(12, upper: true);

			// Assert
			Assert.True(result.All(c => StringF.LowercaseChars.Contains(c) || StringF.UppercaseChars.Contains(c)));
		}

		[Fact]
		public void ReturnsOnlyLowercaseAndNumericCharacters()
		{
			// Arrange

			// Act
			var result = StringF.Random(12, upper: false, numbers: true);

			// Assert
			Assert.True(result.All(c => StringF.LowercaseChars.Contains(c) || StringF.NumberChars.Contains(c)));
		}

		[Fact]
		public void ReturnsOnlyLowercaseAndSpecialCharacters()
		{
			// Arrange

			// Act
			var result = StringF.Random(12, upper: false, special: true);

			// Assert
			Assert.True(result.All(c => StringF.LowercaseChars.Contains(c) || StringF.SpecialChars.Contains(c)));
		}

		[Fact]
		public void ReturnsStringWithAllCharacters()
		{
			// Arrange

			// Act
			var result = StringF.Random(12, upper: true, numbers: true, special: true);

			// Assert
			Assert.True(result.All(c => StringF.AllChars.Contains(c)));
		}
	}
}
