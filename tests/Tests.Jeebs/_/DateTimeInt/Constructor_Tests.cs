// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeInt_Tests;

public class Constructor_Tests
{
	[Fact]
	public void From_Integers_Sets_Values()
	{
		// Arrange
		const string expected = "200001020304";

		// Act
		var result = new DateTimeInt(2000, 1, 2, 3, 4);

		// Assert
		Assert.Equal(expected, result.ToString());
	}

	[Fact]
	public void From_DateTime_Sets_Values()
	{
		// Arrange
		const string expected = "200001020304";
		var input = new DateTime(2000, 1, 2, 3, 4, 5);

		// Act
		var result = new DateTimeInt(input);

		// Assert
		Assert.Equal(expected, result.ToString());
	}

	[Fact]
	public void From_Valid_String_Sets_Values()
	{
		// Arrange
		const string input = "200001020304";

		// Act
		var result = new DateTimeInt(input);

		// Assert
		Assert.Equal(input, result.ToString());
	}

	[Theory]
	[InlineData("2000")]
	[InlineData("20000102030405")]
	[InlineData("invalid")]
	public void From_Invalid_String_Throws_ArgumentException(string input)
	{
		// Arrange

		// Act
		var result = object () => new DateTimeInt(input);

		// Assert
		Assert.Throws<ArgumentException>(result);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void From_Null_Or_Empty_String_Returns_Zeroes(string? input)
	{
		// Arrange

		// Act
		var result = new DateTimeInt(input);

		// Assert
		Assert.Equal("000000000000", result.ToString());
	}

	[Fact]
	public void From_Valid_Long_Sets_Values()
	{
		// Arrange
		const long input = 200001020304;

		// Act
		var result = new DateTimeInt(input);

		// Assert
		Assert.Equal(input.ToString(), result.ToString());
	}

	[Theory]
	[InlineData(2000)]
	[InlineData(20000102030405)]
	public void From_Invalid_Long_Throws_ArgumentException(long input)
	{
		// Arrange

		// Act
		var result = object () => new DateTimeInt(input);

		// Assert
		Assert.Throws<ArgumentException>(result);
	}
}
