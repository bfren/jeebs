// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.DateTimeF_Tests;

public class FromFormat_Tests
{
	[Fact]
	public void CorrectInput_ValidFormat_ReturnsDateTime()
	{
		// Arrange
		const string input = "15:59 04/01/2000";
		var expected = new DateTime(2000, 1, 4, 15, 59, 00);
		const string format = "HH:mm dd/MM/yyyy";

		// Act
		var result = DateTimeF.FromFormat(input, format);

		// Assert
		var success = result.AssertSome();
		Assert.Equal(expected, success);
	}

	[Fact]
	public void CorrectInput_InvalidFormat_ReturnsFalse()
	{
		// Arrange
		const string input = "15:59 04/01/2000";
		const string format = "this is not a valid format";

		// Act
		var result = DateTimeF.FromFormat(input, format);

		// Assert
		result.AssertNone();
	}

	[Fact]
	public void IncorrectInput_ValidFormat_ReturnsFalse()
	{
		// Arrange
		const string input = "15:59:30 01/31/2000";
		const string format = "HH:mm dd/MM/yyyy";

		// Act
		var result = DateTimeF.FromFormat(input, format);

		// Assert
		result.AssertNone();
	}

	[Fact]
	public void IncorrectInput_InvalidFormat_ReturnsFalse()
	{
		// Arrange
		const string input = "15:59:30 01/31/2000";
		const string format = "this is not a valid format";

		// Act
		var result = DateTimeF.FromFormat(input, format);

		// Assert
		result.AssertNone();
	}
}
