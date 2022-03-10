﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeExtensions_Tests;

public class FirstDayOfMonth_Tests
{
	[Fact]
	public void Date_ReturnsMidnightOnFirstDayOfMonth()
	{
		// Arrange
		var date = new DateTime(2000, 1, 4, 15, 59, 30);
		var expected = new DateTime(2000, 1, 1, 0, 0, 0);

		// Act
		var actual = date.FirstDayOfMonth();

		// Assert
		Assert.Equal(expected, actual);
	}
}