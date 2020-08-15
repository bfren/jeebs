using Jeebs;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.DateTimeInt_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void FromIntegers_CreatesObject()
		{
			// Arrange
			const string expected = "200001020304";

			// Act
			var result = new DateTimeInt(2000, 1, 2, 3, 4).ToString();

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void FromDateTime_CreatesObject()
		{
			// Arrange
			const string expected = "200001020304";
			var input = new DateTime(2000, 1, 2, 3, 4, 5);

			// Act
			var result = new DateTimeInt(input).ToString();

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void FromValidString_CreatesObject()
		{
			// Arrange
			const string input = "200001020304";

			// Act
			var result = new DateTimeInt(input).ToString();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("2000")]
		[InlineData("20000102030405")]
		[InlineData("invalid")]
		public void FromInvalidString_ThrowsArgumentException(string input)
		{
			// Arrange

			// Act
			Action result = () => new DateTimeInt(input);

			// Assert
			Assert.Throws<ArgumentException>(result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void FromNullString_ReturnsEmpty(string input)
		{
			// Arrange

			// Act
			var result = new DateTimeInt(input);

			// Assert
			Assert.Equal(string.Empty, result.ToString());
		}

		[Fact]
		public void FromValidLong_CreatesObject()
		{
			// Arrange
			const long input = 200001020304;

			// Act
			var result = new DateTimeInt(input).ToString();

			// Assert
			Assert.Equal(input.ToString(), result);
		}

		[Theory]
		[InlineData(2000)]
		[InlineData(20000102030405)]
		public void FromInvalidLong_ThrowsArgumentException(long input)
		{
			// Arrange

			// Act
			Action result = () => new DateTimeInt(input);

			// Assert
			Assert.Throws<ArgumentException>(result);
		}
	}
}
