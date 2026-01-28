// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeExtensions_Tests;

public class FirstDayOfMonth_Tests
{
	[Fact]
	public void Date_ReturnsMidnightOnFirstDayOfMonth()
	{
		// Arrange
		var date = Rnd.DateTime;
		var expected = new DateTime(date.Year, date.Month, 1, 0, 0, 0);

		// Act
		var result = date.FirstDayOfMonth();

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
		var result = date.FirstDayOfMonth();

		// Assert
		Assert.Equal(input, result.Kind);
	}
}
