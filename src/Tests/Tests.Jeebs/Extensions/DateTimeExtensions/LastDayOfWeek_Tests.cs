// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.DateTimeExtensions_Tests
{
	public class LastDayOfWeek_Tests
	{
		[Fact]
		public void Date_ReturnsOneMinuteToMidnightOnLastDayOfWeek()
		{
			// Arrange
			var date = new DateTime(2000, 1, 4, 15, 59, 30);
			var expected = new DateTime(2000, 1, 8, 23, 59, 59);

			// Act
			var actual = date.LastDayOfWeek();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
