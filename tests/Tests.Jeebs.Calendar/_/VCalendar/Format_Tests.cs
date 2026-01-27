// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Calendar.VCalendar_Tests;

public class Format_Tests
{
	[Fact]
	public void Returns_Date_With_Time()
	{
		// Arrange
		var dt = Rnd.DateTime;
		var expected = dt.ToString(@"yyyyMMdd\THHmmss");

		// Act
		var result = VCalendar.Format(dt);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Returns_Date_Without_Time()
	{
		// Arrange
		var dt = Rnd.DateTime;
		var expected = dt.ToString("yyyyMMdd");

		// Act
		var result = VCalendar.Format(dt, false);

		// Assert
		Assert.Equal(expected, result);
	}
}
