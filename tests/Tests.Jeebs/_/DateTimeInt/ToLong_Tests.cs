// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeInt_Tests;

public class ToLong_Tests
{
	[Fact]
	public void Valid_Returns_Long_Value()
	{
		// Arrange
		const long expected = 200001020304;
		var input = new DateTimeInt(expected);

		// Act
		var result = input.ToLong();

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(IsValidDateTime_Tests.Invalid_DateTime_Data), MemberType = typeof(IsValidDateTime_Tests))]
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
	public void Invalid_Returns_Zero(int year, int month, int day, int hour, int minute, string _)
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
	{
		// Arrange
		var input = new DateTimeInt(year, month, day, hour, minute);

		// Act
		var result = input.ToLong();

		// Assert
		Assert.Equal(0, result);
	}
}
