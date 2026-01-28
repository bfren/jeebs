// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeExtensions_Tests;

public class EndOfDay_Tests
{
	[Fact]
	public void Date_ReturnsOneMinuteToMidnight()
	{
		// Arrange
		var date = Rnd.DateTime;
		var expected = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

		// Act
		var result = date.EndOfDay();

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(DateTimeKind.Unspecified)]
	[InlineData(DateTimeKind.Utc)]
	[InlineData(DateTimeKind.Local)]
	public void Date_Retains_Kind(DateTimeKind input)
	{
		// Arrange
		var date = DateTime.SpecifyKind(Rnd.DateTime, input);

		// Act
		var result = date.EndOfDay();

		// Assert
		Assert.Equal(input, result.Kind);
	}
}
