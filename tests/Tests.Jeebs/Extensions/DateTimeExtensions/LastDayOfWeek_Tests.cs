// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeExtensions_Tests;

public class LastDayOfWeek_Tests
{
	[Fact]
	public void Date_ReturnsOneMinuteToMidnightOnLastDayOfWeek()
	{
		// Arrange
		var date = new DateTime(2000, 1, 4, 15, 59, 30);
		var expected = new DateTime(2000, 1, 8, 23, 59, 59);

		// Act
		var result = date.LastDayOfWeek();

		// Assert
		Assert.Equal(expected, result);
	}
}
