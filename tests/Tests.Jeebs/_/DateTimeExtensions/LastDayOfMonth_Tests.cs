// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeExtensions_Tests;

public class LastDayOfMonth_Tests
{
	[Fact]
	public void Date_ReturnsOneMinuteToMidnightOnLastDayOfMonth()
	{
		// Arrange
		var date = Rnd.DateTime;
		var expected = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1).EndOfDay();

		// Act
		var result = date.LastDayOfMonth();

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
		var result = date.LastDayOfMonth();

		// Assert
		Assert.Equal(input, result.Kind);
	}
}
